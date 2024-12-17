using LibrarieModele.DTOs;

using Microsoft.AspNetCore.Mvc;

using NivelService.Abstraction;


namespace FitnessAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController(IUsersService usersService) : ControllerBase
    {
        private const string PostBaseSuccessMessage = "Successfully registered";

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<List<UserDTO>> Get()
        {
            return await usersService.GetUsers();
        }

        [HttpGet("deleted")]
        public async Task<List<UserDTO>> GetDeleted()
        {
            return await usersService.GetUsersDeleted();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<UserDTO> GetById(int id)
        {
            return await usersService.GetUser(id);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserDTO userDTO)
        {
            await usersService.CreateUser(userDTO);

            string postMessage = PostBaseSuccessMessage + " " + userDTO.Name;
            return CreatedAtAction(nameof(Get), new { id = userDTO.UserId }, postMessage);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] UserDTO userDTO, int id)
        {
            return Ok(await usersService.UpdateUser(userDTO, id));
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await usersService.DeleteUser(id);
            return NoContent();
        }
    }
}
