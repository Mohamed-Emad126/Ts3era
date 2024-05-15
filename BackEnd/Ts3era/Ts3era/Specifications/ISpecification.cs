using System.Linq.Expressions;

namespace Ts3era.Specifications
{
    public interface ISpecification<T> where T : class
    {
        Expression<Func<T,bool>> creiteria {  get; }
        List<Expression<Func<T,object>>>Includes { get; }


    }
}
