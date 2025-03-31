using Ecommerce.Core.Entites;
using Ecommerce.Core.Repository.Contract;
using Ecommerce.Core.Specification;
using Ecommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _dbContext;

        public GenericRepository( StoreContext dbContext )
        {
            _dbContext = dbContext;
        }

        #region Without Using Spec design pattern

        public async Task<T> GetAsync( int id )
        {
            if ( typeof(T) == typeof(Product) ) // to include Brand and Category
            {
                return await _dbContext.Set<Product>()
                                       .Where(p => p.Id == id)
                                       .Include(p => p.Brand)
                                       .Include(p => p.Category)
                                       .FirstOrDefaultAsync() as T;
            }
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            if ( typeof(T) == typeof(Product) ) // to include Brand and Category
            {
                return ( IReadOnlyList<T> )await _dbContext.Set<Product>()
                                       .Include(p => p.Brand)
                                       .Include(p => p.Category)
                                       .ToListAsync();
            }
            return await _dbContext.Set<T>().ToListAsync();
        }

        #endregion Without Using Spec design pattern

        #region Using Spec design pattern

        public async Task<T> GetWithSpecAsync( ISpecification<T> spec )
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync( ISpecification<T> spec )
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        // to reduce redundant code
        private IQueryable<T> ApplySpecification( ISpecification<T> spec )
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>(), spec);
        }

        /// Why Spec Pattern is usefull ?
        /// 1 - Generic Query Generation: <
        /// The method GetQuery works on IQueryable<TEntity>.
        /// Since TEntity is constrained to inherit from BaseEntity, the repository can accept
        /// any entity derived from it, like Product, Category, or Brand.
        ///
        /// 2 - Dynamic Query Handling:
        /// This dynamically builds the query with all necessary
        /// Include expressions and Where clauses, making the repository agnostic to the
        /// specific type (Product, Category, etc.).
        ///
        /// 3 - No Type Checking Needed:
        ///Since all the logic( such as Where conditions or Include clauses)
        ///is applied inside the GetQuery method, there's no need to check typeof(T)
        ///or cast Product to T in the repository. The query returned is already typed
        ///correctly as IQueryable<TEntity>.
        ///

        #endregion Using Spec design pattern
    }
}