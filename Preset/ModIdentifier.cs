global using ModIdentifier = (string Identifier, string Name);
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> Methods to use on a mod identifier. </summary>
public static class ModIdentifierExtensions
{
    /// <summary> An empty mod identifier. </summary>
    private static readonly ModIdentifier EmptyIdentifier = new(string.Empty, string.Empty);

    /// <summary> Readonly Methods to use on the mod identifier. </summary>
    extension(in ModIdentifier id)
    {
        /// <summary> Compare two mod identifiers. </summary>
        /// <param name="other"> The other identifier. </param>
        /// <returns> Lexicographical comparison of the names if they differ, otherwise lexicographical comparison of the identifiers if they differ. </returns>
        public int CompareTo(ModIdentifier other)
        {
            var nameComparison = string.Compare(id.Name, other.Name, StringComparison.Ordinal);
            if (nameComparison is not 0)
                return nameComparison;

            return string.Compare(id.Identifier, other.Identifier, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary> Get whether the identifier is empty. </summary>
        public bool IsEmpty
            => id.Identifier.Length is 0 && id.Name.Length is 0;

        /// <summary> An empty mod identifier. </summary>
        public static ModIdentifier Empty
            => EmptyIdentifier;

        /// <summary> Create an identifier that only contains the mod identifier (directory name). </summary>
        /// <param name="directory"> The directory name. </param>
        /// <returns> The identifier. </returns>
        public static ModIdentifier Directory(string directory)
            => new(directory, string.Empty);

        /// <summary> Create an identifier that only contains the mod name. </summary>
        /// <param name="name"> The name. </param>
        /// <returns> The identifier. </returns>
        public static ModIdentifier Name(string name)
            => new(string.Empty, name);

        /// <summary> Find the best match for a mod identifier in a list of items. </summary>
        /// <typeparam name="T"> The type of item. </typeparam>
        /// <param name="list"> The list of items. </param>
        /// <param name="identifierSelector"> A mapping from the item to the mod identifier. </param>
        /// <param name="nameSelector"> A mapping from the item to the mod name. </param>
        /// <returns> The index of the matching item or -1 if none matched. </returns>
        public int FindBestMatch<T>(IEnumerable<T> list, Func<T, string> identifierSelector, Func<T, string> nameSelector)
        {
            if (id.IsEmpty)
                return -1;

            var firstNameMatch = id.Name.Length > 0 ? -2 : -1;


            foreach (var (idx, item) in list.Index())
            {
                var itemIdentifier = identifierSelector(item);
                if (string.Equals(id.Identifier, itemIdentifier, StringComparison.OrdinalIgnoreCase))
                    return idx;

                if (firstNameMatch is not -2)
                    continue;

                var itemName = nameSelector(item);
                if (itemName == id.Name)
                    firstNameMatch = idx;
            }

            return firstNameMatch is -2 ? -1 : firstNameMatch;
        }

        /// <summary> Write the properties of this identifier to the JSON writer without starting or ending an object. </summary>
        /// <param name="j"> The writer. </param>
        public void WriteJsonProperties(Utf8JsonWriter j)
        {
            if (id.Identifier.Length > 0)
                j.WriteString("ModIdentifier"u8, id.Identifier);
            if (id.Name.Length > 0)
                j.WriteString("ModName"u8, id.Name);
        }
    }

    /// <summary> Write-access Methods to use on the mod identifier. </summary>
    extension(ref ModIdentifier id)
    {
        /// <summary> Try to read the properties belonging to an identifier from the current JSON token. </summary>
        /// <param name="j"> The JSON reader. </param>
        /// <returns> True if the current property is one of the identifier properties and got parsed, false if it is another property. </returns>
        /// <exception cref="JsonException"> When invalid JSON or value types are encountered. </exception>
        /// <remarks> Only call this on a property name token. </remarks>
        public bool TryReadJsonProperties(ref Utf8JsonReader j)
        {
            Debug.Assert(j.TokenType is JsonTokenType.PropertyName);
            if (j.ValueTextEquals("ModIdentifier"u8))
            {
                if (!j.Read())
                    throw new JsonException("Unexpected end after string property ModIdentifier.");

                if (j.TokenType is JsonTokenType.Null)
                {
                    id.Identifier = string.Empty;
                    return true;
                }

                if (j.TokenType is not JsonTokenType.String)
                    throw new JsonException(
                        $"Unexpected {j.TokenType} value for string property ModIdentifier.");

                id.Identifier = j.GetString() ?? string.Empty;
                return true;
            }

            if (j.ValueTextEquals("ModName"u8))
            {
                if (!j.Read())
                    throw new JsonException("Unexpected end after string property ModName.");

                if (j.TokenType is JsonTokenType.Null)
                {
                    id.Name = string.Empty;
                    return true;
                }

                if (j.TokenType is not JsonTokenType.String)
                    throw new JsonException(
                        $"Unexpected {j.TokenType} value for string property ModName.");

                id.Name = j.GetString() ?? string.Empty;
                return true;
            }

            return false;
        }
    }
}
