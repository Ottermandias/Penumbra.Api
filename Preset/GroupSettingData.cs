global using GroupSettingData =
    (System.Collections.Generic.Dictionary<(System.Guid Identifier, string? Name), byte> Options, bool DisableAllUnknown);
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> Methods to use on the group setting data. </summary>
public static class GroupSettingDataExtensions
{
    /// <summary> Readonly methods to use on the group setting data. </summary> 
    extension(ref readonly GroupSettingData data)
    {
        /// <summary> Create a new, empty group setting data. </summary>
        public static GroupSettingData Create()
            => new(new Dictionary<ModObjectIdentifier, byte>(ModObjectIdentifierExtensions.Comparer.Instance), false);

        /// <summary> Get the correct value for a specific option. </summary>
        /// <param name="identifier"> The option reference. </param>
        /// <returns> The desired state for that option. Unknown options are disabled or ignored depending on DisableAllUnknown. </returns>
        public OptionState GetValue(ModObjectIdentifier identifier)
        {
            // Check for matching ID. Does not need to remove the name since identifiers are equal when their IDs are equal.
            if (identifier.Identifier != Guid.Empty && data.Options.TryGetValue(identifier, out var v))
                return (OptionState)v;

            // Check for matching name.
            if (!string.IsNullOrEmpty(identifier.Name) && data.Options.TryGetValue(new ModObjectIdentifier(Guid.Empty, identifier.Name), out v))
                return (OptionState)v;

            return data.DisableAllUnknown ? OptionState.Disabled : OptionState.Ignored;
        }

        /// <summary> Get whether this group setting data is equal to the passed one. </summary>
        /// <param name="other"> The other group setting data to check. </param>
        /// <returns> True if they have identical options set to identical values and the same options ignored. </returns>
        public bool Equals(in GroupSettingData other)
        {
            if (data.DisableAllUnknown != other.DisableAllUnknown || data.Options.Count != other.Options.Count)
                return false;

            foreach (var (key, value) in data.Options)
            {
                if (!other.Options.TryGetValue(key, out var otherValue))
                    return false;
                if (otherValue != value)
                    return false;
            }

            return true;
        }

        /// <summary> Get all enabled options. </summary>
        public IEnumerable<ModObjectIdentifier> Enabled()
            => data.Options.Where(p => p.Value is (byte)OptionState.Enabled).Select(p => p.Key);

        /// <summary> Get all disabled options. </summary>
        public IEnumerable<ModObjectIdentifier> Disabled()
            => data.Options.Where(p => p.Value is (byte)OptionState.Disabled).Select(p => p.Key);

        /// <summary> Get all toggling options. </summary>
        public IEnumerable<ModObjectIdentifier> Toggle()
            => data.Options.Where(p => p.Value is (byte)OptionState.Toggle).Select(p => p.Key);

        /// <summary> Get all options with their respective state, but ordered by the option name. </summary>
        public IEnumerable<(ModObjectIdentifier Identifier, OptionState State)> Ordered()
            => data.Options.Select(p => (p.Key, (OptionState)p.Value)).OrderBy(p => p.Key.Name);

        /// <summary> Turn the selected options in this group setting data to generic options by discarding the identifiers and only keeping the referenced names. </summary>
        /// <returns> True if this changed the data, false if not. </returns>
        public bool MakeGeneric()
        {
            var oldDict = data.Options.ToArray();
            data.Options.Clear();
            var changes = false;
            foreach (var (key, value) in oldDict)
            {
                // Add generic keys unaltered.
                if (key.Identifier == Guid.Empty)
                {
                    data.Options.Add(key, value);
                    continue;
                }

                changes = true;
                // Skip empty keys.
                if (key.Name is null)
                    continue;

                var newKey = new ModObjectIdentifier(Guid.Empty, key.Name);
                if (data.Options.TryAdd(newKey, value))
                    continue;

                // Multiple objects resolve to the same name. Prefer in order Enabled > Disabled > Ignored.
                if (!data.Options.TryGetValue(newKey, out var conflictingValue))
                    continue;

                if (conflictingValue < value)
                    data.Options[newKey] = value;
            }

            return changes;
        }

        /// <summary> Get the number of referenced options. </summary>
        public int Count
            => data.Options.Count;

