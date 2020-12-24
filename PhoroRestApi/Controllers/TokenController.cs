using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoroRestApi.BusinessLogic;
using PhoroRestApi.DataAccess;
using PhoroRestApi.DTOs;

namespace PhoroRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IUserAuth _userAuth;
        private readonly IUnitOfWork _unitOfWork;

        public TokenController(ITokenGenerator tokenGenerator, IUserAuth userAuth, IUnitOfWork unitOfWork)
        {
            _tokenGenerator = tokenGenerator;
            _userAuth = userAuth;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public ActionResult Login(UserLoginDto userLoginDto)
        {
            var user = _userAuth.Login(userLoginDto.Username, userLoginDto.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            bool isSeller = true;
            bool isAdmin = true;

            if (_unitOfWork.Sellers.GetByUsername(user.Username) == null)
            {
                isSeller = false;
            }

            if (_unitOfWork.Admins.GetByUsername(user.Username) == null)
            {
                isAdmin = false;
            }

            var token = _tokenGenerator.GenerateToken(user.Id, isSeller, isAdmin);

            return Ok(token);

        } 
    }
}
