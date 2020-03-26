using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentController : ControllerBase
    {
        private readonly IDbService _dbService;

        public StudentController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(String orderBy)
        {
            List<Student> list = (List<Student>) _dbService.GetStudents();
            
            string o="";
            for (int i = 0; i < list.Count; i++)
            {
                Student student = list[i];
                o = o + student.firstName + " " + student.lastName + " " + student.birthDate + " " + student.studyName +
                    " " + student.semester + "\r\n";
            }
            
            
            return Ok(o);
        }
        
        [HttpGet("{id}/semester")]
        public IActionResult GetSemester(int id)
        {
            return Ok(_dbService.GetSemester(id));

        }

        [HttpGet("{id}")]
        public IActionResult GetStudent(int id)
        {
            if (id <= _dbService.GetStudents().Count())
                return Ok(_dbService.GetStudents().Where(student => student.idStudent == id));
            else
                return NotFound("nie znaleziono studenta");
        }

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            student.indexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, Student student)
        {
            student.idStudent = id;
            return Ok(student);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}
