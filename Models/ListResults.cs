using Microsoft.Azure.Documents;

namespace DocDBNotificationApi.Models
{
    public class ListResults
    {
        public Document[] Result { get; set; }
        public int? Continuation { get; set; }
    }
}