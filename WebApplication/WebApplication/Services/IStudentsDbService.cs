using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;

namespace WebApplication.Services
{
    public interface IStudentsDbService
    {
        public EnrollmentResponse CreateStudent(EnrollStudentResponse request);
        public EnrollmentResponse PromoteStudents(EnrollPromoteRequest enrollPromoteRequest);
    }
}