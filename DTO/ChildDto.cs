using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class ChildDto
    {
        public int age{ get; set; }
        public string name{ get; set; }
         public string gender{ get; set; }
        public int parent { get; set; }

    }
}