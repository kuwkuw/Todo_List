using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;

namespace TodosList.Services
{
    public class TodoService
    {
        private TodoRepository _repository;

        public TodoService(TodoRepository repository)
        {
            _repository = repository;
        }

        public Todo Get(int id)
        {
            return _repository.GetTodo(id);
        }

        public Todo Add(Todo item)
        {
            return _repository.AddTodo(item); 
        }

        public Todo Udate(Todo item)
        {
            return _repository.UpdateTodo(item);
        }

        public void Delete(int id)
        {
            _repository.DeleteTodo(id);
        }

        public void Dispose()
        {
            _repository.Dispose();
        }

    }
}