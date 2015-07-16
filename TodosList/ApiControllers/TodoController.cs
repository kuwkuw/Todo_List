using System;
using System.Web.Http;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;
using TodosList.Services;

namespace TodosList.ApiControllers
{
    public class TodoController : ApiController
    {
        private TodoService _service;

        public TodoController()
        {
            _service = new TodoService(new TodoRepository());
        }

        // POST api/todo
        [HttpPost]
        [Route("api/todo")]
        public IHttpActionResult PostTodo(Todo newTodo)
        {
            try
            {
                return Ok(_service.Add(newTodo));
            }
            catch (Exception)
            {
                
                return NotFound();
            }
        }

        // PUT api/todo/5
        [HttpPost]
        [Route("api/todo/{id}")]
        public IHttpActionResult Put(int id, Todo todo)
        {
            try
            {
                _service.Udate(todo);
                return Ok();
            }
            catch (Exception)
            {

                return NotFound();
            }
        }

        // DELETE api/values/5
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
                if (_service!= null)
                {
                    _service.Dispose();
                }
            }

            base.Dispose(disposing);
        }
    }
}