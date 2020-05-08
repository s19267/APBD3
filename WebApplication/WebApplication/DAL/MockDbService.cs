using System.Collections;
using System.Collections.Generic;
using WebApplication.Models;

namespace WebApplication.DAL
{
    public class MockDbService : IDbService
    {
        private static IEnumerable<StudentModel> _students;

        public MockDbService()
        {
            _students=new List<StudentModel>
            {
                new StudentModel{idStudent = 1,firstName = "Maciej",lastName = "Petrykowski"},
                new StudentModel{idStudent = 2,firstName = "Barbara",lastName = "Bis"},
                new StudentModel{idStudent = 3,firstName = "Daniel",lastName = "Petrykowski"},
            };
        }

        public IEnumerable<StudentModel> GetStudents()
        {
            return _students;
        }

        public string GetSemester(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}