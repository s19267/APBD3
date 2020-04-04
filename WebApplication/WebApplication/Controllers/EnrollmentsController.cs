using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebApplication.DAL;
using WebApplication.DTOs;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateStudent(EnrollStudentResponse request)
        {
            using (SqlConnection con =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;

                //idStudies
                com.CommandText = "select IdStudy from Studies where name=@StudyName";
                com.Parameters.AddWithValue("StudyName", request.Studies);
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("Studia nie istnieja");
                }

                int idStudies = (int) dr["IdStudy"];
                dr.Close();

                // Enrollment
                com.CommandText =
                    "select * from Enrollment where Semester=1 and IdStudy=@idStudies and StartDate=(select max(StartDate) from Enrollment)";
                com.Parameters.AddWithValue("idStudies", idStudies);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    dr.Close();
                    com.CommandText =
                        "insert into Enrollment(IdEnrollment,Semester,IdStudy,StartDate) values ((select max(IdEnrollment)from Enrollment)+1,1,@idStudies,(SELECT CAST(GETDATE() AS DATE)))";
                    com.ExecuteNonQuery();
                    dr.Close();
                    com.CommandText =
                        "select IdEnrollment from Enrollment where Semester=1 and IdStudy=@idStudies and StartDate=(select max(StartDate) from Enrollment)";
                    dr = com.ExecuteReader();
                    dr.Read();
                }

                int idEnrollment = (int) dr["IdEnrollment"];
                int semesterEnrollment = (int) dr["Semester"];
                int idStudyEnrollment = (int) dr["IdStudy"];
                string startDateEnrollment = dr["StartDate"].ToString();
                dr.Close();

                //idStudent unique
                com.CommandText = "select IndexNumber from Student where IndexNumber=@IndexStudent";
                com.Parameters.AddWithValue("IndexStudent", request.IndexNumber);
                dr = com.ExecuteReader();
                if (dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("student z tym indexem juz istnieje");
                }

                dr.Close();

                com.CommandText =
                    "insert into Student(IndexNumber,FirstName,LastName,BirthDate,IdEnrollment) values (@IndexStudent,@FirstName,@LastName,Convert(varchar(40),@BirthDate,4) ,@idStudies)";
                com.Parameters.AddWithValue("FirstName", request.FirstName);
                com.Parameters.AddWithValue("LastName", request.LastName);
                request.BirthDate = request.BirthDate.Substring(6) + "-" + request.BirthDate.Substring(3, 2) + "-" +
                                    request.BirthDate.Substring(0, 2);
                com.Parameters.AddWithValue("BirthDate", request.BirthDate);
                com.ExecuteNonQuery();

                tran.Commit();
                return CreatedAtAction("createStudent",
                    new EnrollmentResponse
                    {
                        IdEnrollment = idEnrollment, IdStudy = idStudyEnrollment, Semester = semesterEnrollment,
                        StartDate = startDateEnrollment
                    });
            }
        }

        [HttpPost("promotions")]
        public IActionResult Promote(EnrollPromoteRequest enrollPromoteRequest)
        {
            using (SqlConnection con =
                new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;

                con.Open();
                var tran = con.BeginTransaction();
                com.Transaction = tran;
                
                //idStudy
                com.CommandText = "select IdStudy from Studies where name=@studiesName";
                com.Parameters.AddWithValue("studiesName", enrollPromoteRequest.studies);
                var dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("Studia nie istnieja");
                }
                int idStudies = (int) dr["IdStudy"];
                dr.Close();
                
                //Enrollments
                com.CommandText = "select IdEnrollment from Enrollment where Semester=@semester and IdStudy=@idStudy";
                com.Parameters.AddWithValue("semester", enrollPromoteRequest.semster);
                com.Parameters.AddWithValue("idStudy", idStudies);
                dr = com.ExecuteReader();
                if (!dr.Read())
                {
                    tran.Rollback();
                    return BadRequest("Studia nie istnieja");
                }
                
                
            }
            return 
        }
    }
}