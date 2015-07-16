using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;

namespace TodosList.Services
{
    public class SubTodoService
    {
        private TodoRepository _repository;

        public SubTodoService(TodoRepository repository)
        {
            _repository = repository;
        }

        public void Add(SubTodo item)
        {
            _repository.AddSubTodo(item);
        }

        public SubTodo Update(SubTodo item)
        {
           return _repository.UpdateSubTodo(item);
        }

        public void Delete(int id)
        {
            _repository.DeleteSubTodo(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }
    }
}