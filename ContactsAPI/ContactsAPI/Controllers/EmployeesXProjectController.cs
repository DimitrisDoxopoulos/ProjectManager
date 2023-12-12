using AutoMapper;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/assign-projects")]
    public class EmployeesXProjectController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationService _applicationService;
        private readonly IMapper? _mapper;

        public EmployeesXProjectController(
            IApplicationService applicationService, IMapper mapper, IConfiguration configuration
        ) : base()
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> AssignProject(int employeeId, int projectId)
        {
            var employee = _applicationService.EmployeeService.GetEmployeeByIdAsync( employeeId );
            var project = _applicationService.ProjectService.GetProject( projectId );

            if (employee is null || project is null) return NotFound();
            try
            {
                await _applicationService.EmployeesXProjectsService.AssignProjectAsync(employeeId, projectId);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveAssignment(int employeeId, int projectId)
        {
            var employee = _applicationService.EmployeeService.GetEmployeeByIdAsync(employeeId);
            var project = _applicationService.ProjectService.GetProject(projectId);

            if (employee is null || project is null) return NotFound();
            try
            {
                await _applicationService.EmployeesXProjectsService.DeleteAssignmentAsync(employeeId, projectId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
