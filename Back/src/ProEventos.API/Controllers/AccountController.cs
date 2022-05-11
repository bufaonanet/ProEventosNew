using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application;
using ProEventos.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(
            IAccountService accountService,
            ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;
        }

        [HttpGet("get-user")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userName = User.GetUserName();
                var user = await _accountService.GetUserByUserNameAsync(userName);

                return Ok(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar recuperar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserDto userDto)
        {
            try
            {
                if (await _accountService.UserExists(userDto.UserName)) return BadRequest("Usuário já existe");

                var user = await _accountService.CreateAccountAsync(userDto);
                if (user != null) return Ok(user);

                return BadRequest("Usuário não criado, tente novamente mais tarde!");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar registrar Usuário. Erro: {ex.Message}");
            }
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLoginDto userLogin)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(userLogin.Username);
                if (user == null) return Unauthorized("Usuário ou Senha estão errados.");

                var result = await _accountService.CheckUserPasswordAsync(user, userLogin.Password);
                if (result.Succeeded) Unauthorized("Usuário ou Senha estão errados.");

                return Ok(new
                {
                    userName = user.UserName,
                    primeiroNome = user.PrimeiroNome,
                    token = _tokenService.CreateToken(user).Result
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar realizar Login. Erro: {ex.Message}");
            }
        }

        [HttpPost("update-user")]      
        public async Task<IActionResult> UpdateUser(UserUpdateDto userUpdateDto)
        {
            try
            {
                var user = await _accountService.GetUserByUserNameAsync(User.GetUserName());
                if (user == null) return Unauthorized("Usuário Inválido.");

                var userToReturn = await _accountService.UpdateAccount(userUpdateDto);
                if (userToReturn == null) return NoContent();

                return Ok(userToReturn);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao tentar atualizar usuário. Erro: {ex.Message}");
            }
        }
    }
}
