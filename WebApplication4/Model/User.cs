using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication4.Model
{
    public class User: Type
    {
        public int? id { get; set; }

        public string? UserName { get; set; } 
        public string? firstname { get; set; }
        public string? lastname { get; set; }
        public string? dob { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; } 
        public string? role { get; set; }
        public string? contactno { get; set; }
        public string? gender { get; set; }
    }
}
