using System;
using System.Web.Http;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;
using TodosList.Services;


namespace TodosList.ApiControllers
{
    public class SubTodoController : ApiController
    {
        private SubTodoService _service;
        public SubTodoController()
        {
            _service = new SubTodoService(new TodoRepository());
        }

        // POST api/subtodo
        [HttpPost]
        [Route("api/subtodo")]
        public IHttpActionResult Post(SubTodo newSubTodo)
        {
            try
            {
                _service.Add(newSubTodo);
                return Ok(newSubTodo);
            }
            catch (Exception)
            {
                return NotFound();
            }
        }

        // PUT api/subtodo/5
        [HttpPost]
        [Route("api/subtodo/{id}")]
        public IHttpActionResult Put(int id, SubTodo subTodo)
        {
            try
            {
                return Ok(_service.Update(subTodo));
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // DELETE api/subtodo/5
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_service != null)
                {
                    _service.Dispose();
                }
            }
            base.Dispose(disposing);
        }
    }
}
