using Business.Enums;
using Data.Entities;
using System.Linq.Expressions;

namespace Business.Builders
{
    public class SortBuilder
    {
        private Expression<Func<Game, bool>> _expression;

        public SortBuilder()
        {
            _expression = game => game.IsDeleted == false;
        }

        public void FilterByName(string name)
        {
            _expression = AppendExpressions(_expression, game => game.Name.Contains(name));
        }

        public void FilterByMaxPrice(int maxPrice, int minPrice)
        {
            _expression = AppendExpressions(_expression, game => game.Price >= minPrice && game.Price <= maxPrice);
        }

        public void FilterByMinPrice(int minPrice)
        {
            _expression = AppendExpressions(_expression, game => game.Price >= minPrice);
        }

        public void SortByDate(string date)
        {
            switch (date)
            {
                case nameof(PublishedDateEnum.None):
                    break;
                case nameof(PublishedDateEnum.Week):
                    _expression = AppendExpressions(_expression, game => game.CreationDate == DateTime.Now.AddDays(-7));
                    break;
                case nameof(PublishedDateEnum.Month):
                    _expression = AppendExpressions(_expression, game => game.CreationDate == DateTime.Now.AddMonths(-1));
                    break;
                case nameof(PublishedDateEnum.Year):
                    _expression = AppendExpressions(_expression, game => game.CreationDate == DateTime.Now.AddYears(-1));
                    break;
                case nameof(PublishedDateEnum.TwoYears):
                    _expression = AppendExpressions(_expression, game => game.CreationDate == DateTime.Now.AddYears(-2));
                    break;
                case nameof(PublishedDateEnum.ThreeYears):
                    _expression = AppendExpressions(_expression, game => game.CreationDate == DateTime.Now.AddYears(-3));
                    break;
            }
        }

        public void FilterByPlatforms(ICollection<string> Platforms)
        {
            _expression = AppendExpressions(_expression, game => game.Platforms.Any(y => Platforms.Contains(y.Id.ToString())));
        }

        public void FilterByGenres(ICollection<string> Genres)
        {
            _expression = AppendExpressions(_expression, game => game.Genres.Any(y => Genres.Contains(y.Id.ToString())));
        }

        public void FilterByPublisher(ICollection<string> Publishers)
        {
            _expression = AppendExpressions(_expression, game => Publishers.Contains(game.Publisher.Id.ToString()));
        }

        public Expression<Func<Game, bool>> GetExpression()
        {
            return _expression;
        }

        public static Expression<Func<Game, bool>> AppendExpressions(Expression<Func<Game, bool>> leftExpression, Expression<Func<Game, bool>> rightExpression)
        {
            ParameterExpression parameter = leftExpression.Parameters[0];
            Expression body = Expression.AndAlso(leftExpression.Body, Expression.Invoke(rightExpression, parameter));

            return Expression.Lambda<Func<Game, bool>>(body, parameter);
        }
    }
}
