using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string role  { get; set; }
        public byte[] passwordHash { get; set; }
        public byte[] passwordKey { get; set; }
    }
}