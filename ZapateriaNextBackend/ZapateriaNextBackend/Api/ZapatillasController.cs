using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ZapateriaNextBackend.Models;

namespace ZapateriaNextBackend.Api
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ZapatillasController : ControllerBase
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public ZapatillasController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: api/Zapatillas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(contexto.Zapatillas.Where(x => x.Stock > 0 && x.Estado==1));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // GET: api/Zapatillas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(contexto.Zapatillas.SingleOrDefault(x => x.Id == id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // POST: api/Zapatillas
         [HttpPost]
        public async Task<IActionResult> Post([FromBody] Zapatilla entidad)
         {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Zapatillas.Add(entidad);
                    contexto.SaveChanges();
                    return CreatedAtAction(nameof(Get), new { id = entidad.Id }, entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        // PUT: api/Zapatillas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Zapatilla entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.Zapatillas.AsNoTracking().SingleOrDefault(z => z.Id == id) != null)
                {
                    //entidad.Id = id;
                    contexto.Zapatillas.Update(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // Delete: api/Zapatillas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var entidad = contexto.Zapatillas.FirstOrDefault(z => z.Id == id);
                if (entidad != null)
                {
                    entidad.Estado = 0;
                    contexto.Zapatillas.Update(entidad);
                    contexto.SaveChanges();
                    return Ok(entidad);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }

}
