
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AwesomeDevEvents.API.Entities;
using AwesomeDevEvents.API.Models;
using AwesomeDevEvents.API.Persistence;
using Microsoft.AspNetCore.Mvc;


namespace AwesomeDevEvents.API.Controllers
{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {

        private readonly DevEventsDbContext _context;
        private Guid id;

        public DevEventsController(DevEventsDbContext context)
        {
            _context = context;
        }

        // api/dev-events GET-1
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _context.DevEvents.Where(d =>!d.IsDeleted).ToList();
            return Ok(devEvents);
        }

        // api/dev-events/2722ba6-27d7-43c9-8ecc-3d9509e4656e(chave primaria/ pk) GET-2
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            return Ok(devEvent);
        }

        // api/dev-events/ POST-1
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _context.DevEvents.Add(devEvent);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);

        }

        //api/dev-events/2722ba6-27d7-43c9-8ecc-3d9509e4656e PUT
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, DevEvent input)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);


            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);
            _context.SaveChanges();


            return NoContent();
        }

        // api/dev-events/2722ba6-27d7-43c9-8ecc-3d9509e4656e DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Delete();
            _context.SaveChanges();


            return NoContent();
        }

        // api/dev-evens/2722ba6-27d7-43c9-8ecc-3d9509e4656e/speakers/POST-2
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, DevEventSpeaker speaker)
        {
            var devEvent = _context.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }
            devEvent.Speakers.Add(speaker);
            _context.SaveChanges();


            return NoContent();
        }
    }
}

