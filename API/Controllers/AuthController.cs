using API.Models.User;
using Core.Services;
using Core.Validation;
using Domain.Entities;
using Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    private readonly UserValidator _userValidator;

    public AuthController(
        ILogger<AuthController> logger, 
        IUserService userService,
        ITokenService tokenService,
        UserValidator userValidator)
    {
        _logger = logger;
        _userService = userService;
        _tokenService = tokenService;
        _userValidator = userValidator;
    }


    [HttpPost("Register")]
    public IActionResult Register([FromBody] RegisterUser userModel)
    {
        if (userModel.Password != userModel.ConfirmPassword)
            return BadRequest("Passwords doesn't match");

        var user = new User
        {
            Name = userModel.Name,
            Email = userModel.Email,
            Password = CryptoService.Encryption(userModel.Password)
        };

        var resultValidation = _userValidator.Validate(user);
        if (!resultValidation.IsValid)
            return BadRequest(resultValidation.Errors);

        if (_userService.GetByEmail(user.Email) != null)
            return BadRequest("User already exists");

        if (_userService.Add(user))
            return Ok();
        else
            return BadRequest();
    }

    [HttpPost("Login")]
    public IActionResult Login([FromBody] LoginUser login)
    {
        var user = _userService.GetByEmail(login.Email);
        if (user == null)
            return BadRequest();

        if(!CryptoService.CheckPassword(login.Password, user.Password))
            return BadRequest();

        return Ok(_tokenService.CreateToken(user));
    }

}
