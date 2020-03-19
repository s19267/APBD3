using System.Collections;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<Student> _students;

        public MockDbService()
        {
            _students=new List<Student>
            {
                new Student{idStudent = 1,firstName = "Maciej",lastName = "Petrykowski"},
                new Student{idStudent = 2,firstName = "Barbara",lastName = "Bis"},
                new Student{idStudent = 3,firstName = "Daniel",lastName = "Petrykowski"},
            };
        }

        public IEnumerable<Student> GetStudents()
        {
            return _students;
        }
    }
}