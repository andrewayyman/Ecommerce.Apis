using Ecommerce.Core.Entites;
using Ecommerce.Core.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Repository.Contract
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        // Without Specification Pattern
        Task<T> GetAsync( int id );

        Task<IReadOnlyList<T>> GetAllAsync();

        // With Specification Pattern
        Task<T> GetWithSpecAsync( ISpecification<T> spec );

        Task<IReadOnlyList<T>> GetAllWithSpecAsync( ISpecification<T> spec );

        Task<int> GetCountSpecAsync( ISpecification<T> spec );
    }
}