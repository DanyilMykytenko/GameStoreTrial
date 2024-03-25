using Business.Enums;
using Data.Entities;

namespace Business.Builders
{
    public class BusinessBuilder
    {
        private IEnumerable<Game> _query;

        public void SetParams(IEnumerable<Game> query)
        {
            _query = query;
        }

        public void FilterByName(string name)
        {
            _query = _query.Where(x => x.Name.Contains(name));
        }

        public void FilterByMaxPrice(int maxPrice, int minPrice)
        {
            _query = _query.Where(x => x.Price >= minPrice && x.Price <= maxPrice);
        }

        public void FilterByMinPrice(int minPrice)
        {
            _query = _query.Where(x => x.Price >= minPrice);
        }

        public void FilterByDate(string date)
        {
            switch (date)
            {
                case nameof(PublishedDateEnum.None):
                    break;
                case nameof(PublishedDateEnum.Week):
                    _query = _query.Where(x => x.CreationDate == DateTime.Now.AddDays(-7));
                    break;
                case nameof(PublishedDateEnum.Month):
                    _query = _query.Where(x => x.CreationDate == DateTime.Now.AddMonths(-1));
                    break;
                case nameof(PublishedDateEnum.Year):
                    _query = _query.Where(x => x.CreationDate == DateTime.Now.AddYears(-1));
                    break;
                case nameof(PublishedDateEnum.TwoYears):
                    _query = _query.Where(x => x.CreationDate == DateTime.Now.AddYears(-2));
                    break;
                case nameof(PublishedDateEnum.ThreeYears):
                    _query = _query.Where(x => x.CreationDate == DateTime.Now.AddYears(-3));
                    break;
            }
        }

        public void FilterByPlatforms(ICollection<string> Platforms)
        {
            _query = _query.Where(x => x.Platforms.Any(y => Platforms.Contains(y.Id.ToString())));
        }

        public void FilterByGenres(ICollection<string> Genres)
        {
            _query = _query.Where(x => x.Genres.Any(y => Genres.Contains(y.Id.ToString())));
        }

        public void FilterByPublisher(ICollection<string> Publishers)
        {
            _query = _query.Where(x => Publishers.Contains(x.Publisher.Id.ToString()));
        }

        public void SortByType(string Filter)
        {
            switch (Filter)
            {
                case nameof(SortEnum.Popular):
                    _query = _query.OrderByDescending(x => x.Viewed);
                    break;
                case nameof(SortEnum.Commented):
                    _query = _query.OrderByDescending(x => x.Comments.Count());
                    break;
                case nameof(SortEnum.Asc):
                    _query = _query.OrderByDescending(x => x.Price);
                    break;
                case nameof(SortEnum.Desc):
                    _query = _query.OrderBy(x => x.Price);
                    break;
                case nameof(SortEnum.New):
                    _query = _query.OrderByDescending(x => x.CreationDate);
                    break;
            }
        }

        public void FilterWithPagging(int? Page, int? PageCount)
        {
            _query = _query.Skip((Page.Value - 1) * (PageCount.Value)).Take(PageCount.Value);
        }

        public IEnumerable<Game> GetExecuted()
        {
            return _query;
        }
    }
}
