using AutoMapper;
using ContactsAPI.DTO;
using ContactsAPI.Models;
using ContactsAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ContactsAPI.Controllers
{
    [ApiController]
    [Route("/api/employees")]
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IApplicationService _applicatiionService;
        private readonly IMapper _mapper;

        public EmployeeController(
            IConfiguration configuration, IApplicationService applicatiionService, IMapper mapper
        ) : base()
        {
            _configuration = configuration;
            _applicatiionService = applicatiionService;
            _mapper = mapper;
        }

        [HttpPost]
        [Route("/api/employees")]
        public async Task<IActionResult> InsertEmployeeAsync(EmployeeDTO request)
        {
            try
            {
                await _applicatiionService.EmployeeService.InsertEmployeeAsync(request);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdateDTO request)
        {
            try
            {
                var employee = await _applicatiionService.EmployeeService.GetEmployeeByIdAsync(request.Id);
                if (employee is null) return NotFound();
                var updatedEmployee = await _applicatiionService.EmployeeService.UpdateEmployeeAsync(request, request.Id);
                return Ok(updatedEmployee);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            try
            {
                var employee = await _applicatiionService.EmployeeService.GetEmployeeByIdAsync(id);
                if (employee is null) return NotFound();
                _applicatiionService.EmployeeService.DeleteEmployee(id);
                return Ok();
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/employee/{slug}")]
        public async Task<IActionResult> GetEmployeeBySlugAsync(string slug)
        {
            try
            {
                if (slug is null) return NotFound();
                var employee = await _applicatiionService.EmployeeService.GetEmployeeBySlugAsync(slug);
                if (employee is null) return NotFound();
                return Ok(employee);
            } catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("/api/employees/all")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _applicatiionService.EmployeeService.GetAllEmployeesAsync();
                return Ok(employees);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
