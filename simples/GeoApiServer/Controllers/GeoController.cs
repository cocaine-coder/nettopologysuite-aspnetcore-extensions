using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace GeoApiServer.Controllers
{
    [ApiController]
    [Route("api/geo")]
    public class GeoController : ControllerBase
    {

        [HttpPost("point")]
        public ActionResult<Point> AddPoint(Point point)
        {
            return Ok(point);
        }

        [HttpPost("Line")]
        public ActionResult<LineString> AddPoint(LineString line)
        {
            return Ok(line);
        }


        [HttpPost("polygon")]
        public ActionResult<Polygon> AddPoint(Polygon polygon)
        {
            return Ok(polygon);
        }
    }
}
