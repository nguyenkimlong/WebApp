using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.Infrastructure;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Post")]
    public class PostController : Controller
    {
      //  private UnitOfWork unitOfWork = new UnitOfWork();

        [HttpGet("Get")]
        public IActionResult GetAll()
        {
            try
            {
               // var list = unitOfWork.PostRepository.GetAll();
                return Ok(null);
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
               // var item = unitOfWork.PostRepository.GetByID(id);
                return Ok(null);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost("Add")]
        public IActionResult Add(Post post)
        {
            try
            {
                //var item = unitOfWork.PostRepository.GetAll();
                //unitOfWork.PostRepository.Add(post);
                //unitOfWork.SaveChanges();
                return Ok(null);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
        [HttpPost("Update")]
        public IActionResult Update(string id, [FromBody] Post post)
        {
            try
            {
                //var Post = unitOfWork.PostRepository.GetByID(post.ID);
                //if (Post != null)
                //{
                //    unitOfWork.PostRepository.Update(post);
                //    unitOfWork.SaveChanges();
                //}
                return Ok(null);
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
                //var item = unitOfWork.PostRepository.GetByID(id);
                //if (item != null)
                //{
                //    unitOfWork.PostRepository.Delete(item);
                //    unitOfWork.SaveChanges();
                //}
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}