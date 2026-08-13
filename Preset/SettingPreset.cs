using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> A full persisted setting preset with unique identifier, name and usage timestamps. </summary>
public class SettingPreset
{
    /// <summary> The current version of preset data. </summary>
    public const int CurrentVersion = 1;

    /// <summary> A unique identifier for this persisted preset. </summary>
    public Guid Identifier { get; init; } = Guid.NewGuid();

    /// <summary> A display name for this persisted preset. </summary>
    public string Name = "Preset";

    /// <summary> The date and time of the last modification made to this preset. </summary>
    public DateTimeOffset LastEdit = DateTimeOffset.UtcNow;

    /// <summary> The date and time of the last use of this preset. </summary>
    public DateTimeOffset LastApplication = DateTimeOffset.UtcNow;

    /// <summary> The actual data for the preset. </summary>
    public SettingPresetData Data = SettingPresetData.Create();

    /// <summary> Create a new, empty preset. </summary>
    public SettingPreset()
    { }

    /// <summary> Create a new preset with a given identifier and data. </summary>
    /// <param name="identifier"> The identifier to use. </param>
    /// <param name="data"> The data for the preset. </param>
    public SettingPreset(Guid identifier, in SettingPresetData data)
    {
        Identifier = identifier;
        Data       = data;
    }

    /// <summary> Create a deep-copy clone of this preset. </summary>
    public SettingPreset(SettingPreset clone, Guid? newIdentifier, string? newName)
        : this(newIdentifier ?? clone.Identifier, clone.Data.Clone())
    {
        Name            = newName ?? clone.Name;
        LastEdit        = clone.LastEdit;
        LastApplication = clone.LastApplication;
    }

    /// <summary> Parse a persisted preset from the current JSON element. </summary>
    /// <param name="j"> The JSON reader. </param>
    /// <returns> The preset if it was parsed successfully, null otherwise. </returns>
    /// <exception cref="JsonException"> If invalid JSON or invalid value types are encountered, or if the preset has no identifier set. </exception>
    public static SettingPreset? ParseJson(ref Utf8JsonReader j)
    {
        Guid?   identifier      = null;
        string? name            = null;
        var     lastEdit        = DateTimeOffset.UtcNow;
        var     lastApplication = DateTimeOffset.UtcNow;
        var     data            = SettingPresetData.Create();
        if (j.TokenType is not JsonTokenType.StartObject)
            return null;

        var limit = j.CurrentDepth;
        while (j.Read())
        {
            if (j.TokenType is JsonTokenType.EndObject && j.CurrentDepth == limit)
                break;

            // This should not be able to happen?
            if (j.CurrentDepth < limit)
                throw new JsonException("Invalid JSON: Left object depth without ending it.");

            if (j.TokenType is not JsonTokenType.PropertyName)
                continue;

            if (j.ValueTextEquals("Identifier"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.String || !j.TryGetGuid(out var id))
                    throw new JsonException("Setting Preset Identifier has to be a GUID.");

                identifier = id;
            }
            else if (j.ValueTextEquals("Name"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.String and not JsonTokenType.Null)
                    throw new JsonException("Setting Preset Name has to be a string or null.");

                name = j.GetString();
            }
            else if (j.ValueTextEquals("LastEdit"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.Number and not JsonTokenType.Null)
                    throw new JsonException("Setting Preset Last Edit Timestamp has to be a number or null.");

                if (j.TokenType is JsonTokenType.Number && j.TryGetInt64(out var stamp))
                    lastEdit = DateTimeOffset.FromUnixTimeMilliseconds(stamp);
            }
            else if (j.ValueTextEquals("LastApplication"u8))
            {
                if (!j.Read() || j.TokenType is not JsonTokenType.Number and not JsonTokenType.Null)
                    throw new JsonException("Setting Preset Last Application Timestamp has to be a number or null.");

                if (j.TokenType is JsonTokenType.Number && j.TryGetInt64(out var stamp))
                    lastApplication = DateTimeOffset.FromUnixTimeMilliseconds(stamp);
            }
            else if (!data.ParseJsonProperties(ref j))
            {
                j.Skip();
            }
        }

        if (identifier is null)
            throw new JsonException("No identifier provided for setting preset.");

        return new SettingPreset(identifier.Value, data)
        {
            Name            = name ?? "Preset",
            LastEdit        = lastEdit,
            LastApplication = lastApplication,
        };
    }

    /// <summary> Write a persisted preset to JSON. </summary>
    public void WriteJson(Utf8JsonWriter writer)
    {
        writer.WriteStartObject();
        writer.WriteString("Identifier"u8, Identifier);
        writer.WriteNumber("LastEdit"u8,        LastEdit.ToUnixTimeMilliseconds());
        writer.WriteNumber("LastApplication"u8, LastApplication.ToUnixTimeMilliseconds());
        if (Name.Length > 0)
            writer.WriteString("Name"u8, Name);

        Data.WriteJsonProperties(writer);
        writer.WriteEndObject();
    }

    /// <summary> Update the display name of a preset. </summary>
    /// <param name="newName"> The new name. </param>
    /// <returns> Whether anything changed. </returns>
    public bool UpdateName(string newName)
    {
        if (Name == newName)
            return false;

        Name     = newName;
        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.SetPriority"/>
    public bool SetPriority(int? newPriority, bool force)
    {
        if (!Data.SetPriority(newPriority, force))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.SetState"/>
    public bool SetState(ModState state, bool force)
    {
        if (!Data.SetState(state, force))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.AddGroup"/>
    public bool AddGroup(ModObjectIdentifier group)
    {
        if (!Data.AddGroup(group))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.SetOption"/>
    public bool SetOption(ModObjectIdentifier group, ModObjectIdentifier option, OptionState state, bool force)
    {
        if (!Data.SetOption(group, option, state, force))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.SetDisableUnknownOptions"/>
    public bool SetDisableUnknownOptions(ModObjectIdentifier group, bool disableUnknownOptions)
    {
        if (!Data.SetDisableUnknownOptions(group, disableUnknownOptions))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingPresetExtensions.Update"/>
    public bool Update(in SettingPresetData update, bool force)
    {
        if (!Data.Update(update, force))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingsDictionaryExtensions.ReplaceGroupIdentifier"/>
    public bool ReplaceGroupIdentifier(ModObjectIdentifier oldIdentifier, ModObjectIdentifier newIdentifier)
    {
        if (!Data.Settings.ReplaceGroupIdentifier(oldIdentifier, newIdentifier))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingsDictionaryExtensions.ReplaceOptionIdentifiers"/>
    public bool ReplaceOptionIdentifiers(ModObjectIdentifier oldIdentifier, ModObjectIdentifier newIdentifier, ModObjectIdentifier? group)
    {
        if (!Data.Settings.ReplaceOptionIdentifiers(oldIdentifier, newIdentifier, group))
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }

    /// <inheritdoc cref="SettingsDictionaryExtensions.MakeGeneric"/>
    public bool MakeGeneric()
    {
        if (!Data.Settings.MakeGeneric())
            return false;

        LastEdit = DateTimeOffset.UtcNow;
        return true;
    }
}
