using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        public async Task<int> AddProjectAsync(Project project)
        {
            var result = await _context.AddAsync(project);
            _ = await _context.SaveChangesAsync();
            return result.Entity.Id;
        }

        public async Task<int?> UpdateProjectAsync(Project project)
        {
            var projectToUpdate = await _context.Projects.FirstOrDefaultAsync(x => x.Id == project.Id);
            if (projectToUpdate == null) return null;
            projectToUpdate.Title = project.Title;
            projectToUpdate.LastModifiedDate = DateTime.UtcNow;
            _ = await _context.SaveChangesAsync();
            return projectToUpdate.Id;
        }

        public async Task<List<Project>> GetUserProjectsAsync(User user)
        {
            var projects = await _context.Projects
                .Where(x => x.Creator == user)
                .ToListAsync();
            return projects;
        }

    }
}
