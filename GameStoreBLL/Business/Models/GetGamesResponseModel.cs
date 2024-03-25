namespace Business.Models
{
    public class GetGamesResponseModel
    {
        public ICollection<GameModel> Games { get; set; }
        public string TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
