﻿using System;
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
            List<StudentModel> list = (List<StudentModel>) _dbService.GetStudents();
            
            string o="";
            for (int i = 0; i < list.Count; i++)
            {
                StudentModel studentModel = list[i];
                o = o + studentModel.firstName + " " + studentModel.lastName + " " + studentModel.birthDate + " " + studentModel.studyName +
                    " " + studentModel.semester + "\r\n";
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
        public IActionResult CreateStudent(StudentModel studentModel)
        {
            studentModel.indexNumber = $"s{new Random().Next(1, 20000)}";
            return Ok(studentModel);
        }

        [HttpPut("{id}")]
        public IActionResult PutStudent(int id, StudentModel studentModel)
        {
            studentModel.idStudent = id;
            return Ok(studentModel);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            return Ok("Usuwanie ukończone");
        }
    }
}
