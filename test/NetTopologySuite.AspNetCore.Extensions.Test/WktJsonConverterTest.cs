using NetTopologySuite.Geometries;
using System.Text.Json;
using Xunit;

namespace NetTopologySuite.AspNetCore.Extensions.Test
{
    public class WktJsonConverterTest
    {
        static readonly JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
        static int once = 0;

        public WktJsonConverterTest()
        {
            if (once == 0)
            {
                jsonSerializerOptions.Converters.AddWktJsonConverter();
            }

            once = 1;
        }

        [Fact]
        public void ConvertPoint2Wkt()
        {
            var point = new Point(1, 2);

            var result = JsonSerializer.Serialize(point, jsonSerializerOptions).Trim('\"');

            Assert.Equal("POINT (1 2)", result);
        }

        [Fact]
        public void ConvertMultiPoint2Wkt()
        {
            var multPoint = new MultiPoint(new[] { new Point(1, 2), new Point(1, 3) });

            var result = JsonSerializer.Serialize(multPoint, jsonSerializerOptions).Trim('\"');

            Assert.Equal("MULTIPOINT ((1 2), (1 3))", result);
        }

        [Fact]
        public void ConvertLineString2Wkt()
        {
            var line = new LineString(new[] { new Coordinate(1, 0), new Coordinate(1, 1) });

            var result = JsonSerializer.Serialize(line, jsonSerializerOptions).Trim('\"');

            Assert.Equal("LINESTRING (1 0, 1 1)", result);
        }

        [Fact]
        public void ConvertLinearRing2Wkt()
        {
            var linearRing = new LinearRing(new[] { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0) });

            var result = JsonSerializer.Serialize(linearRing, jsonSerializerOptions).Trim('\"');

            Assert.Equal("LINEARRING (1 0, 1 1, 0 0, 1 0)", result);
        }

        [Fact]
        public void ConvertMultiLineString2Wkt()
        {
            var multiLineString = new MultiLineString(new[] {
                new LineString(new[]{new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(0, 0) }),
                new LineString(new[]{new Coordinate(2, 3), new Coordinate(3, 5), new Coordinate(2, 4) }),
                });

            var result = JsonSerializer.Serialize(multiLineString, jsonSerializerOptions).Trim('\"');

            Assert.Equal("MULTILINESTRING ((1 0, 1 1, 0 0), (2 3, 3 5, 2 4))", result);
        }

        [Fact]
        public void ConvertPolygon2Wkt()
        {
            var polygon = new Polygon(new LinearRing(new[] { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0) }));

            var result = JsonSerializer.Serialize(polygon, jsonSerializerOptions).Trim('\"');

            Assert.Equal("POLYGON ((1 0, 1 1, 0 0, 1 0))", result);
        }

        [Fact]
        public void ConvertMultiPolyton2Wkt()
        {
            var polygon = new MultiPolygon(new[] {
                new Polygon(new LinearRing(new[] { new Coordinate(1, 0), new Coordinate(1, 1), new Coordinate(0, 0), new Coordinate(1, 0) })),
                new Polygon(new LinearRing(new[] { new Coordinate(2, 3), new Coordinate(3, 5), new Coordinate(2, 4), new Coordinate(2, 3) }))
                });

            var result = JsonSerializer.Serialize(polygon, jsonSerializerOptions).Trim('\"');

            Assert.Equal("MULTIPOLYGON (((1 0, 1 1, 0 0, 1 0)), ((2 3, 3 5, 2 4, 2 3)))", result);
        }
    }
}