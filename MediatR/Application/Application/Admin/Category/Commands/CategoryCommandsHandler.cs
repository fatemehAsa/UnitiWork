using Application.Application.Admin.Category.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.UnitOfWork;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Application.Admin.tbl_Category.Commands
{
    public class CategoryCommandsHandler : IRequestHandler<AddCategoryCommand, Models.Models.Category>,
        IRequestHandler<EditCategoryCommand, bool>,
        IRequestHandler<DeleteCategoryCommand, bool>
    {
        //private readonly CustomContext _context;

        private readonly UnitOfWork _unitOfWork;
        public CategoryCommandsHandler(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public async Task<Models.Models.Category> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            //await _context.tbl_Categories.AddAsync(request.Category);

            //await _context.SaveChangesAsync(cancellationToken);

            _unitOfWork.CategoryRepository.Insert(request.Category);
            await _unitOfWork.Save(cancellationToken);
            return request.Category;
        }

        public async Task<bool> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
        {
            //_context.tbl_Categories.Update(request.Category);
            //await _context.SaveChangesAsync(cancellationToken);
            _unitOfWork.CategoryRepository.Update(request.Category);
            await _unitOfWork.Save(cancellationToken);
            return true;
        }

        public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            //var cat = await _context.tbl_Categories.FindAsync(request.Id);
            var cat =await _unitOfWork.CategoryRepository.GetByID(request.Id,cancellationToken);

            try
            {
                //_context.tbl_Categories.Remove(cat);
                //await _context.SaveChangesAsync(cancellationToken);
                _unitOfWork.CategoryRepository.Delete(cat);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (System.Exception ex)
            {

                throw;
            }

            return true;
        }


    }
}
