using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserDto
    {
        public string username { get; set; }
        public string role { get; set; }
        public string Token { get; set; }

        public int id{get;set;}
    }
}