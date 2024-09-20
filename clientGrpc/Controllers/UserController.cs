﻿using clientGrpc.DTOs;
using clientGrpc.Services;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace clientGrpc.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<IGreeterService> _logger;

        public UserController(IUserService userService, ILogger<IGreeterService> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var response = await _userService.GetUserGrpc(id);

                _logger.LogInformation("[UserController][GetUserById]: {message}", response.ToString());

                var responseDTO = new mainDTO { Content = response };

                return Ok(responseDTO);
            }

            catch (RpcException ex)
            {
                var responseDTO = new mainDTO { Content = ex.Status.Detail };

                _logger.LogError("[UserController][GetUserById]: {error}", ex.Message);

                if (ex.StatusCode.Equals(Grpc.Core.StatusCode.Unavailable))
                {
                    return StatusCode(500, responseDTO);
                }

                return NotFound(responseDTO);
            }
        }
    }
}