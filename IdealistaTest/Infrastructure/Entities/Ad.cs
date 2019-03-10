using System.Collections.Generic;

namespace IdealistaTest.Infrastructure.Entities
{
    public class Ad
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Typology { get; set; }
        public int HouseSize { get; set; }
        public int GardenSize { get; set; }
        public IEnumerable<int> Pictures { get; set; }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Ad)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}