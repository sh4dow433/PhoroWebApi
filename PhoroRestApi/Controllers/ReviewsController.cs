using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;

namespace PhoroRestApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReviewsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //[HttpGet]
        //public ActionResult<IEnumerable<ReviewReadDto>> Get()
        //{
        //    throw new NotImplementedException();
        //    ///// nu e necesar
        //}

        [HttpPost]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult Create(ReviewCreateDto reviewCreateDto)
        {
            var review = _mapper.Map<Review>(reviewCreateDto);
           
            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != reviewCreateDto.FromUserId)
            {
                return Forbid();
            }

            _unitOfWork.Reviews.Add(review);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult Remove(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var review = _unitOfWork.Reviews.GetById(id);
            if (review == null)
            {
                return NotFound();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != review.FromUserId)
            {
                return Forbid();
            }

            _unitOfWork.Reviews.Remove(review);
            _unitOfWork.SaveChanges();
            return NoContent();
        }
    }
}
