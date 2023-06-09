using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class StudentsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public StudentsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("GetAllStudents")]

        public Response GetAllStudents()
        { 
            Response response = new Response();
            Application apl= new Application();
            response = apl.GetAllStudents();
            return response;
        }

        [HttpGet]
        [Route("GetStudentById/{id}")]

        public Response GetStudentsById(int id)
        {
            Response response = new Response();
            Application apl = new Application();
            response = apl.GetStudent(id);
            return response;
        }

        [HttpPost]
        [Route("AddStudentsAsync")]
        public Task<Response> AddStudentsAsync(Student student)
        {  
            Application apl = new Application();
            Task<Response> response=apl.AddStudentsAsync(student);
            return response;

        }

        [HttpPut]
        [Route("UpdateStudentsAsync")]
        public Task<Response> UpdateStudentsAsync(Student student)
        {
            Application apl = new Application();
            Task<Response> response = apl.UpdateStudentsAsync(student);
            return response;

        }

        [HttpDelete]
        [Route("DeleteStudentsAsync")]
        public Task<Response> DeleteStudentsAsync(int id)
        {
            Application apl = new Application();
            Task<Response> response = apl.DeleteStudentsAsync(id);
            return response;

        }
    }
}
