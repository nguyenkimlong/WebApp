using Business.IService;
using DAL.Infrastructure;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Service
{
    public class PostCategoryService : IPostCategoryService
    {
        private IUnitOfWork _unitOfWork;
      
        public PostCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
           
        }
        public IEnumerable<PostCategory> Add(PostCategory PostCategory)
        {
            var item = _unitOfWork.Repository<PostCategory>().GetAll();
            _unitOfWork.Repository<PostCategory>().Add(PostCategory);          
            return item;
        }

        public PostCategory Delete(string id)
        {
            var item = _unitOfWork.Repository<PostCategory>().GetByID(id);
            if (item != null)
            {
                _unitOfWork.Repository<PostCategory>().Delete(item);              
            }
            return item;
        }

        public IEnumerable<PostCategory> GetAll()
        {
            var list = _unitOfWork.Repository<PostCategory>().GetAll().ToList();
            return list;
        }

        public PostCategory GetByID(int id)
        {
            var item = _unitOfWork.Repository<PostCategory>().Get(x => x.ID == id).FirstOrDefault();
            return item;
        }

        public void Update(string id, PostCategory PostCategory)
        {
            var item = _unitOfWork.Repository<PostCategory>().GetQuery<PostCategory>().Where(x => x.ID == PostCategory.ID).ToList();
            if (PostCategory != null)
            {
                _unitOfWork.Repository<PostCategory>().Update(PostCategory);              
            }           
        }
        public void SaveChange()
        {
            _unitOfWork.SaveChanges();
        }
    }
}
