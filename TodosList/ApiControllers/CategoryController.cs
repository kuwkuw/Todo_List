using System;
using System.Collections.Generic;
using System.Web.Http;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;
using TodosList.Services;

namespace TodosList.ApiControllers
{
    public class CategoryController : ApiController
    {

        CategoryService _service = new CategoryService(new TodoRepository());


        [HttpGet]
        [Route("api/category")]
        public IEnumerable<TodoCategory> Get()
        {
            return _service.GetAll();
        }


        // POST api/category
        [HttpPost]
        [Route("api/category")]
        public IHttpActionResult Add(TodoCategory newCategory)
        {
            try
            {
                return Ok(_service.Add(newCategory));
            }
            catch (Exception)
            {

                return NotFound();
            }
        }


        [HttpDelete]
        [Route("api/category/clear/{Id}")]
        public IHttpActionResult Clear(int id)
        {
            try
            {
                _service.Clear(id);
                return Ok(_service.Get(id));
            }
            catch (Exception)
            {

                return NotFound();
            }
           
        }
        [HttpDelete]
        [Route("api/category/delete/{Id}")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }
    }
}
