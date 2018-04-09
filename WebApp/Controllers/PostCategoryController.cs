using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/PostCategory")]
    public class PostCategoryController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet("Get")]
        public IActionResult GetAll()
        {
            try
            {
                var list = unitOfWork.PostCategoryRepository.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("GetByID")]
        public IActionResult GetByID(string id)
        {
            try
            {
                var item = unitOfWork.PostCategoryRepository.GetByID(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("Add")]
        public IActionResult Add(PostCategory PostCategory)
        {
            try
            {
                var item = unitOfWork.PostCategoryRepository.GetAll();
                unitOfWork.PostCategoryRepository.Add(PostCategory);
                unitOfWork.SaveChanges();
                return Ok(item);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost("Update")]
        public IActionResult Update(string id, [FromBody] PostCategory PostCategory)
        {
            try
            {
                var item = unitOfWork.PostCategoryRepository.GetByID(PostCategory.ID);
                if (PostCategory != null)
                {
                    unitOfWork.PostCategoryRepository.Update(PostCategory);
                    unitOfWork.SaveChanges();
                }
                return Ok(PostCategory);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            try
            {
                var item = unitOfWork.PostCategoryRepository.GetByID(id);
                if (item != null)
                {
                    unitOfWork.PostCategoryRepository.Delete(item);
                    unitOfWork.SaveChanges();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}