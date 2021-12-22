using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using NetTopologySuite.Geometries;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;

namespace NetTopologySuite.AspNetCore.Extensions.Swagger
{
    public class WKTSchemaFilter : ISchemaFilter
    {
        public static readonly Dictionary<Type, SchemaChangeType> Mapper = new Dictionary<Type, SchemaChangeType>
        {
            [typeof(Point)] = new SchemaChangeType("string","POINT (1 2)"),
            [typeof(MultiPoint)] = new SchemaChangeType("string", "MULTIPOINT (1 2, 1 3)"),
            [typeof(LinearRing)] = new SchemaChangeType("string", "LINEARRING (1 0, 1 1, 0 0, 1 0)"),
            [typeof(LineString)] = new SchemaChangeType("string", "LINESTRING (1 0, 1 1)"),
            [typeof(Polygon)] = new SchemaChangeType("string", "POLYGON ((1 0,1 1, 0 0 , 1 0))"),
            [typeof(MultiPolygon)] = new SchemaChangeType("string", "MULTIPOLYGON (((1 0,1 1, 0 0 , 1 0)))"),
            [typeof(Geometry)] = new SchemaChangeType("string", "POINT (1 2)"),
        };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (Mapper.TryGetValue(context.Type, out var value))
            {
                schema.Properties.Clear();
                schema.Type = value.Type;
                schema.Example = new OpenApiString(value.Value);
            }
        }
    }

    public class SchemaChangeType
    {
        public SchemaChangeType(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; set; }

        public string Value { get; set; }
    }
}