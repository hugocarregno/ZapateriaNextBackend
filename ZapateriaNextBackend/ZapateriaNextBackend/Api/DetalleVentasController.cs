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
    public class DetalleVentasController : ControllerBase
    {
        private readonly DataContext contexto;
        private readonly IConfiguration config;

        public DetalleVentasController(DataContext contexto, IConfiguration config)
        {
            this.contexto = contexto;
            this.config = config;
        }
        
        // GET: api/DetalleVentas
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var detalles = contexto.DetalleVentas.Include(v => v.Venta).ThenInclude(e => e.Empleado).Include(z => z.Zapatilla).Where(x => x.Cantidad != 0);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/DetalleVentas/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var detalles = contexto.DetalleVentas.Include(v => v.Venta).ThenInclude(e => e.Empleado).Include(z => z.Zapatilla).SingleOrDefault(x => x.Id == id);
                return Ok(detalles);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/DetalleVentas
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] List<DetalleVenta> listado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    contexto.Ventas.Add(listado[0].Venta);
                    contexto.SaveChanges();
                    
                    foreach(DetalleVenta dv in listado)
                    {
                        dv.IdVenta = listado[0].Venta.Id;
                        
                        contexto.Zapatillas.Update(dv.Zapatilla);
                        contexto.SaveChanges();
                    }

                    contexto.DetalleVentas.AddRange(listado);
                    contexto.SaveChanges();

                    return Ok(listado[0].Id);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // PUT: api/DetalleVentas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, DetalleVenta entidad)
        {
            try
            {
                if (ModelState.IsValid && contexto.DetalleVentas.AsNoTracking().SingleOrDefault(z => z.Id == id) != null)
                {
                    //entidad.Id = id;
                    contexto.DetalleVentas.Update(entidad);
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
