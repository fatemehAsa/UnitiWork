using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Admin.Category.Queries
{
   public class IsExistCategoryQueries:IRequest<bool>
    {

        public IsExistCategoryQueries(int categoryId)
        {

        }

        public int CategoryId { get; set; }
    }
}
