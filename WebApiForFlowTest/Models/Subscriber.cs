using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TRex.Metadata;

namespace WebApiForFlowTest.Models
    {
    public class Subscriber
        {
        [CallbackUrl]
        public string CallbackUrl { get; set; }

        public string EventSubbedTo { get; set; }

        public int TriggerId { get; set; }
        }
    }