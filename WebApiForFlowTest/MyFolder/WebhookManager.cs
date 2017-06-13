using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using Newtonsoft.Json;
using WebApiForFlowTest.Models;

namespace WebApiForFlowTest.MyFolder
    {
    public class WebhookManager
        {
        public static List<Subscriber> subscriptions = new List<Subscriber>();

        public static void TriggerEvent (string eventTriggered, Product product)
            {
            var subscribersToSend = subscriptions.Where(item => item.EventSubbedTo == eventTriggered);

            using ( HttpClient client = new HttpClient() )
                {
                foreach ( var sub in subscriptions )
                    {
                    var s = client.PostAsync(sub.CallbackUrl, new StringContent(JsonConvert.SerializeObject(product).ToString(),
                                    Encoding.UTF8,
                                    "application/json")).Result;
                    }
                }
            }

        public static void Subscribe (Subscriber subscriber)
            {
            subscriptions.Add(subscriber);
            }

        public static void Unsubscribe (int triggerId)
            {
            subscriptions.Remove(subscriptions.Where(item => item.TriggerId == triggerId).FirstOrDefault());
            }
        }
    }