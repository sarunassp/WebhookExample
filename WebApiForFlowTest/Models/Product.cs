using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiForFlowTest.Models
    {
    public class Product
        {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Category { get; set; }
        
        public double Price { get; set; }
        }
    }