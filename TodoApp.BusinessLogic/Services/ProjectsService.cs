using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Core.Services;
using TodoApp.Core.Repositories;
using TodoApp.DAL.Entities;

namespace TodoApp.BusinessLogic.Services
{
    public class ProjectsService : IProjectsService
    {
        private readonly IProjectsRepository _projectsRepository;

        public ProjectsService(IProjectsRepository projectsRepository)
        {
            _projectsRepository = projectsRepository;
        }
        public async Task<int> CreateProjectAsync(Project project, User user)
        {
            var projectToSave = new Project
            {
                Title = project.Title,
                Creator = user,
                CreatedDate = DateTime.UtcNow,
            };
            var result = await _projectsRepository.AddProjectAsync(projectToSave);
            return result;
        }

        public async Task<bool> DeleteProjectAsync(int projectId, User user)
        {
            var project = await _projectsRepository.FindProjectById(projectId);
            if (project == null || project.Creator != user) return false;
            var result = await _projectsRepository.DeleteProjectAsync(project);
            return result;
        }

        public async Task<int?> UpdateProjectAsync(Project project, User user)
        {
            project.LastModifiedDate = DateTime.UtcNow;
            var result = await _projectsRepository.UpdateProjectAsync(project, user);
            return result;
        }

        public async Task<List<Project>> GetUserProjectsAsync(User user)
        {
            var result = await _projectsRepository.GetUserProjectsAsync(user);
            return result;
        }
    }
}
