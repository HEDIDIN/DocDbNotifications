using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;
using static System.Globalization.CultureInfo;

namespace DocDBNotificationApi.Controllers
{
    public class AuthorizationController : ApiController
    {
        /// <summary>
        ///     If you are using the internal Resource Ids (or _rids)
        ///     of a resource then switch this to false
        /// </summary>
        /// <returns>URI Encoded string</returns>
        [HttpPost]
        [Metadata("GenerateAuthToken", "Gets the authorization token for a DcoumentDB REST API method.")]
        [SwaggerOperation("GetAuthToken")]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof (string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal Server Operation Error")]
        public string GetAuthToken(
            [Metadata("verb", "POST, GET, Etc.")] string verb,
            [Metadata("Current DateTime", "UtcNow.ToString('r')")] string currentDateTime)

        {
            const string authorizationFormat = "type={0}&ver={1}&sig={2}";

            var key = DocumentDbContext.AuthKey;
            var keyType = DocumentDbContext.KeyType;
            var tokenVersion = DocumentDbContext.TokenVersion;
            var databaseId = DocumentDbContext.DatabaseId;
            var collectionId = DocumentDbContext.CollectionId;

            var hmacSha256 = new HMACSHA256 {Key = Convert.FromBase64String(key)};


            var resourceId = $"/dbs/{databaseId}/colls/{collectionId}";


            const string resourceType = "docs";


            var payLoad = string.Format(InvariantCulture,
                "{0}\n{1}\n{2}\n{3}\n{4}\n",
                verb.ToLowerInvariant(),
                resourceType.ToLowerInvariant(),
                resourceId,
                currentDateTime.ToLowerInvariant(),
                ""
                );

            var hashPayLoad = hmacSha256.ComputeHash(Encoding.UTF8.GetBytes(payLoad));
            var signature = Convert.ToBase64String(hashPayLoad);

            return
                HttpUtility.UrlEncode(string.Format(InvariantCulture,
                    authorizationFormat,
                    keyType,
                    tokenVersion,
                    signature));
        }


        /// <summary>
        ///     Gets the current UTC Date value
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Metadata("GetUtcDate", "Gets the current UTC Date value minus the Hours Back")]
        [SwaggerOperation("GetUtcDate")]
        [SwaggerResponse(HttpStatusCode.OK, type: typeof (string))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "Internal Server Operation Error")]
        public string GetUtcDate(
            [Metadata("Hours Back", "How many hours back from the current Date Time")] int hoursBack)
        {
            return DateTime.UtcNow.AddHours(-hoursBack).ToString("r");
        }
    }
}