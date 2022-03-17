using NetTopologySuite.Geometries;
using System.Text.Json;
using Xunit;

namespace NetTopologySuite.AspNetCore.Extensions.Test
{
    public class WktJsonDeserializeTest
    {
        static readonly JsonSerializerOptions jsonSerializerOptions = new();
        static int once = 0;

        public WktJsonDeserializeTest()
        {
            if (once == 0)
            {
                jsonSerializerOptions.Converters.AddWktJsonConverter();
            }

            once = 1;
        }

        [Fact]
        public void ConvertWkt2Point()
        {
            var wkt = "\"POINT (1 2)\"";

            var result = JsonSerializer.Deserialize<Point>(wkt, jsonSerializerOptions);

            Assert.Equal(result, new Point(1, 2));
        }

        [Fact]
        public void ConvertBadWkt2Null()
        {
            var wkt = "\"a bad wkt\"";

            var result = JsonSerializer.Deserialize<Point>(wkt, jsonSerializerOptions);

            Assert.Null(result);
        }
    }
}
