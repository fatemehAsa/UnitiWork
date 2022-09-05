using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Application.Admin.Category.Queries
{
    public class IsExistCategoryInPostQueries : IRequest<bool>
    {
        public IsExistCategoryInPostQueries(int categoryId)
        {
            Id = categoryId;
        }

        public int Id { get; set; }
    }
}
