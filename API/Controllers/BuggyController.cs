using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
public class BuggyController : BaseApiController
{
    [HttpGet("Unauthorized")]
    public IActionResult GetUnauthorized()
    {
        return Unauthorized();
    }
    [HttpGet("BadRequest")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("Not a good request");
    }
    [HttpGet("NotFound")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }
    [HttpGet("InternalError")]
    public IActionResult GetInternalError()
    {
        throw new Exception("This is a test exception");
    }
    [HttpPost("ValidationError")]
    public IActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }
}
