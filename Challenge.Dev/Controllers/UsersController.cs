using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Challenge.Dev.Context;
using Challenge.Dev.Models;
using Challenge.Dev.Helpers;
using Microsoft.EntityFrameworkCore;
using Challenge.Dev.Repositories;
using System.Collections.Generic;


namespace Challenge.Dev.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            if (_userRepository.GetAll().Count() == 0)
            {
                _userRepository.Add(new User { FirstName = "Adilson", LastName = "Feitoza" });
            }
        }

        /// <summary>
        /// retorna todos os usuários ordernados
        /// </summary>
        /// <param name="sortingParams">Parâmetros de ordernação</param>
        /// <returns></returns>
        [HttpGet(Name = "GetUsers")]
        public IEnumerable<User> GetAll(SortingParams sortingParams)
        {
            var users = _userRepository.GetUsers(sortingParams);
            return users;
        }

        [HttpGet("{id}", Name = "GetUser")]
        public IActionResult GetById(long id)
        {
            var item = _userRepository.Find(id);
            if (item == null)
                return BadRequest();

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody]User user)
        {
            try
            {
                _userRepository.Add(user);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return CreatedAtRoute("GetUser", new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody]User user)
        {
            var res = _userRepository.Update(id , user);
            if (!res)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var user = _userRepository.Find(id);
            if (user == null)
                return NotFound();

            _userRepository.Delete(user);

            return NoContent();
        }
    }
}
