using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindredPOC.API.Models
{
    public class Item
    {
        [Key]
        [Newtonsoft.Json.JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }

    }
}
 