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

        // sequence : _dbContext.Set<Product>()
        public static IQueryable<TEntity> GetQuery( IQueryable<TEntity> sequence, ISpecification<TEntity> spec )
        {
            //-------------- Declare query with the sequence --------------//
            var query = sequence;                                  // query = _dbContext.Set<Products>()

            //-------------- Where Filter--------------//
            if ( spec.Criteria is not null )
            {
                query = query.Where(spec.Criteria);       // query = _dbContext.Set<Product>().where()
            }

            //-------------- OrderBy Filter--------------//
            if ( spec.OrderBy is not null )
            {
                query = query.OrderBy(spec.OrderBy); // query = _dbContext.Set<Product>().where().OrderBy(p=>p.Id)
            }

            //-------------- OrderByDescending Filter--------------//
            if ( spec.OrderByDescending is not null )
            {
                query = query.OrderByDescending(spec.OrderByDescending); // query = _dbContext.Set<Product>().where().OrderByDescending(p=>p.Id)
            }

            //-------------- Includes Expressions --------------//    \
            // The Aggregate method is used to iterate through all includes and apply them one by one.
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