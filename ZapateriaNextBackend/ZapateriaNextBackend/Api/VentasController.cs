using System;
using System.Collections;
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
    public class VentasController : ControllerBase
    {

        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public VentasController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }

        // GET: api/Ventas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var Ventas = contexto.Ventas.Include(e => e.Empleado).Where(x => x.Estado != 0).OrderByDescending(f => f.Fecha).Take(10);
                return Ok(Ventas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        
        /*
        // GET: api/Ventas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var venta = contexto.Ventas.Include(e => e.Empleado).SingleOrDefault(x => x.Id == id);
                //var venta = contexto.DetalleVentas.Include(v => v.Venta).ThenInclude(e => e.Empleado).Include(z => z.Zapatilla).Where(v => v.IdVenta == id);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        */

        // GET: api/Ventas/Detalle/5
        [HttpGet("{Detalle}/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                //var venta = contexto.Ventas.Include(e => e.Empleado).SingleOrDefault(x => x.Id == id);
                var venta = contexto.DetalleVentas.Include(v => v.Venta).ThenInclude(e => e.Empleado).Include(z => z.Zapatilla).Where(v => v.IdVenta == id);
                return Ok(venta);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/Ventas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] Venta entidad)
        {
            try
            {

                if (ModelState.IsValid && contexto.Ventas.AsNoTracking().SingleOrDefault(z => z.Id == id) != null)
                {    
                    //entidad.Id = id;   
                    contexto.Ventas.Update(entidad);
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

        // Delete: api/Ventas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               
                var entidad = contexto.Ventas.FirstOrDefault(z => z.Id == id);
                if (entidad != null)
                {
                    entidad.Estado = 0;
                    contexto.Ventas.Update(entidad);
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
