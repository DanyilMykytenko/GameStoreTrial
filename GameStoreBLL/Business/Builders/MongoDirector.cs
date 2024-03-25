using Data.Models;
using MongoData.Entities;
using MongoDB.Driver;

namespace Business.Builders
{
    public class MongoDirector
    {
        private MongoBuilder _builder;

        public FilterDefinition<Product> Sort(Filter model)
        {
            _builder = new MongoBuilder();

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

            if (model.Genres.Any())
            {
                _builder.FilterByGenres(model.Genres);
            }

            if (model.Publishers.Any())
            {
                _builder.FilterByPublisher(model.Publishers);
            }

            if (model.Platforms.Any())
            {
                _builder.FilterByPlatform();
            }

            return _builder.GetFilter();
        }
    }
}
