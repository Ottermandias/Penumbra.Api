namespace Penumbra.Api.Api;

public partial interface IPenumbraApi
{
    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the collection currently associated with the player.</returns>
    public string GetPlayerMetaManipulations();

    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the given collection associated with the character name, or the default collection.</returns>
    public string GetMetaManipulations(string characterName);

    /// <returns>A base64 encoded, zipped json-string with a prepended version-byte of the current manipulations
    /// in the given collection applying to the given game object or the default collection if it does not exist.</returns>
    public string GetGameObjectMetaManipulations(int gameObjectIdx);
}
