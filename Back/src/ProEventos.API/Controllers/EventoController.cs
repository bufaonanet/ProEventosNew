using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {
        private readonly IEnumerable<Evento> _evento;
        public EventoController()
        {
            _evento = new Evento[]
            {
                new Evento {
                    Id = 1,
                    Tema ="Angular 12 e .net 5",
                    Local ="Guanhães",
                    Lote = "1º Lote",
                    DataEvento = DateTime.Now.AddDays(2).ToString("dd/MM/yyyy"),
                    QtdPessoas = 250
                },
                 new Evento {
                    Id = 2,
                    Tema ="Novidades .net core",
                    Local ="Belo Horizonte",
                    Lote = "2º Lote",
                    DataEvento = DateTime.Now.AddDays(5).ToString("dd/MM/yyyy"),
                    QtdPessoas = 50
                }
            };
        }


        [HttpGet]
        public IEnumerable<Evento> Get()
        {
            return _evento;
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> Get(int id)
        {
            return _evento
                .Where(e => e.Id == id);
        }
    }
}
