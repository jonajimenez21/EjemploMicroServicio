using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using testapi3.Context;
using testapi3.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace testapi3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly AppDBContext context;
        public PersonasController(AppDBContext context) {
            this.context = context;
        }
        // GET: api/<PersonasController>
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(context.persona.ToList());
            }
            catch(Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // GET api/<PersonasController>/5
        [HttpGet("{id}", Name="getpersona")]
        public ActionResult Get(int id)
        {
            try
            {
                var persona = context.persona.FirstOrDefault(p=>p.id==id);
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PersonasController>
        [HttpPost]
        public ActionResult Post([FromBody] Personas persona)
        {
            try
            {
                context.Add(persona);
                context.SaveChanges();
                return CreatedAtRoute("getpersona", new {id=persona.id }, persona);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<PersonasController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Personas persona)
        {
            try {
                if (persona.id == id)
                {
                    context.Entry(persona).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("getpersona", new { id = persona.id }, persona);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }

        }

        // DELETE api/<PersonasController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var persona = context.persona.FirstOrDefault(p => p.id == id);
                if (persona != null)
                {
                    context.persona.Remove(persona);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
