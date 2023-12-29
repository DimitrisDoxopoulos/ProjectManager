using AutoMapper;
using ContactsAPI.Models;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;

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
        public async Task<IActionResult> AssignProject(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];
            var userId = request[2];

            var employee = _applicationService.EmployeeService.GetEmployeeByIdAsync( employeeId );
            var project = _applicationService.ProjectService.GetProjectAsync( projectId );

            if (employee is null || project is null) return NotFound();
            try
            {
                await _applicationService.EmployeesXProjectsService.AssignProjectAsync(employeeId, projectId, userId);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/assign-projects/delete")]
        public async Task<IActionResult> RemoveAssignment(params int[] request)
        {
            var employeeId = request[0];
            var projectId = request[1];
            var userId = request[2];

            var employee = _applicationService.EmployeeService.GetEmployeeByIdAsync(employeeId);
            var project = _applicationService.ProjectService.GetProjectAsync(projectId);

            if (employee is null || project is null) return NotFound();
            try
            {
                await _applicationService.EmployeesXProjectsService.DeleteAssignmentAsync(employeeId, projectId, userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/assign-projects/all")]
        public async Task<IActionResult> GetAllAssignments()
        {
            try
            {
                List<EmployeeProject> assignments = (List<EmployeeProject>)await _applicationService.EmployeesXProjectsService.GetAllAssignmentsAsync();
                return Ok(assignments);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("/api/assign-projects/user")]
        public async Task<IActionResult> GetAllAssignmentsOfUser(int userId)
        {
            try
            {
                List<EmployeeProject> assignments = (List<EmployeeProject>)await _applicationService.EmployeesXProjectsService.GetAllAssignmentsOfUserAsync(userId);
                return Ok(assignments);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
