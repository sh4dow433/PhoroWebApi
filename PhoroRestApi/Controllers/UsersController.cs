using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserAuth _userAuth;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IMapper mapper, IUserAuth userAuth)
        {
            _userAuth = userAuth;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> Get()
        {
            var results = _unitOfWork.Users.GetAll();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<UserReadDto>>(results));
        }

        [HttpGet("{id}")]
        public ActionResult<UserReadDto> GetById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var result = _unitOfWork.Users.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserReadDto>(result));
        }

        [HttpPost]
        public ActionResult Create(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<User>(userCreateDto);

            if (_userAuth.Register(user, userCreateDto.Password))
            {
                return NoContent();
            }
            return Conflict();
            //return CreatedAtRoute(nameof(GetById), new { Id = userReadDto.Id }, userReadDto);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult Update(int id, UserUpdateDto userUpdateDto)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var user = _unitOfWork.Users.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            //
            var user1 = HttpContext.User;
            int userId = int.Parse(user1.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != user.Id)
            {
                return Forbid();
            }

            try
            {
                _mapper.Map(userUpdateDto, user);
                _unitOfWork.SaveChanges();
            }
            catch(Exception)
            {
                return ValidationProblem();
            }
         
            return NoContent();
        }

        [HttpPut]
        [Authorize(Roles = "user")]
        [Route("password/{id}")]
        public ActionResult PasswordUpdate(int id, UserUpdatePasswordDto userUpdatePasswordDto)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var userFromRepo = _unitOfWork.Users.GetById(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            var user1 = HttpContext.User;
            int userId = int.Parse(user1.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != userFromRepo.Id)
            {
                return Forbid();
            }
            if (_userAuth.UpdatePassword(userFromRepo, userUpdatePasswordDto.Password))
            {
                return NoContent();
            } 
            else
            {
                return Conflict();
            }
        }



        [HttpPatch("{id}")]
        [Authorize(Roles = "user")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var userFromRepo = _unitOfWork.Users.GetById(id);
            if (userFromRepo == null)
            {
                return NotFound();
            }
            //
            var user1 = HttpContext.User;
            int userId = int.Parse(user1.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != userFromRepo.Id)
            {
                return Forbid();
            }
            var userToPatch = _mapper.Map<UserUpdateDto>(userFromRepo);
            patchDocument.ApplyTo(userToPatch, ModelState);
            
            if (TryValidateModel(userToPatch) == false)
            {
                return ValidationProblem(ModelState);
            }

            if(_unitOfWork.Users.GetByEmail(userToPatch.Email) != null)
            {
                return Conflict();
            }
            _mapper.Map(userToPatch, userFromRepo);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var user = _unitOfWork.Users.GetById(id);

            if (user == null)
            {
                return NotFound();
            }
            //
            var user1 = HttpContext.User;
            int userId = int.Parse(user1.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != user.Id)
            {
                return Forbid();
            }

            _unitOfWork.Users.Remove(user);
            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
