using System;
using System.Collections.Generic;
using System.Linq;
using TodosList.DataAccess.Context;
using TodosList.DataAccess.Entities;
//using WebGrease.Css.Extensions;

namespace TodosList.DataAccess.Repository
{
    public  class TodoRepository: IDisposable
    {
        TodoContext _context = new TodoContext();

        /// <summary>
        /// Get all todos
        /// </summary>
        /// <returns>todo list</returns>
        public IEnumerable<TodoCategory> GetTodosList()
        {
                return _context.TodoCategories.ToList();
        } 

        /// <summary>
        /// Add new category
        /// </summary>
        /// <param name="newCategory"> new category object</param>
        public TodoCategory AddCategory(TodoCategory newCategory)
        {
            _context.TodoCategories.Add(newCategory);
            _context.SaveChanges();
            return _context.TodoCategories.FirstOrDefault(c => c.Name.Equals(newCategory.Name));

        }

        /// <summary>
        /// Get todo category
        /// </summary>
        /// <param name="id">category id</param>
        /// <returns></returns>
        public TodoCategory GetCategory(int id)
        {
                return _context.TodoCategories.Find(id);
        } 

        /// <summary>
        /// Delete category from database
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns>result of deleting</returns>
        public void DeleteCategory(int id)
        {
                var category = _context.TodoCategories.Find(id);
                _context.TodoCategories.Remove(category);
                _context.SaveChanges();
        }

        /// <summary>
        /// Add new todo item in database
        /// </summary>
        /// <param name="newTodo">new item</param>
        /// <returns>result of adding</returns>
        public Todo AddTodo(Todo newTodo)
        {
            
            _context.Todos.Add(newTodo);
            _context.SaveChanges();
                
            //Get created task form datebase
            var todo = _context.Todos.FirstOrDefault(item => item.Text.Equals(newTodo.Text));
            return todo;
        }

        /// <summary>
        /// Get todo from category
        /// </summary>
        /// <param name="id">todo id</param>
        /// <returns></returns>
        public Todo GetTodo(int id)
        {
            return _context.Todos.Find(id);
        }

        /// <summary>
        /// Delete todo from database
        /// </summary>
        /// <param name="id">Todo id</param>
        /// <returns>result of deleting</returns>
        public void DeleteTodo(int id)
        {
                var todo = _context.Todos.Find(id);
                _context.Todos.Remove(todo);
                _context.SaveChanges();
        }

        /// <summary>
        /// Update todo 
        /// </summary>
        /// <param name="newTodo">new state</param>
        /// <returns>result of ubdating</returns>
        public Todo UpdateTodo(Todo newTodo)
        {
            var todo = _context.Todos.Find(newTodo.TodoId);
            if (todo != null)
            {
                if (todo.IsDone != newTodo.IsDone)//change state of todo
                {
                    todo.IsDone = newTodo.IsDone;

                    if (todo.IsDone)
                    {
                        //change state of all subtodos to true
                        todo.SubTodos.Where(i => i.IsDone == false).ToList().ForEach(subtodo => subtodo.IsDone = true);
                    }
                    else
                    {
                        //change state of all subtodos to false
                        todo.SubTodos.Where(i => i.IsDone == true).ToList().ForEach(subtodo => subtodo.IsDone = false);
                    }
                }

                _context.SaveChanges();
            }
           
            return todo;
        }

        /// <summary>
        /// Add new Sub todo
        /// </summary>
        /// <param name="newSubTodo">new item</param>
        /// <returns>result of adding</returns>
        public void AddSubTodo(SubTodo newSubTodo)
        {
                _context.SubTodos.Add(newSubTodo);
                _context.SaveChanges();

        }

        /// <summary>
        /// Delete subtodo from database
        /// </summary>
        /// <param name="id">subtodo id</param>
        /// <returns>result of deleting</returns>
        public void DeleteSubTodo(int id)
        {
            var subtodo = _context.SubTodos.Find(id);
            _context.SubTodos.Remove(subtodo);
            _context.SaveChanges();
        }

        /// <summary>
        /// Update subtask 
        /// </summary>
        /// <param name="newSubTodo">new subtask state</param>
        /// <returns>result of updating</returns>
        public SubTodo UpdateSubTodo(SubTodo newSubTodo)
        {
            var todo = _context.Todos.Find(newSubTodo.TodoId);
            if (todo != null)
            {
                var subTodo = todo.SubTodos.FirstOrDefault(item=>item.TodoId==newSubTodo.TodoId);

                if (subTodo != null)
                {
                    subTodo.IsDone = newSubTodo.IsDone;// mark subtask as done
                        
                    //If all subtask in todos checked todos mark it as done
                    if (!todo.SubTodos.Where(subtodo => subtodo.IsDone == false).Any() && !todo.IsDone)
                    {
                        todo.IsDone = true;
                    }
                }
                _context.SaveChanges();
                return subTodo;
                
            }
            return null;
        }

        /// <summary>
        /// Clean category from checked todos and subtodos
        /// </summary>
        /// <param name="categoryId">Category Id</param>
        /// <returns>cleaning state</returns>
        public void CleanCategory(int categoryId)
        {
            var category = _context.TodoCategories.Find(categoryId);
            var todos = category.Todos.FindAll(item => item.IsDone);
                
            if (todos.Count > 0)
                _context.Todos.RemoveRange(todos);//Remove done things (todos) 

            List<SubTodo> sb = new List<SubTodo>();
            category.Todos
                .Where(item => item.IsDone == false).ToList()
                .ForEach(i => i.SubTodos.Where(t => t.IsDone).ToList()
                .ForEach(z => sb.Add(z)));
            foreach (var subTodo in sb)
            {
                _context.SubTodos.Remove(subTodo);//Remove done things (subtodos)
            }

            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }




    }
}