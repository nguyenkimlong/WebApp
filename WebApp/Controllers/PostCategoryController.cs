using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.IService;
using DAL.Infrastructure;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/PostCategory")]
    public class PostCategoryController : Controller
    {
        // private UnitOfWork unitOfWork = new UnitOfWork();
        private IPostCategoryService _PostCategoryService;

        public PostCategoryController(IPostCategoryService PostCategoryService)
        {
            _PostCategoryService = PostCategoryService;
        }

        [HttpGet("Get")]
        public IActionResult GetAll()
        {
            try
            {
                var list = _PostCategoryService.GetAll();
                return Ok(list);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("GetByID")]
        public IActionResult GetByID(int id)
        {
            try
            {
                var item = _PostCategoryService.GetByID(id);
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
                var item = _PostCategoryService.Add(PostCategory);
                _PostCategoryService.SaveChange();
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
                if (PostCategory != null)
                {
                    _PostCategoryService.Update(id, PostCategory);
                    //var a = (new PostCategory
                    //{
                    //    ID=3,
                    //    Alias ="1233",
                    //    Status= true,
                    //    Name="long1123"
                    //}
                    //);
                    //_PostCategoryService.Update(id, a);
                    _PostCategoryService.SaveChange();
                }
                return Ok();
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
                _PostCategoryService.Delete(id);
                _PostCategoryService.SaveChange();
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}