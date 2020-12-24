using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.BusinessLogic;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SellersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SellersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SellerReadInfoDto>> Get()
        {
            var repoResults = _unitOfWork.Sellers.GetAll();
            if (repoResults == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IEnumerable<SellerReadInfoDto>>(repoResults);
           
            return Ok(results);
        }

        //LOCATION
        [HttpGet]
        [Route("location/{location}")]
        public ActionResult<IEnumerable<SellerReadInfoDto>> GetByLocation(string location)
        {
            var repoResults = _unitOfWork.Sellers.GetAllByLocationLike(location);
            if (repoResults == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IEnumerable<SellerReadInfoDto>>(repoResults);

            return Ok(results);
        }

        //NAME
        [HttpGet]
        [Route("name/{name}")]
        public ActionResult<IEnumerable<SellerReadInfoDto>> GetAllByName(string name)
        {
            var repoResults = _unitOfWork.Sellers.GetAllByNameLike(name);
            if (repoResults == null)
            {
                return NotFound();
            }
            var results = _mapper.Map<IEnumerable<SellerReadInfoDto>>(repoResults);

            return Ok(results);
        }

        [HttpGet]
        [Route("info/{id}")]
        public ActionResult<SellerReadInfoDto> GetInfoById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var repoResult = _unitOfWork.Sellers.GetById(id);
            
            if (repoResult == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<SellerReadInfoDto>(repoResult);

            return Ok(result);
        }

        [HttpGet]
        [Route("full/{id}")]
        public ActionResult<SellerReadFullDto> GetFullById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var repoResult = _unitOfWork.Sellers.GetById(id);

            if (repoResult == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<SellerReadFullDto>(repoResult);
            
            return Ok(result);
        }



        [HttpGet]
        [Route("full/user/{userId}")]
        public ActionResult<SellerReadFullDto> GetFullByUserId(int userId)
        {
            if (userId < 0)
            {
                return NotFound();
            }

            var seller = _unitOfWork.Sellers.GetByUserId(userId);
            if (seller == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<SellerReadFullDto>(seller);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult Create(SellerCreateDto sellerCreateDto)
        {
            if (sellerCreateDto == null)
            {
                return Problem();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != sellerCreateDto.UserId)
            {
                return Forbid();
            }

            var seller = _mapper.Map<Seller>(sellerCreateDto);
            _unitOfWork.Sellers.Add(seller);
            _unitOfWork.SaveChanges();
            return NoContent();
        }

        //[HttpPut("{id}")]
        //public ActionResult Update(int id, SellerUpdateDto sellerUpdateDto)
        //{
        //    if (id < 0)
        //    {
        //        return NotFound();
        //    }
        //    var repoSeller = _unitOfWork.Sellers.GetById(id);

        //    if (repoSeller == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(sellerUpdateDto, repoSeller);
        //    _unitOfWork.SaveChanges();

        //    return NoContent();
        //}

        [HttpPatch("{id}")]
        [Authorize(Roles = "user,seller,admin")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<SellerUpdateDto> patchDocument)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var sellerFromRepo = _unitOfWork.Sellers.GetById(id);
            if (sellerFromRepo == null)
            {
                return NotFound();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != sellerFromRepo.UserId)
            {
                return Forbid();
            }

            var sellerToPatch = _mapper.Map<SellerUpdateDto>(sellerFromRepo);
            patchDocument.ApplyTo(sellerToPatch, ModelState);

            if (TryValidateModel(sellerToPatch) == false)
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(sellerToPatch, sellerFromRepo);
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
            var seller = _unitOfWork.Sellers.GetById(id);
            if (seller == null)
            {
                return NotFound();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != seller.UserId)
            {
                return Forbid();
            }

            _unitOfWork.Sellers.Remove(seller);
            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
