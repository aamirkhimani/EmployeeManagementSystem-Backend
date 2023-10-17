using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Interface;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EMS_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : Controller
    {
        public readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
            var employeesList = await _employeeService.GetEmployees();

            if(!employeesList.Any())
            {
                return NotFound();
            }

            return Ok(employeesList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(string id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            var isAdded = await _employeeService.AddEmployee(employee);

            return isAdded == true ? Ok() : BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateEmployee(Employee employee)
        {
            var isUpdated = await _employeeService.UpdateEmployee(employee);

            return isUpdated == true ? Ok() : BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(string id)
        {
            var employee = await _employeeService.GetById(id);

            if (employee == null)
            {
                return NotFound();
            }

            var isDeleted = await _employeeService.DeleteEmployee(id);

            return isDeleted == true ? Ok() : BadRequest();
        }
    }
}

