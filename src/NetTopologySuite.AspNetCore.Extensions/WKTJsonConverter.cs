using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using NetTopologySuite.Geometries;

namespace NetTopologySuite.AspNetCore.Extensions
{
    public class WktJsonConverter<T> : JsonConverter<T> where T : Geometry
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            try
            {
                return SpatialDataConverter.WktToGeometry<T>(reader.GetString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            var wktJson = value == null ? "" : SpatialDataConverter.GeometryToWkt(value);
            writer.WriteStringValue(wktJson);
        }
    }

    public static class JsonConvertersExtension
    {
        public static IList<JsonConverter> AddWktJsonConverter(this IList<JsonConverter> converters)
        {
            Type wktJsonConverterType = typeof(WktJsonConverter<>);
            var geometriesAssembly = typeof(Point).Assembly;

            foreach (var item in geometriesAssembly.GetTypes().Where(x=>typeof(Geometry).IsAssignableFrom(x)))
            {
                converters.Add((JsonConverter)Activator.CreateInstance(wktJsonConverterType.MakeGenericType(item)));
            }
            return converters;
        }
    }
}
