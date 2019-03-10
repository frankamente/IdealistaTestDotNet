namespace IdealistaTest.Infrastructure.Entities
{
    public class Picture
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Quality { get; set; }

        public override bool Equals(object obj)
        {

            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return ((Picture)obj).Id == Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}