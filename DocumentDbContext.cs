using System;
using System.Configuration;
using Microsoft.Azure.Documents.Client;

namespace DocDBNotificationApi
{
    /// <summary>
    /// 
    /// </summary>
    public class DocumentDbContext
    {
        private DocumentClient _client;

        /// <summary>
        /// 
        /// </summary>
        public static string EndPoint = ConfigurationManager.AppSettings["EndPoint"];

        /// <summary>
        /// 
        /// </summary>
        public static string AuthKey = ConfigurationManager.AppSettings["AuthKey"];

        /// <summary>
        /// 
        /// </summary>
        public static string KeyType = ConfigurationManager.AppSettings["KeyType"];

        /// <summary>
        /// 
        /// </summary>
        public static string TokenVersion = ConfigurationManager.AppSettings["TokenVersion"];

        /// <summary>
        /// 
        /// </summary>
        public static string DatabaseId = ConfigurationManager.AppSettings["DatabaseId"];

        /// <summary>
        /// 
        /// </summary>
        public static string CollectionId = ConfigurationManager.AppSettings["CollectionId"];

        /// <summary>
        /// 
        /// </summary>
        public static string ProcedureId = ConfigurationManager.AppSettings["ProcedureId"];


        /// <summary>
        /// Create the connection 
        /// </summary>
        public DocumentClient Client
        {
            get
            {
                if (_client != null) return _client;
                var endpointUri = new Uri(EndPoint);
                _client = new DocumentClient(endpointUri, AuthKey, new ConnectionPolicy
                {
                    ConnectionMode = ConnectionMode.Direct,
                    ConnectionProtocol = Protocol.Tcp
                });

                return _client;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static string CollectionLink { get; set; }
    }
}