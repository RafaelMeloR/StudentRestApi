using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlTypes;

namespace WebAPI.Models
{
    public class Application
    {
        public Response GetAllStudents()
        {
            DataTable dt= utilities.sql.Get("Select * from Students");
            Response response= new Response();
            List<Student>ListOfStudents = new List<Student>();

            if (dt.Rows.Count > 0)
            { 
                for(int i= 0; i<dt.Rows.Count;i++)
                {
                    Student student = new Student();
                    student.Id = (int)dt.Rows[i]["id"];
                    student.FirstName = (string)dt.Rows[i]["firstName"];
                    student.LastName = (string)dt.Rows[i]["lastName"];
                    student.Email = (string)dt.Rows[i]["email"];
                    
                    ListOfStudents.Add(student);
                }
               
            }

            if (ListOfStudents.Count > 0)
            {
                response.statusCode = 200;
                response.statusMessage = "Data retrievable is successful";
                response.students = ListOfStudents;
            }
            else 
            {
                response.statusCode = 100;
                response.statusMessage = "No data retrievable";
                response.students = null;
            }
            return response;
        }

        public Response GetStudent( int id) 
        {
            Response response = new Response();
            DataTable dt = utilities.sql.Get("Select * from students where id='"+id+"'");

            if (dt.Rows.Count > 0)
            {
                Student student = new Student();
                student.Id = (int)dt.Rows[0]["id"];
                student.FirstName = (string)dt.Rows[0]["firstName"];
                student.LastName = (string)dt.Rows[0]["lastName"];
                student.Email = (string)dt.Rows[0]["email"];

                response.statusCode = 200;
                response.statusMessage = "Data retrievable is successful";
                response.student = student;
            }
            else 
            {
                response.statusCode = 100;
                response.statusMessage = "No data retrievable";
            }
            return response;
        }

        public async Task<Response> AddStudentsAsync(Student student)
        {
            Response response=new Response();
            String query = "insert into Students values('"+student.FirstName+ "', '"+student.LastName+ "', '"+student.Email+"')";
            bool state=await utilities.sql.Set(query);
             
            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data entry Successfully";
                response.student = student;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data inserted";
                response.student = null;
            }

            return response;

        }

        public async Task<Response> UpdateStudentsAsync(Student student)
        {
            Response response = new Response();
            String query = "Update Students set firstName='" + student.FirstName + "',lastName= '" + student.LastName + "', email='" + student.Email + "' where id="+(int)student.Id+"";
            await utilities.sql.Set(query);

            

            bool state = await utilities.sql.Set(query);

            if (state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data updated Successfully";
                response.student = student;
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data updated";
                response.student = null;
            }

            return response;

        }

        public async Task<Response> DeleteStudentsAsync(int id)
        {
            Response response = new Response();
            String query = "Delete from Students where id=" +id + "";
            bool state= await utilities.sql.Set(query);

            if(state)
            {
                response.statusCode = 200;
                response.statusMessage = "Data deleted Successfully";
            }
            else
            {
                response.statusCode = 100;
                response.statusMessage = "No data deleted";
            }
            

            return response;

        }
    }
}
