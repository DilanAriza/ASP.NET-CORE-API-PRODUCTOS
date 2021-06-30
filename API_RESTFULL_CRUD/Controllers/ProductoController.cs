using API_RESTFULL_CRUD.Context;
using API_RESTFULL_CRUD.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_RESTFULL_CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        // Propiedad del contexto que le pasamaos al injectar el AppDbContext
        private readonly AppDbContext context;

        //Constructor del controller, aquí le injectamos el contexto de la app
        public ProductoController(AppDbContext context)
        {
            //  Asignando la propiedad y pasandola al constructor
            this.context = context;
        }

        // GET: api/<ProductoController>
        [HttpGet]
        public IEnumerable<Producto> Get()
        {
            return context.Producto.ToList();
        }

        // GET api/<ProductoController>/5
        [HttpGet("{id}")]
        public Producto Get(string id)
        {
            var producto = context.Producto.FirstOrDefault(p => p.pro_codigo == id);
            return producto;
        }

        // POST api/<ProductoController>
        [HttpPost]
        public ActionResult Post([FromBody] Producto producto)
        {
            try
            {
                context.Producto.Add(producto);
                context.SaveChanges();
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT api/<ProductoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Producto producto)
        {
            try
            {
                if(producto.pro_codigo == id)
                {
                    context.Entry(producto).State = EntityState.Modified;
                    context.SaveChanges();
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        // DELETE api/<ProductoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var producto = context.Producto.FirstOrDefault(p => p.pro_codigo == id);
            if(producto != null)
            {
                context.Producto.Remove(producto);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
