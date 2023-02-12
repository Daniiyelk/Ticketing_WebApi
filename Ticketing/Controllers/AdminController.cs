using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Ticketing.Core.DTOs;
using Ticketing.Core.Services.Interfaces;
using Ticketing.DataLayer.Entities.Admin;

namespace Ticketing.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminServices _adminServices;
        private readonly IMapper _mapper;

        public AdminController(IAdminServices adminServices, IMapper mapper)
        {
            _adminServices = adminServices;
            _mapper = mapper;
        }

        #region Get

        //Get All admins
        [HttpGet]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _adminServices.GetAdminsAsync();
            return Ok(admins);
        }

        //Get specified admin by id , with or without answer list
        [HttpGet("{id}",Name = "GetAdminById")]
        public async Task<IActionResult> GetAdminById(int id, bool includeAnswers = false)
        {
            var admin = await _adminServices.GetAdminByIdAsync(id, includeAnswers);

            if (admin == null)
                return NotFound();

            return Ok(admin);
        }

        #endregion

        #region Post

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminDto adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            if (adminDto.Email == null || adminDto.Password == null)
                return BadRequest();
            
            //Mapping adminCreationDto to Admin By Mapper
            var admin = _mapper.Map<Admin>(adminDto);

            await _adminServices.AddAdminAsync(admin);

            return CreatedAtRoute("GetAdminById", new { id = admin.id }, admin);
        }

        #endregion

        #region Put
        
        //update All values
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdmin(int id, AdminDto adminDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            if (adminDto.Email == null || adminDto.Password == null)
                return BadRequest();

            //Get Admin by input Id
            var admin = await _adminServices.GetAdminByIdAsync(id);

            if (admin == null)
                return NotFound();

            //Store new values in admin
            admin.Email = adminDto.Email;
            admin.Password = adminDto.Password;

            await _adminServices.UpdateAdmin(admin);
            
            return NoContent();
        }

        #endregion

        #region Patch
        
        //update partially
        [HttpPatch("{id}")]
        public async Task<IActionResult> PartiallyUpdateAdmin(int id, JsonPatchDocument<AdminDto> patchDocument)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //Get Admin by input Id
            var admin = await _adminServices.GetAdminByIdAsync(id);

            if (admin == null)
                return NotFound();
            
            //declare new instance for using in 'applyTo' function
            var adminToPatch = new AdminDto()
            {
                Email = admin.Email,
                Password = admin.Password
            };
            
            //using this function to compare old and new values and store init
            patchDocument.ApplyTo(adminToPatch,ModelState);

            //store new values
            admin.Email = adminToPatch.Email;
            admin.Password = adminToPatch.Password;

            await _adminServices.UpdateAdmin(admin);
            
            return NoContent();
        }


        #endregion

        #region Delete

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var admin = await _adminServices.GetAdminByIdAsync(id);

            if (admin == null)
                return NotFound();

            await _adminServices.DeleteAdmin(admin);
            
            //Todo Add Logging
            
            return NoContent();
        }

        #endregion
    }
}