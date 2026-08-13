global using ModObjectIdentifier = (System.Guid Identifier, string? Name);
using System.Diagnostics;
using System.Linq;
using System.Text.Json;

namespace Penumbra.Api.Preset;

/// <summary> Methods to use on the mod object identifier. </summary>
public static class ModObjectIdentifierExtensions
{
    /// <summary> Methods to use on the mod object identifier. </summary>
    extension(ref readonly ModObjectIdentifier identifier)
    {
        /// <summary> Whether the mod object identifier is empty and can not refer to a valid object. </summary>
        public bool IsEmpty
            => string.IsNullOrEmpty(identifier.Name) && identifier.Identifier == Guid.Empty;

        /// <summary> Find the best match for an identifier in a list of identifiers. </summary>
        /// <param name="list"> The list to search through. </param>
        /// <returns> The index of the best match.</returns>
        /// <remarks>
        ///   The best match is in order: <br/>
        ///     - The first identifier that matches in both GUID and name. <br/>
        ///     - The first identifier that matches in GUID and no name is provided for <paramref name="identifier"/>. <br/>
        ///     - The first identifier that matches in name and no GUID is provided for <paramref name="identifier"/>. <br/>
        ///     - The first identifier that matches only in GUID. <br/>
        ///     - The first identifier that matches only in name. 
        /// </remarks>
        public int BestMatch(IEnumerable<ModObjectIdentifier> list)
        {
            var bestMatchIndex = -1;
            var bestMatchType  = 0;
            foreach (var (index, rhs) in list.Index())
            {
                if (rhs.Identifier == identifier.Identifier)
                {
                    if (string.Equals(identifier.Name, rhs.Name, StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(identifier.Name))
                        return index;

                    if (bestMatchType >= 2)
                        continue;

                    bestMatchType  = 2;
                    bestMatchIndex = index;
                }
                else if (string.Equals(identifier.Name, rhs.Name, StringComparison.OrdinalIgnoreCase))
                {
                    if (identifier.Identifier == Guid.Empty)
                        return index;

                    if (bestMatchType >= 1)
                        continue;

                    bestMatchType  = 1;
                    bestMatchIndex = index;
                }
            }

            return bestMatchIndex;
        }

        /// <inheritdoc cref="BestMatch(ref readonly ModObjectIdentifier,IEnumerable{ModObjectIdentifier})"/>
        public int BestMatch<T>(IEnumerable<T> list, Func<T, ModObjectIdentifier> selector)
            => identifier.BestMatch(list.Select(selector));

        /// <summary>
        ///   Whether this identifier matches the other one.
        ///   Two identifiers match if either their GUIDs are equal,
        ///   or if at least one identifier has no GUID set and their names are equal.
        /// </summary>
        /// <param name="other"> The identifier to check. </param>
        /// <returns> True if the identifiers match. </returns>
        public bool Matches(ModObjectIdentifier other)
        {
            if (identifier.Identifier == Guid.Empty)
            {
                if (identifier.Name is null)
                    return other.IsEmpty;

                return string.Equals(identifier.Name, other.Name, StringComparison.OrdinalIgnoreCase);
            }

            if (identifier.Identifier == other.Identifier)
                return true;
            if (other.Identifier != Guid.Empty)
                return false;

            return string.Equals(identifier.Name, other.Name, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary> Write this identifier as an object to JSON. </summary>
        public void WriteJson(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            identifier.AddToJson(writer);
            writer.WriteEndObject();
        }

        /// <summary> Write the properties of this identifier to JSON without opening and closing an object. </summary>
        public void AddToJson(Utf8JsonWriter writer)
        {
            if (identifier.Identifier != Guid.Empty)
                writer.WriteString("Identifier"u8, identifier.Identifier);
            if (!string.IsNullOrEmpty(identifier.Name))
                writer.WriteString("Name"u8, identifier.Name);
        }


        /// <summary> Try to read the properties belonging to an identifier from the current JSON token. </summary>
        /// <param name="reader"> The JSON reader. </param>
        /// <param name="guid"> The current GUID, if any is set. This may be changed to a parsed property when returning true. </param>
        /// <param name="name"> The current name, if any is set. This may be changed to a parsed property when returning true. </param>
        /// <returns> True if the current property is one of the identifier properties, false if it is another property. </returns>
        /// <exception cref="JsonException"> When invalid JSON or value types are encountered. </exception>
        /// <remarks> Only call this on a property name token. </remarks>
        public static bool ReadJsonProperties(ref Utf8JsonReader reader, ref Guid? guid, ref string? name)
        {
            Debug.Assert(reader.TokenType is JsonTokenType.PropertyName);
            if (reader.ValueTextEquals("Identifier"u8))
            {
                if (!reader.Read())
                    throw new JsonException("Unexpected end after GUID property Identifier.");

                if (reader.TryGetGuid(out var g))
                {
                    guid = g;
                    return true;
                }

                throw new JsonException(
                    $"Unexpected {reader.TokenType} value for string property Identifier.");
            }

            if (reader.ValueTextEquals("Name"u8))
            {
                if (!reader.Read())
                    throw new JsonException("Unexpected end after string property Name.");

                if (reader.TokenType is JsonTokenType.Null)
                {
                    name = null;
                    return true;
                }

                if (reader.TokenType is JsonTokenType.String)
                {
                    name = reader.GetString();
                    return name is not null;
                }

                throw new JsonException(
                    $"Unexpected {reader.TokenType} value for string property Name.");
            }

            return false;
        }
    }

    /// <summary> A comparer so that only one identifier per GUID can be added to a dictionary, regardless of chosen name. </summary>
    internal sealed class Comparer : IEqualityComparer<ModObjectIdentifier>
    {
        public static readonly Comparer Instance = new();

        public bool Equals(ModObjectIdentifier x, ModObjectIdentifier y)
        {
            if (x.Identifier == y.Identifier && x.Identifier != Guid.Empty)
                return true;

            return string.Equals(x.Name, y.Name, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(ModObjectIdentifier obj)
        {
            if (obj.Identifier != Guid.Empty)
                return obj.Identifier.GetHashCode();

            return obj.Name?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;
        }
    }
}
