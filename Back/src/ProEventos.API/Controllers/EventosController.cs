using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Extensions;
using ProEventos.Application;
using ProEventos.Application.DTOs;

namespace ProEventos.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {
        private readonly IEventoService _service;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IAccountService _accountService;

        public EventosController(
            IEventoService service,
            IWebHostEnvironment hostEnvironment,
            IAccountService accountService)
        {
            _service = service;
            _hostEnvironment = hostEnvironment;
            _accountService = accountService;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await _service.GetAllEventosAsync(User.GetUserId());
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var eventos = await _service.GetEventoByIdAsync(User.GetUserId(), id, true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{tema}/tema")]
        public async Task<IActionResult> GetByTema(string tema)
        {
            try
            {
                var eventos = await _service.GetAllEventosByTemaAsync(User.GetUserId(), tema, true);
                if (eventos == null) return NoContent();

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar eventos. Erro: {ex.Message}");
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Post(EventoDto model)
        {
            try
            {
                var eventos = await _service.AddEvento(User.GetUserId(), model);
                if (eventos == null) return BadRequest("Não foi possível adicionar evento.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{eventoId}")]
        public async Task<IActionResult> UploadImage(int eventoId)
        {
            try
            {
                var eventoDto = await _service.GetEventoByIdAsync(User.GetUserId(), eventoId, false);
                if (eventoDto == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(eventoDto.ImagemURL);
                    eventoDto.ImagemURL = await SaveImage(file);
                }

                var eventoRetorno = await _service.UpdateEvento(User.GetUserId(), eventoId, eventoDto);

                return Ok(eventoRetorno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar evento. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EventoDto model)
        {
            try
            {
                var eventos = await _service.UpdateEvento(User.GetUserId(), id, model);
                if (eventos == null) return BadRequest("Não foi possível autalizar o evento.");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar o evento. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return await _service.DeleteEvento(User.GetUserId(), id) ?
                    Ok("Deletado") :
                    BadRequest("Evento não deletado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar deletar o evento. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            var imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                .Take(10)
                .ToArray()).Replace(" ", "-");

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"resources\images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources\Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
