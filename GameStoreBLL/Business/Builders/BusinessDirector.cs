using Data.Entities;
using Data.Models;

namespace Business.Builders
{
    public class BusinessDirector
    {
        private BusinessBuilder _builder;
        private readonly IEnumerable<Game> _games;

        public IEnumerable<Game> Sort(Filter model, IEnumerable<Game> query)
        {
            _builder = new BusinessBuilder();
            _builder.SetParams(query);

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
                _builder.FilterByDate(model.DatePublishing);
            }

            if (model.Sort != null)
            {
                _builder.SortByType(model.Sort);
            }

            if ((model.Page != null && model.PageCount != null) && model.PageCount != "all")
            {
                if (int.TryParse(model.PageCount, out int count))
                {
                    _builder.FilterWithPagging(model.Page, count);
                }
            }
            return _builder.GetExecuted();
        }
    }
}
