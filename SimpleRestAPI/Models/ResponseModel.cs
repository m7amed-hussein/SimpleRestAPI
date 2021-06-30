using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleRestAPI.Models
{
    public class ResponseModel
    {
        public string FlowName { get; set; }
        public Facebookresponse FacebookResponse { get; set; }

    }

    public class Facebookresponse
    {
        public string messaging_type { get; set; }
        public Recipient recipient { get; set; }
        public Message message { get; set; }
    }

    public class Recipient
    {
        public string id { get; set; }
    }

    public class Message
    {
        public Attachment attachment { get; set; }
    }

    public class Attachment
    {
        public string type { get; set; }
        public Payload payload { get; set; }
    }

    public class Payload
    {
        public string template_type { get; set; }
        public Element[] elements { get; set; }
    }

    public class Element
    {
        public string title { get; set; }
        public string image_url { get; set; }
        public string subtitle { get; set; }
        public Default_Action default_action { get; set; }
        public Button[] buttons { get; set; }
    }

    public class Default_Action
    {
        public string type { get; set; }
        public string url { get; set; }
        public string webview_height_ratio { get; set; }
    }

    public class Button
    {
        public string type { get; set; }
        public string url { get; set; }
        public string title { get; set; }
    }

}
