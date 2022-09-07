using AppCitas.Service.Data;
using AppCitas.Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace AppCitas.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        //GET api/users

        [HttpGet] //Sirve para utilizar un verbo get
        public ActionResult<IEnumerable<AppUser>> GetUsers() //IEnumerable trae un arreglo enumerado
        {
            var users = _context.Users.ToList();

            return users;

        }

        //GET api/users/{id}
        [HttpGet("{id}")] //Sirve para utilizar un verbo get
        public ActionResult<AppUser> GetUsersById(int id) //IEnumerable trae un arreglo enumerado
        {
            var users = _context.Users.Find(id);

            return users;

        }

    }
}
