using AppCitas.Service.Data;
using AppCitas.Service.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public  async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() //IEnumerable trae un arreglo enumerado
        {
            //var users = _context.Users.ToList();

            return await _context.Users.ToListAsync();

        }

        //GET api/users/{id}
        [HttpGet("{id}")] //Sirve para utilizar un verbo get
        public async Task<ActionResult<AppUser>> GetUsersById(int id) //IEnumerable trae un arreglo enumerado
        {
            //var users = _context.Users.Find(id);

            return  await _context.Users.FindAsync(id);

        }

    }
}
