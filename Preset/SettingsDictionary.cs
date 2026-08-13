global using SettingsDictionary =
    System.Collections.Generic.Dictionary<(System.Guid Identifier, string? Name), (
        System.Collections.Generic.Dictionary<(System.Guid Identifier, string? Name), byte> Options, bool DisableAllUnknown)>;
using System.Linq;
using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> Methods to use on the settings dictionary. </summary>
public static class SettingsDictionaryExtensions
{
    /// <summary> Methods to use on the settings dictionary. </summary>
    extension(SettingsDictionary dict)
    {
        /// <summary> Turn the selected groups and options in this dictionary to generic references by discarding the identifiers and only keeping the referenced names. </summary>
        /// <returns> True if this changed the data, false if not. </returns>
        public bool MakeGeneric()
        {
            var changes = false;
            var oldDict = dict.ToArray();
            dict.Clear();
            for (var i = 0; i < oldDict.Length; ++i)
            {
                var (id, data) = oldDict[i];
                if (id.Identifier == Guid.Empty)
                    continue;

                if (id.Name is null)
                {
                    changes = true;
                    continue;
                }

                changes |= data.MakeGeneric();
                changes |= dict.TryAdd(new ModObjectIdentifier(Guid.Empty, id.Name), data);
            }

            return changes;
        }

        /// <summary> Replace all group references equal to <paramref name="oldIdentifier"/> in this dictionary with references to <paramref name="newIdentifier"/>. </summary>
        /// <returns> True if this changed anything. </returns>
        /// <remarks> If <paramref name="newIdentifier"/> already exists in the dictionary, its old values will be overwritten. </remarks>
        public bool ReplaceGroupIdentifier(ModObjectIdentifier oldIdentifier, ModObjectIdentifier newIdentifier)
        {
            if (oldIdentifier.Equals(newIdentifier) || newIdentifier.IsEmpty)
                return false;

            if (!dict.Remove(oldIdentifier, out var data))
                return false;

            dict[newIdentifier] = data;
            return true;
        }

        /// <summary> Replace all option references equal to <paramref name="oldIdentifier"/> in this dictionary with references to <paramref name="newIdentifier"/>. </summary>
        /// <returns> True if this changed anything. </returns>
        /// <remarks> If <paramref name="newIdentifier"/> already exists in a group's dictionary, its old value will be overwritten. </remarks>
        public bool ReplaceOptionIdentifiers(ModObjectIdentifier oldIdentifier, ModObjectIdentifier newIdentifier, ModObjectIdentifier? groupId)
        {
            if (oldIdentifier.Equals(newIdentifier) || newIdentifier.IsEmpty)
                return false;

            if (groupId is null)
                return dict.Values.Aggregate(false, (current, data) => current | ChangeIdentifier(data));

            return dict.TryGetValue(groupId.Value, out var set) && ChangeIdentifier(set);

            bool ChangeIdentifier(in GroupSettingData data)
            {
                if (!data.Options.Remove(oldIdentifier, out var oldValue))
                    return false;

                var newValue = data.Options.Remove(newIdentifier, out var currentValue)
                    ? Math.Max(currentValue, oldValue)
                    : oldValue;
                data.Options.Add(newIdentifier, newValue);
                return true;
            }
        }

        /// <summary> Get whether two settings dictionaries are exactly equal. </summary>
        public bool Equals(SettingsDictionary other)
        {
            if (dict.Count != other.Count)
                return false;

            foreach (var (key, leftValue) in dict)
            {
                if (!other.TryGetValue(key, out var rightValue))
                    return false;

                if (!leftValue.Equals(rightValue))
                    return false;
            }

            return true;
        }

        /// <summary> Write this dictionary as a JSON array of objects. </summary>
        public void WriteJson(Utf8JsonWriter writer)
        {
            writer.WriteStartArray();
            foreach (var (group, data) in dict)
            {
                writer.WriteStartObject();
                group.AddToJson(writer);
                data.AddToJson(writer);
                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        /// <summary> Populate this dictionary with values parsed from the current element of the JSON reader. </summary>
        /// <exception cref="JsonException"> When invalid JSON or invalid value types are encountered. </exception>
        /// <remarks> The reader should be positioned at a value element that is either a null-token or a start-array token. </remarks>
        public void ReadJson(ref Utf8JsonReader j)
        {
            dict.Clear();
            if (j.TokenType is JsonTokenType.Null)
                return;

            if (j.TokenType is not JsonTokenType.StartArray)
                throw new JsonException("Invalid JSON: SettingDictionary needs to be an array.");

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

                var     limit2          = j.CurrentDepth;
                Guid?   groupIdentifier = null;
                string? groupName       = null;
                var     groupData       = GroupSettingData.Create();
                while (j.Read())
                {
                    if (j.TokenType is JsonTokenType.EndObject && j.CurrentDepth == limit2)
                        break;

                    // This should not be able to happen?
                    if (j.CurrentDepth < limit2)
                        throw new JsonException("Invalid JSON: Left object depth without ending it.");

                    if (j.TokenType is not JsonTokenType.PropertyName)
                        continue;

                    if (!ModObjectIdentifier.ReadJsonProperties(ref j, ref groupIdentifier, ref groupName)
                     && !groupData.ReadJson(ref j))
                        j.Skip();
                }

                var id = new ModObjectIdentifier(groupIdentifier ?? Guid.Empty, groupName);
                if (!id.IsEmpty)
                    dict.TryAdd(id, groupData);
            }
        }
    }
}
