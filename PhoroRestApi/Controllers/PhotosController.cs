using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.BusinessLogic;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;
using PhoroRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoLoader _photoLoader;
        private readonly IPhotoSaver _photoSaver;
        private readonly IPhotoDeleter _photoDeleter;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PhotosController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoLoader photoLoader, IPhotoSaver photoSaver, IPhotoDeleter photoDeleter)
        {
            _photoLoader = photoLoader;
            _photoSaver = photoSaver;
            _photoDeleter = photoDeleter;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PhotoReadDto>> Get()
        {
            var repoResults = _unitOfWork.Photos.GetAll();
            List<PhotoReadDto> results = new List<PhotoReadDto>();

            if (repoResults == null)
            {
                return NotFound();
            }

            Parallel.ForEach(repoResults, (currentRepoResult) =>
            {
                var newPhotoReadDto = _mapper.Map<PhotoReadDto>(currentRepoResult);
                newPhotoReadDto.ImgFile = _photoLoader.LoadPhoto(currentRepoResult.DiskName);
                results.Add(newPhotoReadDto);
            });

            return Ok(results);
        }

        [HttpGet]
        [Route("from/{userId}")]
        public ActionResult<IEnumerable<PhotoReadDto>> GetAllFromUser(int userId)
        {
            var repoResults = _unitOfWork.Photos.GetAllByOwnersId(userId);
            List<PhotoReadDto> results = new List<PhotoReadDto>();

            if (repoResults == null)
            {
                return NotFound();
            }

            Parallel.ForEach(repoResults, (currentRepoResult) =>
            {
                var newPhotoReadDto = _mapper.Map<PhotoReadDto>(currentRepoResult);
                newPhotoReadDto.ImgFile = _photoLoader.LoadPhoto(currentRepoResult.DiskName);
                results.Add(newPhotoReadDto);
            });

            return Ok(results);
        }

        [HttpGet("{id}")]
        public ActionResult<PhotoReadDto> GetById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var repoPhoto = _unitOfWork.Photos.GetById(id);
            if (repoPhoto == null)
            {
                return NotFound();
            }

            var result = _mapper.Map<PhotoReadDto>(repoPhoto);
            result.ImgFile = _photoLoader.LoadPhoto(repoPhoto.DiskName);
            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "admin,seller")]
        public ActionResult Create(PhotoCreateDto photoCreateDto)
        {
            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != photoCreateDto.UserId)
            {
                return Forbid();
            }

            var photo = _mapper.Map<Photo>(photoCreateDto);
            try
            {
                photo.DiskName = _photoSaver.SavePhoto(Convert.FromBase64String(photoCreateDto.ImgFile));
            }
            catch (Exception)
            {
                return Problem();
            }

            _unitOfWork.Photos.Add(photo);
            _unitOfWork.SaveChanges();

            return NoContent();
            //return CreatedAtRoute();
        }


        //[HttpPut("{id}")]
        //public ActionResult Update(int id, PhotoUpdateDto photoUpdateDto)
        //{
        //    if (id < 0)
        //    {
        //        return NotFound();
        //    }

        //    var photo = _unitOfWork.Users.GetById(id);
        //    if (photo == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        _mapper.Map(photoUpdateDto, photo);
        //        _unitOfWork.SaveChanges();
        //    }
        //    catch(Exception)
        //    {
        //        return ValidationProblem();
        //    }

        //    return NoContent();
        //}
        

        [HttpPatch("{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<PhotoUpdateDto> patchDocument)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var photoFromRepo = _unitOfWork.Photos.GetById(id);

            if (photoFromRepo == null)
            {
                return NotFound();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != photoFromRepo.UserId)
            {
                return Forbid();
            }

            var photoToPatch = _mapper.Map<PhotoUpdateDto>(photoFromRepo);
            patchDocument.ApplyTo(photoToPatch, ModelState);

            if(TryValidateModel(photoToPatch) == false)
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(photoToPatch, photoFromRepo);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult Delete(int id)
        {
            var photo = _unitOfWork.Photos.GetById(id);

            if (photo == null)
            {
                return NotFound();
            }

            //check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != photo.UserId)
            {
                return Forbid();
            }

            if (_photoDeleter.DeletePhoto(photo.DiskName))
            {
                _unitOfWork.Photos.Remove(photo);
                _unitOfWork.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }
    }
}
