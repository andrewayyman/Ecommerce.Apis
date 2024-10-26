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


        // sequence : _dbContext.Set<Product>() 
        public static IQueryable<TEntity> GetQuery( IQueryable<TEntity> sequence, ISpecification<TEntity> spec )
        {

            //-------------- Declare query with the sequence --------------//  
            var query = sequence; // query = _dbContext.Set<T>() , if Tentity = any BaseEntity Class 


            //-------------- Where Filter--------------//  
            if ( spec.Criteria is not null ) // then we have where clause 
            {
                query = query.Where(spec.Criteria); // query = _dbContext.Set<Product>().where() 
            }


            //-------------- Includes Expressions --------------//    
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
