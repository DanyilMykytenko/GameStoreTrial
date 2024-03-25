namespace Business.Models
{
    public class CreateUpdateGenreModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public int? ParentGenreId { get; set; }
    }
}
