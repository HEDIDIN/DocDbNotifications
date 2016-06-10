using System;
using System.Net;
using System.Web;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;

namespace DocDBNotificationApi.Controllers
{
    public class ConversionController : ApiController
    {
        /// <summary>
        ///     Converts DateTime to double
        /// </summary>
        /// <param name="currentdateTime"></param>
        /// <returns></returns>
        [Metadata("Converts Universal DateTime to number")]
        [SwaggerResponse(HttpStatusCode.OK, null, typeof (double))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "DateTime is invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerOperation(nameof(ConvertToTimestamp))]
        public double ConvertToTimestamp(
            [Metadata("currentdateTime", "DateTime value to convert")] string currentdateTime)
        {
            double result;

            try
            {
                var uncoded = HttpContext.Current.Server.UrlDecode(currentdateTime);

                var newDateTime = DateTime.Parse(uncoded);
                //create Timespan by subtracting the value provided from the Unix Epoch
                var span = newDateTime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();

                //return the total seconds (which is a UNIX timestamp)
                result = span.TotalSeconds;
            }
            catch (Exception e)
            {
                throw new Exception("unable to convert to Timestamp", e.InnerException);
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stringToEncode"></param>
        /// <returns></returns>
        [HttpGet]
        [Metadata("Url Encodes a string")]
        [SwaggerResponse(HttpStatusCode.OK, null, typeof (double))]
        [SwaggerResponse(HttpStatusCode.BadRequest, "DateTime is invalid")]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        [SwaggerOperation(nameof(UrlEncode))]
        public string UrlEncode(
            [Metadata("stringToEncode", "String to be encoded")] string stringToEncode)
        {
            return HttpContext.Current.Server.UrlEncode(stringToEncode);
        }
    }
}