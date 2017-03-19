using System;
using System.Collections.Generic;
using System.Linq;
using ApiTienda.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTienda.Controllers{

     [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
      [Authorize()]
    public class CompraController : Controller
    {

        private ListaCompraConext context;

        public CompraController(ListaCompraConext _context)
        {
            context = _context;
        }

        public IEnumerable<Producto> Get()
        {
           return context.Producto.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = context.Producto.Find(id);

            if (data != null)
                return Ok(data);

            return NotFound();


        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Producto value)
        {

            if(value.Tienda!=null){
                var tienda=context.Tienda.Find(value.Tienda.Id);
                if(tienda!=null)
                    value.Tienda=tienda;
                    else{
                        value.Tienda=new Tienda(){
                            Nombre=value.Tienda.Nombre,
                            Direccion=value.Tienda.Direccion
                        };
                    }
            }
            context.Add(value);
            try
            {
                context.SaveChanges();
                value.Tienda=null;
                return Created("", value);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody]Producto value)
        {
            if (!context.Producto.Any(o => o.Id == value.Id))
                return NotFound();

            context.Entry(value).State = EntityState.Modified;
            try
            {
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var data = context.Producto.Find(id);
            if (data == null)
                return NotFound();
            context.Producto.Remove(data);


            try
            {
                context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }
    }
}