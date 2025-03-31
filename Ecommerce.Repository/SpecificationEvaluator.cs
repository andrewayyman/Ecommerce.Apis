using Ecommerce.Core.Entites;
using Ecommerce.Core.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    internal static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        /// IQueryable<T> is a collection type that allows us to query data from a database or another data source
        /// in a way that is optimized for performance.

        // Function To Build Query
        public static IQueryable<TEntity> GetQuery( IQueryable<TEntity> sequence, ISpecification<TEntity> spec )
        {
            //------- Declare query with the sequence ------//
            var query = sequence;                                                  // query = dbContext.Products

            //-------------- Where Filter-------------------//
            if ( spec.Criteria is not null )
                query = query.Where(spec.Criteria);                       // query =  dbContext.Products().Where()

            //-------------- OrderByAsc --------------------//
            if ( spec.OrderBy is not null )
                query = query.OrderBy(spec.OrderBy);                     // query =  dbContext.Products().where().OrderBy()

            //-------------- OrderByDesc -------------------//
            if ( spec.OrderByDescending is not null )
                query = query.OrderByDescending(spec.OrderByDescending); // query =  dbContext.Products().where().OrderByDesc()

            // -------------- Skip&Take , Pagination -------//
            if ( spec.IsPaginationEnabled )
                query = query.Skip(spec.Skip).Take(spec.Take);          // query =  dbContext.Products().where().OrderByDesc().Skip().Take()

            //-------------- Includes Expressions ----------//                     // query =  dbContext.Products().where().OrderByDesc()..Skip().Take().Include()
            query = spec.Includes.Aggregate(query, ( current, includesExp ) => current.Include(includesExp));
            /// Trace this line :
            /// query = _dbContext.Set<Product>().where()
            /// Assume Spec.Includes = {p=>p.Brand , p=>p.Category}
            /// first iteration : current = _dbContext.Set<Product>().where()   , includesExp = p=>p.Brand
            /// Then current.Include(includesExp) = _dbContext.Set<Product>().where().Include(p=>p.Brand)
            /// Done we added include to the query succefully
            /// And so on the other includes

            return query;
        }
    }
}