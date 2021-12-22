using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

using NetTopologySuite.Features;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;

namespace NetTopologySuite.AspNetCore.Extensions 
{
    public static class SpatialDataConverter
    {
        private static readonly WKTReader _wktReader = new WKTReader();
        private static readonly WKTWriter _wktWriter = new WKTWriter();
        private static readonly FeatureCollectionConverter _featureCollectionConverter = new FeatureCollectionConverter();

        /// <summary>
        /// wtk 转 Geometry
        /// </summary>
        /// <param name="wkt"></param>
        /// <returns></returns>
        public static T WktToGeometry<T>(string wkt) where T : Geometry
        {
            if (string.IsNullOrEmpty(wkt))
                return null;

            return (T)_wktReader.Read(wkt);
        }

        /// <summary>
        /// wtk 转 Geometry
        /// </summary>
        /// <param name="wkt"></param>
        /// <returns></returns>
        public static Task<T> WktToGeometryAsync<T>(string wkt) where T : Geometry
        {
            if (string.IsNullOrEmpty(wkt))
                throw new ArgumentException($"{nameof(wkt)} is required");

            return Task.FromResult(WktToGeometry<T>(wkt));
        }

        /// <summary>
        /// Geometry 转 wkt
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static string GeometryToWkt(Geometry geometry)
        {
            if (geometry == null)
                throw new ArgumentException($"{nameof(geometry)} is required");

            return _wktWriter.Write(geometry);
        }

        /// <summary>
        /// Geometry 转 wkt
        /// </summary>
        /// <param name="geometry"></param>
        /// <returns></returns>
        public static Task<string> GeometryToWktAsync(Geometry geometry)
        {
            if (geometry == null)
                throw new ArgumentException($"{nameof(geometry)} is required");

            return Task.FromResult(GeometryToWkt(geometry));
        }

        /// <summary>
        /// features 转 GeoJson
        /// </summary>
        /// <param name="features"></param>
        /// <returns></returns>
        public static string FeaturesToGeoJson(FeatureCollection features)
        {
            if (features == null)
                throw new ArgumentNullException("features is required");

            if (features.Count < 1)
                return "{}";

            var ret = new StringBuilder();
            _featureCollectionConverter.WriteJson(
                new JsonTextWriter(new StringWriter(ret)),
                features,
                GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326), 3));

            return ret.ToString();
        }

        /// <summary>
        /// features 转 GeoJson
        /// </summary>
        /// <param name="features"></param>
        /// <returns></returns>
        public static Task<string> FeaturesToGeoJsonAsync(FeatureCollection features)
        {
            return Task.FromResult(FeaturesToGeoJson(features));
        }
    }
}