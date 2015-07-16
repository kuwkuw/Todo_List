using System.Collections.Generic;
using TodosList.DataAccess.Entities;
using TodosList.DataAccess.Repository;

namespace TodosList.Services
{
    public class CategoryService
    {
        private TodoRepository _repository;

        public CategoryService(TodoRepository repository)
        {
            _repository = repository;
        }

        public TodoCategory Get(int id)
        {
            return _repository.GetCategory(id);
        }

        public IEnumerable<TodoCategory> GetAll()
        {
            return _repository.GetTodosList();
        }

        public TodoCategory Add(TodoCategory item)
        {
            return _repository.AddCategory(item);
        }

        public void Delete(int id)
        {
            _repository.DeleteCategory(id);
        }

        public void Clear(int id)
        {
            _repository.CleanCategory(id);
        }
    }
}