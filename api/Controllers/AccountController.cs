using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route ("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager,ITokenService tokenService,SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService; 
            _signInManager = signInManager;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if(user == null)return Unauthorized("Invalid UserName");

            var result = await _signInManager.CheckPasswordSignInAsync(user,loginDto.Password,false);

            if(!result.Succeeded) return Unauthorized("UserName or Password is incorrect");
            
            return Ok(new NewUserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            });

        }




        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody]RegisterDto registerDto)
        {
            try{

                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var AppUser = new AppUser{
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var CreatedUser = await _userManager.CreateAsync(AppUser, registerDto.Password);
                if (CreatedUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(AppUser, "User");
                    if(roleResult.Succeeded)   {


                        return Ok(
                            new NewUserDto{
                                UserName = AppUser.UserName,
                                Email = AppUser.Email,
                                Token = _tokenService.CreateToken(AppUser)
                            }
                        );



                    }else{
                        return StatusCode(500,roleResult.Errors);
                    }
                }
                else{
                    return StatusCode(500,CreatedUser.Errors);
                }

            }catch(Exception ex){
                return StatusCode(500,ex);
            }
        }
    }
}