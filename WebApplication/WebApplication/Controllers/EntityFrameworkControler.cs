using System;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/EF")]
    public class EntityFrameworkControler : ControllerBase
    {
        private IdbService2 _dbService;
        
        public EntityFrameworkControler(IdbService2 dbService)
        {
            _dbService = dbService;
        }
        
        [HttpGet]
        public IActionResult getStudent()
        {
            var students = _dbService.getStudents();
            if (students == null)
               return BadRequest();

            return Ok(students);
        }

        [HttpPost]
        public IActionResult updateStudent(Student student)
        {
            var s = _dbService.updateStudent(student);
            if (s == null)
                return BadRequest();
            return Ok(s);
        }

        [HttpDelete("{index}")]
        public IActionResult deleateStudent(String index)
        {
            var s = _dbService.deleateStudent(index);
            if (s == null)
                return BadRequest();
            return Ok(s);    
        }

        [HttpPost("EnrollStudent")]
        public IActionResult createStudent(Student student)
        {
            var s = _dbService.createStudnt(student);
            if (s == null)
                return BadRequest();
            return Ok(s);
        }

        [HttpPost("PromoteStudent")]
        public IActionResult promoteStudent(EnrollPromoteRequest promoteRequest)
        {
            var newEnrollment = _dbService.promoteStudents(promoteRequest);
            if (newEnrollment == null)
                return BadRequest();
            return Ok(newEnrollment);
        }
    }
}