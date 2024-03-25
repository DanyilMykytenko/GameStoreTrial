using Data.Entities;
using Data.Models;
using System.Linq.Expressions;

namespace Business.Builders
{
    public class QueryDirector
    {
        private SortBuilder _builder;

        public Expression<Func<Game, bool>> Sort(Filter model)
        {
            _builder = new SortBuilder();

            if (!string.IsNullOrEmpty(model.Name))
            {
                _builder.FilterByName(model.Name);
            }

            if (model.MaxPrice != null)
            {
                if (model.MinPrice != null)
                {
                    _builder.FilterByMaxPrice(model.MaxPrice.Value, model.MinPrice.Value);
                }
                else
                {
                    _builder.FilterByMaxPrice(model.MaxPrice.Value, 0);
                }
            }

            if (model.MinPrice != null)
            {
                _builder.FilterByMinPrice(model.MinPrice.Value);
            }

            if (model.DatePublishing != null)
            {
                _builder.SortByDate(model.DatePublishing);
            }

            if (model.Platforms.Any())
            {
                _builder.FilterByPlatforms(model.Platforms);
            }

            if (model.Genres.Any())
            {
                _builder.FilterByGenres(model.Genres);
            }

            if (model.Publishers.Any())
            {
                _builder.FilterByPublisher(model.Publishers);
            }

            return _builder.GetExpression();
        }
    }
}