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
        [Route("/api/projects")]
        public async Task<IActionResult> UpdateProjectAsync(ProjectUpdateDTO request)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProjectAsync(request.Id);
                var updatedProject = await _applicatiionService.ProjectService.UpdateProjectAsync(request, project!.Id);
                return Ok(updatedProject);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/projects/get-project")]
        public async Task<IActionResult> GetProjectByIdAsync(int id)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProjectAsync(id);
                return Ok(project);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("/api/projects/{id}")]
        public async Task<IActionResult> DeleteProjectAsync(int id)
        {
            try
            {
                var project = await _applicatiionService.ProjectService.GetProjectAsync(id);
                if (project is null) return NotFound();
                _applicatiionService.ProjectService.DeleteProjectAsync(project.Id);
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
