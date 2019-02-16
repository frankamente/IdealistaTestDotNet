using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdealistaTest.Infrastructure.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int HouseSize { get; set; }
        public int GardenSize { get; set; }
        public List<int> Pictures { get; set; }
    }
}