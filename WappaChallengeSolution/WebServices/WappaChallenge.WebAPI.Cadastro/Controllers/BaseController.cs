using Microsoft.AspNetCore.Mvc;
using System;

namespace WappaChallenge.WebAPI.Cadastro.Controllers
{
    public class BaseController : ControllerBase
    {
        protected new IActionResult ReponseException(Exception e)
        {
            return BadRequest(new
            {
                success = false,
                errors = e.Message
            });
        }

        protected new IActionResult ResponseSucess(object result = null)
        {
            return Ok(new
            {
                success = true,
                data = result
            });           
        }

        protected new IActionResult ResponseBadRequest(string message)
        {
            return BadRequest(new
            {
                success = false,
                errors = message
            });
        }
    }
}