using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using Newtonsoft.Json;
using Swashbuckle.Swagger.Annotations;
using TRex.Metadata;
using WebApiForFlowTest.Models;
using WebApiForFlowTest.MyFolder;

namespace WebApiForFlowTest.Controllers
    {
    public class WebhookController : ApiController
        {
        [HttpPost]
        [Trigger(TriggerType.Subscription, typeof(Product), "Details for webhook")]
        [Metadata ("Subscribe to events")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [Route("api/webhooks/subscribe")]
        public IHttpActionResult Subscribe ([FromBody] Subscriber subscriber)
            {
            if ( subscriber.Equals(null) )
                return BadRequest();

            WebhookManager.Subscribe(subscriber);

            var response = new HttpResponseMessage(HttpStatusCode.Created);
            response.Headers.Add("Location", "http://36a038a8.ngrok.io/api/webhooks/unsubscribe/" + subscriber.TriggerId);
            return ResponseMessage(response);
            }

        [HttpGet]
        [Metadata("Get all subscribers")]
        [Route("api/subscribers")]
        public IEnumerable<Subscriber> GetSubscribers ()
            {
            return WebhookManager.subscriptions;
            }

        [HttpDelete]
        [Metadata("Unsubscribe from events")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [Route("api/webhooks/unsubscribe/{triggerId}")]
        public IHttpActionResult Unsubscribe ([FromUri]int triggerId)
            {
            if ( triggerId == 0 )
                return BadRequest();

            WebhookManager.Unsubscribe(triggerId);

            return Ok();
            }
        }
    }