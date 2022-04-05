using Entities.Models;
using Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using WebApi.Services;

[Authorize]
[ApiController]
public class AccountController : Controller
{
    private IRepositoryWrapper _repository;
    private readonly IEmailService _emailService;

    public AccountController(IRepositoryWrapper repository, IEmailService emailService)
    {
        _repository = repository;
        _emailService = emailService;
    }

    [Route("api/auth/login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var userExist = await _repository.User.GetValidUserAsync(user);

            if (userExist != null)
            {
                return BadRequest("Email or password is incorrect");
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message + " " + ex.InnerException);
        }
    }
    [Route("api/auth/register")]
    [HttpPost]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        try
        {
            if (user == null)
            {
                return BadRequest("User object is null");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid model object");
            }

            var userNew = await _repository.User.GetValidUserAsync(user);

            if (userNew == null)
            {
                _repository.User.CreateUser(user);
                await _repository.SaveAsync();
                var userCreated = await _repository.User.GetValidUserAsync(user);
                sendVerificationEmail(userCreated);
            }
            else
            {
                return BadRequest("Email alredy exists");
            }

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error" + ex.Message + " " + ex.InnerException);
        }
    }

    private void sendVerificationEmail(User user)
    {
        string message;
        var verifyUrl = $"api/auth/verify/token={user.Id}";
        message = $@"<p>Haga clic en el siguiente enlace para verificar su dirección de correo electrónico:</p>
                         <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
        _emailService.Send(
            to: user.Email,
            subject: " Verificar correo electrónico",
            html: $@"<h4>Verificar correo electrónico</h4>
                        {message}"
        );
    }
}