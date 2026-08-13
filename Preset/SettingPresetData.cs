global using SettingPresetData = (System.Collections.Generic.Dictionary<(System.Guid Identifier, string? Name),
        (System.Collections.Generic.Dictionary<(System.Guid Identifier, string? Name), byte> Options, bool DisableAllUnknown)> Settings, int
    _priority, short Version, bool _hasPriority, byte _state);
using System.Linq;
using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> Methods to use on the data for a setting preset. </summary>
public static class SettingPresetExtensions
{
    /// <summary> Readonly methods to use on the data for a setting preset. </summary>
    extension(ref readonly SettingPresetData data)
    {
        /// <summary> Whether a preset is valid. </summary>
        public bool Valid
            => data.Settings is not null;

        /// <summary> Get the mod state of this preset. </summary>
        public ModState State
            => (ModState)data._state;

        /// <summary> Get the priority of this preset. </summary>
        public int? Priority
            => data._hasPriority ? data._priority : null;

        /// <summary> Create new, empty data for a setting preset. </summary>
        public static SettingPresetData Create()
            => new(new SettingsDictionary(ModObjectIdentifierExtensions.Comparer.Instance), 0, SettingPreset.CurrentVersion, false,
                (byte)ModState.Ignored);

        /// <summary> Clone the setting preset data with a deep copy. </summary>
        public SettingPresetData Clone()
        {
            var ret = Create();
            if (!data.Valid)
                return ret;

            ret.Version = data.Version;
            ret._state = data._state;
            ret._hasPriority = data._hasPriority;
            ret._priority = data._priority;
            ret.Settings = data.Settings.ToDictionary(kvp => kvp.Key, kvp => (kvp.Value.Options.ToDictionary(), kvp.Value.DisableAllUnknown));
            return ret;
        }

        /// <summary> Add the properties of the preset data to the current JSON object without starting or ending an object. </summary>
        public void WriteJsonProperties(Utf8JsonWriter writer)
        {
            if (!data.Valid)
                throw new Exception("Invalid setting preset data can not be written.");

            writer.WriteNumber("Version"u8, data.Version);
            if (data._state is not (byte)ModState.Ignored)
                writer.WriteString("State"u8, data._state switch
                {
                    (byte)ModState.Disabled  => "Disabled"u8,
                    (byte)ModState.Enabled   => "Enabled"u8,
                    (byte)ModState.Inherited => "Inherited"u8,
                    (byte)ModState.Toggle    => "Toggle"u8,
                    _                        => "Ignored"u8,
                });
            if (data._hasPriority)
                writer.WriteNumber("Priority"u8, data._priority);
            if (data.Settings.Count > 0)
            {
                writer.WritePropertyName("Settings"u8);
                data.Settings.WriteJson(writer);
            }
        }
    }

    /// <summary> Write-access methods to use on the data for a setting preset. </summary>
    extension(ref SettingPresetData data)
    {
        /// <summary> Set the mod state to a new value. </summary>
        /// <param name="state"> The new priority. Use null to not set the priority. </param>
        /// <param name="force"> Whether to set a new priority even if it is currently set to ignore. </param>
        /// <returns> True if anything changed. </returns>
        public bool SetState(ModState state, bool force)
        {
            if (data.State is ModState.Ignored && !force)
                return false;

            if (state == data.State)
                return false;

            data._state = (byte)state;
            return true;
        }

        /// <summary> Set the priority to a new value. </summary>
        /// <param name="priority"> The new priority. Use null to not set the priority. </param>
        /// <param name="force"> Whether to set a new priority even if it is currently set to ignore. </param>
        /// <returns> True if anything changed. </returns>
        public bool SetPriority(int? priority, bool force)
        {
            if (!data._hasPriority && !force)
                return false;

            if (data._hasPriority)
            {
                if (priority is null)
                {
                    data._hasPriority = false;
                    data._priority    = 0;
                    return true;
                }

                if (priority.Value == data.Priority)
                    return false;

                data._priority = priority.Value;
                return true;
            }

            if (priority is null)
                return false;

            data._hasPriority = true;
            data._priority    = priority.Value;
            return true;
        }

        /// <summary> Add a new non-empty identifier to the preset to reference a group. </summary>
        /// <param name="group"> The identifier for the group reference. </param>
        /// <returns> True if the group was added. </returns>
        public bool AddGroup(ModObjectIdentifier group)
        {
            if (group.IsEmpty)
                return false;

            return data.Settings.TryAdd(group, GroupSettingData.Create());
        }

