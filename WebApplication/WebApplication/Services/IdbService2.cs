using System;
using System.Collections;
using WebApplication.DTOs;

namespace WebApplication.Services
{
    public interface IdbService2
    {
        public IEnumerable getStudents();

        public Student updateStudent(Student student);

        public Student deleateStudent(String indexNumber);

        public Student createStudnt(Student student);

        public Enrollment promoteStudents(EnrollPromoteRequest promoteRequest);
    }
}