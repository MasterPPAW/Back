using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IUsersService usersService) : ControllerBase
    {
        // GET: api/<UsersController>
        [HttpGet]
        public async Task<List<UserDTO>> Get()
        {
            return await usersService.GetUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTO> GetById(int id)
        {
            return await usersService.GetUser(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
