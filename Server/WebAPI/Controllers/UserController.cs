using DTOs;
using Microsoft.AspNetCore.Mvc;
using ProjectEntities;
using RepositoryContracts;


namespace WebAPI.Controllers;

[ApiController]                       // enables API conventions (auto 400 on invalid model, body binding)
[Route("users")]  

public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
        
    }
    
    //get
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDTO>> Get(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound();
        }
        var DTO = new UserDTO
        {
            Id = user.Id,
            Username = user.Username
        };
        return Ok(DTO);
    }
    
    //post
    [HttpPost]
    public async Task<ActionResult<UserDTO>> Post(
        [FromBody] UserCreateDTO dto)
    {
        User created = await _userRepository.AddAsync(
            new User
            {   
                Id = 0,
                Username = dto.Username,
                Password = dto.Password
            }
        );
        var result = new UserDTO
        {
            Username = created.Username,
            Id = created.Id
        };
        return CreatedAtAction(nameof(Get), new { id = created.Id }, result);

    }
    
    //put
    
    //delete
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userRepository.DeleteAsync(id);
        return NoContent();
    }
    
}