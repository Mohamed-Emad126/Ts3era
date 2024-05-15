using System.Linq.Expressions;

namespace Ts3era.Specifications
{
    public class BaseSpecification<T> : ISpecification<T> where T : class
    {
        public BaseSpecification(Expression<Func<T,bool>> creiteria )
        {
            this.creiteria = creiteria;
        }
        public Expression<Func<T, bool>> creiteria {  get;  }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();


        protected void AddInclude (Expression<Func<T, object>> include)
            =>Includes.Add(include);

    }
}
