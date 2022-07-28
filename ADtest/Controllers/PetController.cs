using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADtest.Data;
using ADtest.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ADtest.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly ApiContext _context;

        public PetController(ApiContext apiContext)
        {
            _context = apiContext;
        }
        [Authorize(Roles = "PManager")]
        [HttpPost]
        public JsonResult CreateEdit(Pet pet)
        {
            if (pet.id == 0)
            {
                _context.Pets.Add(pet);
            }
            else
            {
                var petInDb = _context.Pets.Find(pet.id);
                if (petInDb == null)
                {
                    return new JsonResult(NotFound());
                }

                petInDb = pet;
                
            }
            _context.SaveChanges();
            return new JsonResult(Ok(pet));
        }
        [Authorize(Roles = "PManager,HR")]
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Pets.Find(id);
            if (result == null)
                return new JsonResult(NotFound());
            return new JsonResult(Ok(result));
        }
        [Authorize(Roles = "PManager")]
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Pets.Find(id);
            if (result == null)
            {
                return new JsonResult(NotFound());
            }

            _context.Pets.Remove(result);
            _context.SaveChanges();
            return new JsonResult(NoContent());
        }
        [Authorize(Roles = "PManager,HR")]
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Pets.ToList();
            return new JsonResult(Ok(result));
        }
    }
}
