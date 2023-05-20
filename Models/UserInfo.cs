using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASHWAR.Models
{
    public class UserInfo
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Age { get; set; }

        public string gender { get; set; }

        public string city { get; set; }

        public string Personality { get; set; }
        public bool  introvert { get; set; }
        public string  message { get; set; }

        public string lat { get; set; }
        public string longt { get; set; }
    }
}
