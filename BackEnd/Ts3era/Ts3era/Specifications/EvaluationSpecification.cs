using Microsoft.EntityFrameworkCore;

namespace Ts3era.Specifications
{
    public class EvaluationSpecification<T> where T : class
    {
        public static IQueryable<T> GetQuery  (IQueryable<T> Query,ISpecification<T> specification)
        {
            var query = Query.AsQueryable ();

            if (specification.creiteria!=null)
                query=query.Where (specification.creiteria);



            query=specification.Includes.Aggregate(query,(current,include)=>current.Include(include));

            return query;
        }
    }
}
