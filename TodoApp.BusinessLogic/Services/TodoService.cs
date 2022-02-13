using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.BusinessLogic.Bus;
using TodoApp.Core.Services;
using TodoApp.Core;
using TodoApp.Core.DTOs;
using TodoApp.Core.Repositories;
using Todo = TodoApp.DAL.Entities.Todo;
using User = TodoApp.DAL.Entities.User;


namespace TodoApp.BusinessLogic.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService(ITodoRepository todoRepository, EvtBus evtBus)
        {
            _todoRepository = todoRepository;
        }
        public async Task<int?> CreateTodo(Todo todo, User user)
        {
            todo.CreatedAt = DateTime.Now;
            todo.Creator = user;
            var result = await _todoRepository.Add(todo);
            return result;
        }

        public async Task<List<Todo>> GetAllTodosInProjectAsync(ProjectId projectId)
        {
            var result = await _todoRepository.GetAllInProject(projectId);
            return result;

        }

        public async Task<bool> UpdateTodoAsync(TodoToUpdate todo)
        {
            var newTodo = new Todo
            {
                Title = todo.title,
                Desc = todo.desc,
                IsFinished = todo.isFinished,
                ProjectId = todo.projectId,
                Id = todo.id,
            };
            var result = await _todoRepository.UpdateTodo(newTodo);
            return result;
        }
    }
}