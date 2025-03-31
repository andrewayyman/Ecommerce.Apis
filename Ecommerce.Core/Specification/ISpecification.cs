using Ecommerce.Core.Entites;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Specification
{
    public interface ISpecification<T> where T : BaseEntity
    {
        // for Where query
        public Expression<Func<T, bool>> Criteria { get; set; } // Lambda Exp --->>>> p=>p.Id == id

        // For Include Query
        public List<Expression<Func<T, object>>> Includes { get; set; } // Nav_Prop Path which is Exp --->>> {p=>p.Brand , p=>p.Category}

        // For OrderBy Query
        public Expression<Func<T, object>> OrderBy { get; set; }

        // For OrderByDescending Query
        public Expression<Func<T, object>> OrderByDescending { get; set; }
    }
}