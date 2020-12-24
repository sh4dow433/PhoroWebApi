using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    public class AlbumsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoLoader _photoLoader;

        public AlbumsController(IUnitOfWork unitOfWork, IMapper mapper, IPhotoLoader photoLoader)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoLoader = photoLoader;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumReadDto>> Get()
        {
            var results = _unitOfWork.Albums.GetAll();
            if (results == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AlbumReadDto>>(results));
        }

        [HttpGet]
        [Route("seller/{sellerId}")]
        public ActionResult<IEnumerable<AlbumReadDto>> Get(int sellerId)
        {
            var results = _unitOfWork.Albums.GetAllByOwnersId(sellerId);
            if (results == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<AlbumReadDto>>(results));
        }


        [HttpGet("{id}")]
        public ActionResult<AlbumReadDto> GetById(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }
            var resultFromRepo = _unitOfWork.Albums.GetById(id);
            if (resultFromRepo == null)
            {
                return NotFound();
            }
            var result = _mapper.Map<AlbumReadDto>(resultFromRepo);
            result.PhotosIds = new List<int>();
           
            foreach (var photoAlbum in resultFromRepo.AlbumPhotos)
            {
                result.PhotosIds.Add(photoAlbum.PhotoId);
            }
            return Ok(result);

        }

        [HttpPost]
        [Authorize(Roles = "seller,admin")]
        public ActionResult Create(AlbumCreateDto albumCreateDto)
        {
            if (albumCreateDto == null)
            {
                return Problem();
            }

            var userIdFromRepo = _unitOfWork.Sellers.GetById(albumCreateDto.SellerId).UserId;

            //Check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != userIdFromRepo)
            {
                return Forbid();
            }

            var repoAlbum = _mapper.Map<Album>(albumCreateDto);

            _unitOfWork.Albums.Add(repoAlbum);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpPost]
        [Route("photo/{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult AddPhotoToAlbum(int id, AddRemovePhotoToAlbumDto photoToAdd)
        {
            if (id < 0)
            {
                return NotFound();
            }

            if (photoToAdd == null)
            {
                return Problem();
            }
            var photo = _unitOfWork.Photos.GetById(photoToAdd.PhotoId);
            if (photo == null)
            {
                return NotFound();
            }
            //Check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != photo.UserId)
            {
                return Forbid();
            }

            if (_unitOfWork.Albums.AddPhotoToAlbum(id, photo) == true)
            {
                _unitOfWork.SaveChanges();
                return NoContent();
            }
            else
            {
                _unitOfWork.Dispose();
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("photo/{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult DeletePhotoFromAlbum(int id, AddRemovePhotoToAlbumDto photoToRemove)
        {
            if (id < 0)
            {
                return NotFound();
            }

            if (photoToRemove == null)
            {
                return Problem();
            }
            var photo = _unitOfWork.Photos.GetById(photoToRemove.PhotoId);
            if (photo == null)
            {
                return NotFound();
            }

            //Check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != photo.UserId)
            {
                return Forbid();
            }
            if (_unitOfWork.Albums.RemovePhotoFromAlbum(id, photo))
            {
                _unitOfWork.SaveChanges();
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        //[HttpPut("{id}")]
        //[Authorize(Roles = "seller,admin")]
        //public ActionResult Update(int id, AlbumUpdateDto albumUpdateDto)
        //{
        //    if (id < 0)
        //    {
        //        return NotFound();
        //    }

        //    var album = _unitOfWork.Albums.GetById(id);
        //    if (album == null)
        //    {
        //        return NotFound();
        //    }

        //    _mapper.Map(albumUpdateDto, album);
        //    _unitOfWork.SaveChanges();

        //    return NoContent();
        //}

        [HttpPatch("{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult PartialUpdate(int id, JsonPatchDocument<AlbumUpdateDto> patchDocument)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var albumFromRepo = _unitOfWork.Albums.GetById(id);
            if (albumFromRepo == null)
            {
                return NotFound();
            }

            //Check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != albumFromRepo.SellerId)
            {
                return Forbid();
            }

            var albumToPatch = _mapper.Map<AlbumUpdateDto>(albumFromRepo);
            patchDocument.ApplyTo(albumToPatch, ModelState);

            if (TryValidateModel(albumToPatch) == false)
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(albumToPatch, albumFromRepo);
            _unitOfWork.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "seller,admin")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
            {
                return NotFound();
            }

            var album = _unitOfWork.Albums.GetById(id);
            if (album == null)
            {
                return NotFound();
            }

            //Check if the user has permission
            var user = HttpContext.User;
            int userId = int.Parse(user.Claims.FirstOrDefault(c => c.Type == "Id").Value);
            if (userId != album.SellerId)
            {
                return Forbid();
            }

            _unitOfWork.Albums.Remove(album);
            _unitOfWork.SaveChanges();

            return NoContent();
        }
    }
}
