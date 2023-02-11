using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Entities.User;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;
        private readonly IMapper _mapper;

        public UserController(IUserServices userServices, IMapper mapper)
        {
            _userServices = userServices;
            _mapper = mapper;
        }

        #region Get

        //Get all user information
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _userServices.GetUsersAsync();
            return Ok(result);
        }

        //Get Specified User with or without tickets
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserById(int id, bool includeTicket = false)
        {
            var user = await _userServices.GetUserByIdAsync(id, includeTicket);

            if (user == null)
                return NotFound(user);

            return Ok(user);
        }

        #endregion

        #region Post

        //Add User 
        [HttpPost]
        public async Task<IActionResult> AddUser(UserCreationDto userCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // Create user by mapping
            var user = _mapper.Map<User>(userCreationDto);

            //Get userId
            var userid = await _userServices.AddUserAsync(user);

            //return url after creating the user
            return CreatedAtRoute("GetUserById", new
            {
                id = userid
            }, user);
        }

        #endregion

        #region Put

        // Update User With Put
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserCreationDto userCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            // Get User By Id
            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            // Import New Values
            user.Email = userCreationDto.Email;
            user.Name = userCreationDto.Name;
            user.Password = userCreationDto.Password;

            await _userServices.UpdateUserAsync(user);

            return NoContent();
        }

        #endregion

        #region Patch

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateUser(int id, JsonPatchDocument<UserCreationDto> patchDocument)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            //Create an new instance to store old values and then apply to new inputs
            var userToPatch = new UserCreationDto()
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password
            };

            patchDocument.ApplyTo(userToPatch, ModelState);

            if (!ModelState.IsValid)
                return BadRequest();

            //validate new values
            if (!TryValidateModel(patchDocument))
                return BadRequest();

            user.Email = userToPatch.Email;
            user.Name = userToPatch.Name;
            user.Password = userToPatch.Password;

            await _userServices.UpdateUserAsync(user);

            return NoContent();
        }

        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userServices.GetUserByIdAsync(id);

            if (user == null)
                return NotFound();

            await _userServices.DeleteUserAsync(user);
            
            return NoContent();
        }

        #endregion
    }
}