using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Symptoms
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int Age { get; set; }
        public string Gender  { get; set; }
    }
}