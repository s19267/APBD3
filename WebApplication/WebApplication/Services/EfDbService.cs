using System;
using System.Collections;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DTOs;

namespace WebApplication.Services
{
    public class EfDbService : IdbService2
    {
        private readonly s19267Context _dbContext;

        public EfDbService(s19267Context s19267Context)
        {
            _dbContext = s19267Context;
        }

        public IEnumerable getStudents()
        {
            return _dbContext.Student.ToList();
        }

        public Student updateStudent(Student student)
        {
            var s=_dbContext.Student.Where(s => s.IndexNumber == student.IndexNumber);
            foreach (var VARIABLE in s)
            {
                VARIABLE.FirstName = student.FirstName;
                VARIABLE.Password = student.Password;
                VARIABLE.BirthDate = student.BirthDate;
                VARIABLE.IdEnrollment = student.IdEnrollment;
                VARIABLE.LastName = student.LastName;
            }

            _dbContext.SaveChanges();
            s = _dbContext.Student.Where(s => s.IndexNumber == student.IndexNumber);
            foreach (var VARIABLE in s)
            {
                return VARIABLE;
            }

            return null;
        }

        public Student deleateStudent(string indexNumber)
        {
            var s = _dbContext.Student.Where(s => s.IndexNumber == indexNumber);
            Student tmp = null;
            foreach (var VARIABLE in s)
            {
                tmp = VARIABLE;
                _dbContext.Student.Remove(VARIABLE);
            }

            _dbContext.SaveChanges();
            
            return tmp;
        }

        public Student createStudnt(Student student)
        {
            _dbContext.Student.Add(student);
            _dbContext.SaveChanges();
            var s=_dbContext.Student.Where(s => s.IndexNumber == student.IndexNumber).First();
            return s;
        }

        public Enrollment promoteStudents(EnrollPromoteRequest promoteRequest)
        {
            var study = _dbContext.Studies.Where(s => s.Name == promoteRequest.studies).FirstOrDefault();
            var enrollment = _dbContext.Enrollment.Where(e => e.Semester == promoteRequest.semester)
                .Where(e => e.IdStudy == study.IdStudy).First();
            var newEnrollment = _dbContext.Enrollment.Where(e => e.Semester == promoteRequest.semester + 1)
                .Where(e => e.IdStudy == study.IdStudy).First();
            if (newEnrollment == null)
            {
                var maxIdEnrollment = _dbContext.Enrollment.Max(e => e.IdEnrollment);
                _dbContext.Enrollment.Add(new Enrollment
                {
                    IdEnrollment = maxIdEnrollment + 1,
                    IdStudy = study.IdStudy,
                    Semester = enrollment.Semester + 1,
                    StartDate = DateTime.Now
                });
                newEnrollment = _dbContext.Enrollment.Where(e => e.Semester == promoteRequest.semester + 1)
                    .Where(e => e.IdStudy == study.IdStudy).First();
            }

            var students = _dbContext.Student.Where(s => s.IdEnrollment == enrollment.IdEnrollment);
            foreach (var s in students)
            {
                s.IdEnrollment = newEnrollment.IdEnrollment;
            }

            return newEnrollment;
        }
    }
    
    
}