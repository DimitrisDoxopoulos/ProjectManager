using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/projects")]
    public class ProjectController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationService _applicatiionService;
        private readonly IMapper _mapper;

        public ProjectController(
            IConfiguration configuration, IApplicationService applicatiionService, IMapper mapper
        ) : base()
        {
            _configuration = configuration;
            _applicatiionService = applicatiionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/projects")]
        public async Task<IActionResult> InsertProject(ProjectDTO request)
        {
            try
            {
                await _applicatiionService.ProjectService.InsertProjectAsync(request);
                return Ok("The project was created successfully");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        [Route("/api/projects/update")]
        public async Task<IActionResult> UpdateProjectAsync(ProjectUpdateDTO request)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProject(request.Id);
                var updatedProject = await _applicatiionService.ProjectService.UpdateProjectAsync(request, project!.Id);
                return Ok(updatedProject);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/projects/{slug}")]
        public async Task<IActionResult> GetProjectBySlugAsync(string slug)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProjectBySlugAsync(slug);
                return Ok(project);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/projects/delete/{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProject(id);
                if (project is null) return NotFound();
                _applicatiionService.ProjectService.DeleteProject(id);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/projects/all")]
        public async Task<IActionResult> GetAllProjectsAsync()
        {
            try
            {
                var projects = await _applicatiionService.ProjectService.GetAllProjectsAsync();
                return Ok(projects);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/projects/all")]
        public async Task<IActionResult> GetAllProjectsOfUserAsync(int userId)
        {
            try
            {
                var projects = await _applicatiionService.ProjectService.GetAllProjectsOfUserAsync(userId);
                return Ok(projects);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
