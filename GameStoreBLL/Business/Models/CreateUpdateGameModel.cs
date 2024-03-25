namespace Business.Models
{
    public class CreateUpdateGameModel
    {
        public int? Id { get; set; }
        public string Key { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public short UnitInStock { get; set; }

        public int Discontinued { get; set; }

        public List<int>? Genres { get; set; }

        public List<int>? Platforms { get; set; }

        public int? Publisher { get; set; }
    }
}
