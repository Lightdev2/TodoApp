using System.Threading.Tasks;
using TodoApp.Core.Repositories;
using TodoApp.DAL;
using TodoApp.DAL.Entities;

namespace TodoApp.BusinessLogic.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly TodoAppContext _context;

        public ProjectsRepository(TodoAppContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Project project)
        {
            var result = await _context.AddAsync(project);
            _ = _context.SaveChangesAsync();
            return result.Entity.Id;
        }
    }
}