        /// <summary> Set an option in a group to a specific state. </summary>
        /// <param name="group"> The reference to the group. </param>
        /// <param name="option"> The reference to the option. </param>
        /// <param name="state"> The new state for the option. </param>
        /// <param name="force"> Whether to set previously ignored states. </param>
        /// <returns> True if anything changed. </returns>
        public bool SetOption(ModObjectIdentifier group, ModObjectIdentifier option, OptionState state, bool force)
        {
            if (!data.Valid)
                return false;

            if (!data.Settings.TryGetValue(group, out var groupData))
            {
                groupData = GroupSettingData.Create();
                data.Settings.Add(group, groupData);
                groupData.Options.Add(option, (byte)state);
                return true;
            }

            if (groupData.Options.TryAdd(option, (byte)state))
                return true;

            var currentValue = (OptionState)groupData.Options[option];
            if (currentValue == state || currentValue is OptionState.Ignored && !force)
                return false;

            groupData.Options[option] = (byte)state;
            return true;
        }

        /// <summary> Set a group to disable all unknown options. </summary>
        /// <param name="group"> The reference to the group. </param>
        /// <param name="disableUnknownOptions"> Whether the group should disable all unknown options. </param>
        /// <returns> True if anything changed. </returns>
        public bool SetDisableUnknownOptions(ModObjectIdentifier group, bool disableUnknownOptions)
        {
            if (!data.Valid)
                return false;

            if (data.Settings.Remove(group, out var groupData))
            {
                data.Settings[group] = groupData with { DisableAllUnknown = disableUnknownOptions };
                return groupData.DisableAllUnknown != disableUnknownOptions;
            }

            groupData                   = GroupSettingData.Create();
            groupData.DisableAllUnknown = disableUnknownOptions;
            data.Settings[group]        = groupData;
            return true;
        }

        /// <summary> Update the current preset with the data from another preset. </summary>
        /// <param name="update"> The data to update.</param>
        /// <param name="force">
        ///   When this is true, all data from <paramref name="update"/> will be applied to this preset.<br/>
        ///   When this is false, data ignored in this preset will be skipped during the update.
        /// </param>
        /// <returns> True if anything changed. </returns>
        /// <remarks> This does not delete existing groups or options that are not contained in <paramref name="update"/>. </remarks>
        public bool Update(in SettingPresetData update, bool force)
        {
            var ret = data.SetState(update.State, force);
            ret |= data.SetPriority(update.Priority, force);
            foreach (var (group, groupData) in update.Settings)
            {
                ret |= data.SetDisableUnknownOptions(group, groupData.DisableAllUnknown);
                foreach (var (option, value) in groupData.Options)
                    ret |= data.SetOption(group, option, (OptionState)value, force);
            }

            return ret;
        }

        /// <summary> Try to populate these preset data properties with parsed JSON properties. </summary>
        /// <param name="j"> The JSON reader. </param>
        /// <returns> True if the current property was one of the relevant properties and could be parsed, false otherwise. </returns>
        /// <exception cref="JsonException"> When invalid JSON or wrong value types are encountered. </exception>
        /// <remarks>
        ///   The reader should be positioned at a property name.
        ///   This only resets values for encountered properties.
        /// </remarks>
        public bool ParseJsonProperties(ref Utf8JsonReader j)
        {
            if (!data.Valid)
                return false;

            if (j.TokenType is not JsonTokenType.PropertyName)
                return false;

            if (j.ValueTextEquals("Version"u8))
            {
                if (!j.Read()
                 || j.TokenType is not JsonTokenType.Number
                 || !j.TryGetInt32(out var version)
                 || version is not SettingPreset.CurrentVersion)
                    throw new JsonException("Could not parse valid setting preset version.");

                return true;
            }

            if (j.ValueTextEquals("State"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.String)
                    throw new JsonException("Could not parse valid setting preset state.");

                data._state = j.GetString() switch
                {
                    "Enabled" or "enabled"     => (byte)ModState.Enabled,
                    "Disabled" or "disabled"   => (byte)ModState.Disabled,
                    "Inherited" or "inherited" => (byte)ModState.Inherited,
                    "Toggle" or "toggle"       => (byte)ModState.Toggle,
                    _                          => (byte)ModState.Ignored,
                };
                return true;
            }

            if (j.ValueTextEquals("Priority"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.Null and not JsonTokenType.Number)
                    throw new JsonException("Could not parse valid setting preset priority.");

                if (j.TokenType is JsonTokenType.Null)
                {
                    data._hasPriority = false;
                    data._priority    = 0;
                }
                else
                {
                    data._hasPriority = true;
                    data._priority    = j.TryGetInt32(out var p) ? p : 0;
                }

                return true;
            }

            if (j.ValueTextEquals("Settings"u8))
            {
                if (!j.Read())
                    throw new JsonException("Unexpected end after object property Settings.");

                data.Settings.ReadJson(ref j);
                return true;
            }

            return false;
        }
    }
}
