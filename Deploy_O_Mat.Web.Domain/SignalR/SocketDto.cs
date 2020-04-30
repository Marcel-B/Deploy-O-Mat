using System.Collections.Generic;

namespace Deploy_O_Mat.Web.Domain.SignalR
{
    public class TransferData
    {
          public string Id { get; set; }
        public string Service { get; set; }
        public string Image { get; set; }
        public string Replicas { get; set; }
    }

    public class SocketDto
    {
        public SocketDto()
        {
            Values = new List<TransferData>();
        }
        public List<TransferData> Values {get;set;}
    }
}