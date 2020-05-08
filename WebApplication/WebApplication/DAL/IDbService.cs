using System;
using System.Collections;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.DAL
{
    public interface IDbService
    {
        public IEnumerable<StudentModel> GetStudents();
        public String GetSemester(int id);
    }
}