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

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Roles = "admin")]
        public ActionResult<IEnumerable<AdminReadDto>> Get()
        {
            var results = _unitOfWork.Admins.GetAll();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AdminReadDto>>(results));
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin")]
        public ActionResult<AdminReadDto> GetById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var result = _unitOfWork.Admins.GetById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MessageReadDto>(result));
        }
        // DEL/UPDATE/CREATE not needed
    }
}
