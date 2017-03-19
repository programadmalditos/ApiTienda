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
 public class TiendaController : Controller
    {

        private ListaCompraConext context;

        public TiendaController(ListaCompraConext _context)
        {
            context = _context;
        }

        public IEnumerable<Tienda> Get()
        {
            return context.Tienda.ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var data = context.Tienda.Find(id);

            if (data != null)
                return Ok(data);

            return NotFound();


        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody]Tienda value)
        {
            context.Add(value);
            try
            {
                context.SaveChanges();
                return Created("", value);
            }
            catch (Exception e)
            {
                return BadRequest();
            }

        }

        [HttpPut]
        public IActionResult Put([FromBody]Tienda value)
        {
            if (!context.Tienda.Any(o => o.Id == value.Id))
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
            var data = context.Tienda.Find(id);
            if (data == null)
                return NotFound();
            context.Tienda.Remove(data);


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