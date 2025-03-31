using Ecommerce.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Ecommerce.Core.Specification
{
    public class BaseSpecifications<T> : ISpecification<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; } // null
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>(); // intialize here instead of ctor

        // in case no need where condition .. criteria = null
        public BaseSpecifications()
        {
            // criteria = null
            //Includes = new List<Expression<Func<T, object>>> (); , WE Can Intialize it in the porperty itself
        }

        // in case we need where condition .. criteria = p=>p.Id == id
        public BaseSpecifications( Expression<Func<T, bool>> criteriaExpression )
        {
            Criteria = criteriaExpression;
        }
    }
}