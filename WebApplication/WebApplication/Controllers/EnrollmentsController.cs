using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL;
using WebApplication.DTOs;
using WebApplication.Models;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private IStudentsDbService _studentsDbService;

        public EnrollmentsController(IStudentsDbService studentsDbService)
        {
            _studentsDbService = studentsDbService;
        }

        [HttpPost]
        public IActionResult CreateStudent(EnrollStudentResponse request)
        {
            EnrollmentResponse enrollmentResponse = _studentsDbService.CreateStudent(request);
            if (enrollmentResponse == null)
                return BadRequest("studia nie istnieją");
            else
                return CreatedAtAction("createStudent", enrollmentResponse);
        }

        [HttpPost("promotions")]
        public IActionResult Promote(EnrollPromoteRequest enrollPromoteRequest)
        {
            EnrollmentResponse enrollmentResponse = _studentsDbService.PromoteStudents(enrollPromoteRequest);
            if (enrollmentResponse == null)
                return BadRequest("studia nie istnieją");
            else
                return CreatedAtAction("createStudent", enrollmentResponse);
        }
    }
}