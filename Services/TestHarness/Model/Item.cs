using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestHarness.Models
{
    public class Item
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

    }
}
 