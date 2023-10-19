using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Data;
using dotnet7.Dtos.User;
using dotnet7.Models;
using Microsoft.AspNetCore.Mvc;
using webapi7.Dtos.User;

namespace webapi7.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponce<int>>> Register(UserRegisterDto request)
        {
            var responce = await _authRepo.Register(
                new User { Username = request.Username }, request.Password
            );
            if (!responce.Success)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponce<int>>> Login(UserLoginDto request)
        {
            var responce = await _authRepo.Login(request.Username, request.Password);
            if (!responce.Success)
            {
                return BadRequest(responce);
            }
            return Ok(responce);
        }
    }
}