namespace Business.Models
{
    public class GetGamesRequestModel
    {
        public GetGamesRequestModel()
        {
            if (Page is null)
            {
                Page = 1;
            }
            if (PageCount is null)
            {
                PageCount = "10";
            }
        }

        public int? MaxPrice { get; set; }
        public int? MinPrice { get; set; }
        public string? Name { get; set; }
        public string? DatePublishing { get; set; }
        public string? Sort { get; set; }
        public int? Page { get; set; }
        public string? PageCount { get; set; }

        public ICollection<string>? Genres { get; set; }
        public ICollection<string>? Platforms { get; set; }
        public ICollection<string>? Publishers { get; set; }
    }
}
