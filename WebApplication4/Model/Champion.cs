using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Model
{
    public class Champion
    {
        public int? id { get; set; }

        public string? name { get; set; }
        public string? picture { get; set; }
        public string? tier { get; set; }
        public string? description { get; set; }
    }
}
