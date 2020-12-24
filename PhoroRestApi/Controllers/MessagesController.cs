using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MessagesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<MessageReadDto>> Get()
        {
            var results = _unitOfWork.Messages.GetAll();
            if (results == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(results));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<MessageReadDto> GetById(int id)
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

            return Ok(_mapper.Map<MessageReadDto>(result));
        }

        [HttpGet]
        [Route("from/{userId}")]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult<IEnumerable<MessageReadDto>> GetFromUserId(int userId)
        {
            if (userId < 0)
            {
                return NotFound();
            }
            //check if the user has permission
            var user = HttpContext.User;
            int userIdFromHeader = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userIdFromHeader != userId)
            {
                return Forbid();
            }


            var results = _unitOfWork.Messages.GetAllFromId(userId);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(results));
        }

        [HttpGet]
        [Route("to/{userId}")]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult<IEnumerable<MessageReadDto>> GetToUserId(int userId)
        {
            if (userId < 0)
            {
                return NotFound();
            }
            //check if the user has permission
            var user = HttpContext.User;
            int userIdFromHeader = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userIdFromHeader != userId)
            {
                return Forbid();
            }

            var results = _unitOfWork.Messages.GetAllToId(userId);
            if (results == null)
            {
                return NotFound();
            }


            return Ok(_mapper.Map<IEnumerable<MessageReadDto>>(results));
        }

        [HttpPost]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult Create(MessageCreateDto msgCreateDto)
        {
            if (msgCreateDto == null)
            {
                return Problem();
            }
            var msg = _mapper.Map<Message>(msgCreateDto);
            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != msgCreateDto.FromUserId)
            {
                return Forbid();
            }

            _unitOfWork.Messages.Add(msg);
            _unitOfWork.SaveChanges();
            return NoContent();
        }

        // HTTP PUT and PATCH arent needed

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            throw new NotImplementedException();
            // BUG: if the sender deletes a msg, that msg gets deleted 
            // from the receivers inbox and the other way around
            if (id < 0)
            {
                return NotFound();
            }

            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
            {
                return NotFound();
            }
            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != message.ToUserId)
            {
                return Forbid();
            }

            _unitOfWork.Messages.Remove(message);
            return NoContent();
        }
    }
}
