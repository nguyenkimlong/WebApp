using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.IService
{
    public interface IPostCategoryService
    {
        IEnumerable<PostCategory> GetAll();
        PostCategory GetByID(int id);
        IEnumerable<PostCategory> Add(PostCategory PostCategory);
        void Update(string id, PostCategory PostCategory);
        PostCategory Delete(string id);
        void SaveChange();
    }
}