        /// <summary> Add the properties of this group setting data to a JSON object without starting or ending an object. </summary>
        public void AddToJson(Utf8JsonWriter j)
        {
            if (data.DisableAllUnknown)
                j.WriteBoolean("DisableAllUnknown"u8, true);
            if (data.Count > 0)
            {
                j.WriteStartArray("Options"u8);
                foreach (var (option, value) in data.Options)
                {
                    j.WriteStartObject();
                    option.AddToJson(j);
                    j.WriteString("State"u8, value switch
                    {
                        (byte)OptionState.Disabled => "Disabled"u8,
                        (byte)OptionState.Enabled  => "Enabled"u8,
                        (byte)OptionState.Toggle   => "Toggle"u8,
                        _                          => "Ignored"u8,
                    });
                    j.WriteEndObject();
                }

                j.WriteEndArray();
            }
        }
    }

    /// <summary> Write-access methods to use on the group setting data. </summary> 
    extension(ref GroupSettingData data)
    {
        /// <summary> Try to read one of the relevant properties for a group setting data from the reader. </summary>
        /// <param name="j"> The JSON reader. </param>
        /// <returns> True if one of the relevant properties was parsed and used to populate this object, false if not. </returns>
        /// <exception cref="JsonException"> If invalid JSON or wrong value types are encountered. </exception>
        /// <remarks> Only call this on a property name token. </remarks>
        public bool ReadJson(ref Utf8JsonReader j)
        {
            Debug.Assert(j.TokenType is JsonTokenType.PropertyName);
            if (j.ValueTextEquals("DisableAllUnknown"u8))
            {
                data.DisableAllUnknown = j.GetBoolean();
                return true;
            }

            if (!j.ValueTextEquals("Options"u8))
                return false;

            // Read Options dictionary.
            data.Options.Clear();
            if (!j.Read())
                throw new JsonException("Unexpected end after array property Options.");

            if (j.TokenType is JsonTokenType.Null)
                return true;

            if (j.TokenType is not JsonTokenType.StartArray)
                throw new JsonException($"Unexpected value type {j.TokenType} for array property Options.");

            var limit = j.CurrentDepth;
            while (j.Read())
            {
                if (j.TokenType is JsonTokenType.EndArray && j.CurrentDepth == limit)
                    break;

                // This should not be able to happen?
                if (j.CurrentDepth < limit)
                    throw new JsonException("Invalid JSON: Left object depth without ending it.");

                if (j.TokenType is not JsonTokenType.StartObject)
                    continue;

                var     limit2 = j.CurrentDepth;
                Guid?   guid   = null;
                string? name   = null;
                var     state  = OptionState.Ignored;
                while (j.Read())
                {
                    if (j.TokenType is JsonTokenType.EndObject && j.CurrentDepth == limit2)
                        break;

                    // This should not be able to happen?
                    if (j.CurrentDepth < limit2)
                        throw new JsonException("Invalid JSON: Left object depth without ending it.");

                    if (j.TokenType is not JsonTokenType.PropertyName)
                        continue;

                    if (j.ValueTextEquals("State"u8))
                    {
                        if (!j.Read())
                            throw new JsonException("Unexpected end after OptionState property State.");

                        state = j.TokenType switch
                        {
                            JsonTokenType.Null  => OptionState.Ignored,
                            JsonTokenType.True  => OptionState.Enabled,
                            JsonTokenType.False => OptionState.Disabled,
                            JsonTokenType.String => j.GetString() switch
                            {
                                "Enabled" or "enabled" or "True" or "true"     => OptionState.Enabled,
                                "Disabled" or "disabled" or "False" or "false" => OptionState.Enabled,
                                "Toggle" or "toggle"                           => OptionState.Toggle,
                                _                                              => OptionState.Ignored,
                            },
                            _ => throw new JsonException("Unexpected value for OptionState property State."),
                        };
                    }
                    else if (!ModObjectIdentifier.ReadJsonProperties(ref j, ref guid, ref name))
                    {
                        j.Skip();
                    }
                }

                var identifier = new ModObjectIdentifier(guid ?? Guid.Empty, name);
                if (!identifier.IsEmpty)
                    data.Options.Add(identifier, (byte)state);
            }

            return true;
        }
    }
}
