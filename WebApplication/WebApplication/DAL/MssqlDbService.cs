using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using WebApplication.Models;

namespace WebApplication.DAL
{
    public class MssqlDbService : IDbService
    {
        public IEnumerable<StudentModel> GetStudents()
        {
            var list=new List<StudentModel>();
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select * from student join enrollment on student.IdEnrollment=enrollment.IdEnrollment join studies on enrollment.IdStudy=studies.IdStudy";

                con.Open();
                var dr = com.ExecuteReader();
                while (dr.Read())
                {
                    StudentModel studentModel = new StudentModel
                    {
                        firstName = dr["FirstName"].ToString(),
                        lastName = dr["LastName"].ToString(),
                        indexNumber = dr["IndexNumber"].ToString(),
                        birthDate = dr["BirthDate"].ToString(),
                        semester = (int) dr["Semester"],
                        studyName = dr["Name"].ToString()
                        
                    };
                    list.Add(studentModel);
                }
            }

            return list;
        }

        public string GetSemester(int id)
        {
            String st="";
            using (var con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19267;Integrated Security=True"))
            using (var com = new SqlCommand())
            {
                com.Connection = con;
                com.CommandText = "select Semester from enrollment join student on enrollment.Idenrollment=student.Idenrollment where student.IndexNumber=@id";
                com.Parameters.AddWithValue("id", "s"+id.ToString());
                con.Open();
                var dr = com.ExecuteReader();
                
                while (dr.Read())
                {
                    st += st + dr["Semester"]+"\r\n";
                }
            }

            return st;
        }
    }
}