using Microsoft.AspNetCore.Mvc;
using SimpleRestAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SimpleRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SimpleController : ControllerBase
    {
        
        public Rootobject rootobject;
        
        private readonly IHttpClientFactory _httpClientFactory;
        public SimpleController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<ResponseModel> OnGet(int pagenumber)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://reqres.in/api/users?" + "page=" + pagenumber);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                rootobject = await JsonSerializer.DeserializeAsync
                    <Rootobject>(responseStream);


                if (pagenumber > rootobject.total_pages)
                {
                    throw new Exception("There is no page number"+pagenumber);
                }
                    var responseModel = new ResponseModel()
                    {
                        FlowName = "New Flow",
                    FacebookResponse = new Facebookresponse()
                    {
                        messaging_type = "RESPONSE",
                        recipient = new Recipient()
                        {
                            id = "{psid}"
                        },
                        message = new Message()
                        {
                            attachment = new Attachment()
                            {
                                type = "template",
                                payload = new Payload()
                                {
                                    template_type = "generic",
                                    
                                   
                                    
                                }
                            }

                        }

                    }
                };

                List<Element> elementitems = new  List<Element>();
                foreach(var elementitem in rootobject.data)
                {
                        elementitems.Add(
                    new Element
                    {
                        title = elementitem.first_name,
                        image_url = elementitem.avatar,
                        subtitle = elementitem.last_name,
                        default_action = new Default_Action()
                        {
                            type = "web_url",
                            url = "mailto:+" + elementitem.email +
                                                "?Subject=Hello",
                            webview_height_ratio = "tall"
                        },
                        buttons = new Button[]
                                            {
                                                new Button()
                                                {
                                                    type = "web_url",
                                                    url = "mailto:" + elementitem.email+
                                                    "?Subject=Hello",
                                                    title="Send Email"
                                                }
                                            }

                    });
                }
                responseModel.FacebookResponse.message.attachment.payload.elements = elementitems.ToArray();
                return responseModel;
            }
            throw new Exception("API Failed");
            
        }
    }
}
