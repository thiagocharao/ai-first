namespace AIFirst.Core.Schema;

/// <summary>
/// Maps JSON Schema types to C# types.
/// </summary>
public static class CSharpTypeMapper
{
    /// <summary>
    /// Maps a JSON Schema to a C# type name.
    /// </summary>
    /// <param name="schema">The JSON Schema.</param>
    /// <param name="propertyName">Optional property name for generating nested type names.</param>
    /// <returns>The C# type name.</returns>
    public static string MapToCSharpType(JsonSchema schema, string? propertyName = null)
    {
        var baseType = MapBaseType(schema, propertyName);
        
        // Add nullability suffix if nullable and not already nullable
        if (schema.Nullable && !baseType.EndsWith("?") && !IsReferenceType(baseType))
        {
            return baseType + "?";
        }

        return baseType;
    }

    private static string MapBaseType(JsonSchema schema, string? propertyName)
    {
        // Handle $ref
        if (!string.IsNullOrEmpty(schema.Ref))
        {
            return ExtractRefTypeName(schema.Ref);
        }

        return schema.Type.ToLowerInvariant() switch
        {
            "string" => MapStringType(schema),
            "integer" => "int",
            "number" => "double",
            "boolean" => "bool",
            "array" => MapArrayType(schema, propertyName),
            "object" => MapObjectType(schema, propertyName),
            "null" => "object?",
            _ => "object"
        };
    }

    private static string MapStringType(JsonSchema schema)
    {
        // Check for format hints
        if (!string.IsNullOrEmpty(schema.Format))
        {
            return schema.Format.ToLowerInvariant() switch
            {
                "date-time" => "DateTimeOffset",
                "date" => "DateOnly",
                "time" => "TimeOnly",
                "uri" => "Uri",
                "uuid" => "Guid",
                "guid" => "Guid",
                _ => "string"
            };
        }

        // Check for enum
        if (schema.Enum != null && schema.Enum.Count > 0)
        {
            return "string"; // Could generate enum type in future
        }

        return "string";
    }

    private static string MapArrayType(JsonSchema schema, string? propertyName)
    {
        if (schema.Items != null)
        {
            var itemType = MapToCSharpType(schema.Items, propertyName != null ? $"{propertyName}Item" : null);
            return $"IReadOnlyList<{itemType}>";
        }

        return "IReadOnlyList<object>";
    }

    private static string MapObjectType(JsonSchema schema, string? propertyName)
    {
        // If it has a title, use that as the type name
        if (!string.IsNullOrEmpty(schema.Title))
        {
            return ToPascalCase(schema.Title);
        }

        // If it has properties, it's a complex type that needs to be generated
        if (schema.Properties.Count > 0 && !string.IsNullOrEmpty(propertyName))
        {
            return ToPascalCase(propertyName);
        }

        // Default to dictionary for untyped objects
        return "IReadOnlyDictionary<string, object>";
    }

    private static string ExtractRefTypeName(string refPath)
    {
        // Handle $ref like "#/definitions/MyType" or "#/$defs/MyType"
        var parts = refPath.Split('/');
        if (parts.Length > 0)
        {
            return ToPascalCase(parts[^1]);
        }
        return "object";
    }

    private static bool IsReferenceType(string typeName)
    {
        return typeName switch
        {
            "string" => true,
            "object" => true,
            "object?" => true,
            _ when typeName.StartsWith("IReadOnly") => true,
            _ when typeName.StartsWith("Uri") => true,
            _ => false
        };
    }

    /// <summary>
    /// Converts a string to PascalCase.
    /// </summary>
    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var result = new System.Text.StringBuilder();
        var capitalizeNext = true;

        foreach (var c in input)
        {
            if (c == '_' || c == '-' || c == ' ')
            {
                capitalizeNext = true;
                continue;
            }

            if (capitalizeNext)
            {
                result.Append(char.ToUpperInvariant(c));
                capitalizeNext = false;
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

    /// <summary>
    /// Converts a string to camelCase.
    /// </summary>
    public static string ToCamelCase(string input)
    {
        var pascal = ToPascalCase(input);
        if (string.IsNullOrEmpty(pascal))
            return pascal;

        return char.ToLowerInvariant(pascal[0]) + pascal.Substring(1);
    }
}
