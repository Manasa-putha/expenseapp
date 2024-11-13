﻿using ExpenseApp.Data;
using ExpenseApp.Models;
using ExpenseApp.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.EntityFrameworkCore;


namespace ExpenseApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Authenticate([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            //var user = await _context.Users
            //    .FirstOrDefaultAsync(x => x.Username == userObj.Username);
            var UserAvailable = _context.Users.Where(u => u.Email == userObj.Email || u.Password == userObj.Password).FirstOrDefault();
            if (UserAvailable == null)
                return NotFound(new { Message = "User not found!" });

            if (!(userObj.Password == UserAvailable.Password))
            {
                return BadRequest(new { Message = "Password is Incorrect" });
            }

            UserAvailable.Token = CreateJwt((UserAvailable));
            var newAccessToken = UserAvailable.Token;
            var newRefreshToken = CreateRefreshToken();
            UserAvailable.RefreshToken = newRefreshToken;
            UserAvailable.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
            await _context.SaveChangesAsync();

            if (UserAvailable != null)
                await _context.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser([FromBody] User userObj)
        {
            if (userObj == null)
                return BadRequest();

            // check email
            if (await CheckEmailExistAsync(userObj.Email))
                return BadRequest(new { Message = "Email Already Exist" });

            //check username
            if (await CheckUsernameExistAsync(userObj.Name))
                return BadRequest(new { Message = "Username Already Exist" });

            var passMessage = CheckPasswordStrength(userObj.Password);
            if (!string.IsNullOrEmpty(passMessage))
                return BadRequest(new { Message = passMessage.ToString() });

            //userObj.Password = PasswordHasher.HashPassword(userObj.Password);
            userObj.CreatedOn = DateTime.UtcNow; // or DateTime.Now if using local time
            userObj.UserType = UserType.USER;
            //userObj.Token = "";
            await _context.AddAsync(userObj);
            await _context.SaveChangesAsync();
            return Ok(new
            {
                Status = 200,
                Message = "User Added!"
            });
        }

        private Task<bool> CheckEmailExistAsync(string? email)
            => _context.Users.AnyAsync(x => x.Email == email);

        private Task<bool> CheckUsernameExistAsync(string? username)
            => _context.Users.AnyAsync(x => x.Email == username);

        private static string CheckPasswordStrength(string pass)
        {
            StringBuilder sb = new StringBuilder();
            if (pass.Length < 9)
                sb.Append("Minimum password length should be 8" + Environment.NewLine);
            if (!(Regex.IsMatch(pass, "[a-z]") && Regex.IsMatch(pass, "[A-Z]") && Regex.IsMatch(pass, "[0-9]")))
                sb.Append("Password should be AlphaNumeric" + Environment.NewLine);
            if (!Regex.IsMatch(pass, "[<,>,@,!,#,$,%,^,&,*,(,),_,+,\\[,\\],{,},?,:,;,|,',\\,.,/,~,`,-,=]"))
                sb.Append("Password should contain special charcter" + Environment.NewLine);
            return sb.ToString();
        }
        private string CreateJwt(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
                new Claim(ClaimTypes.Name,$"{user.Name}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.Now.AddHours(1),
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _context.Users
                .Any(a => a.RefreshToken == refreshToken);
            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }


        private ClaimsPrincipal GetPrincipleFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysceret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("This is Invalid Token");
            return principal;
        }

        //[Authorize]
        //[HttpGet]
        //public async Task<ActionResult<User>> GetAllUsers()
        //{
        //    return Ok(await _context.Users.ToListAsync());
        //}



        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh([FromBody] TokenApiDto tokenApiDto)
        {
            if (tokenApiDto is null)
                return BadRequest("Invalid Client Request");
            string accessToken = tokenApiDto.AccessToken;
            string refreshToken = tokenApiDto.RefreshToken;
            var principal = GetPrincipleFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                return BadRequest("Invalid Request");
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
            return Ok(new TokenApiDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }
        [HttpGet("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _context.Users.ToListAsync();  
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching users." });
            }
        }
        [HttpPut("users/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UserDto updateUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var user = await _context.Users.FindAsync(userId);
                if (user == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                
                user.Name = updateUserDto.Name;
                user.Email = updateUserDto.Email;
           

                await _context.SaveChangesAsync();
                return Ok(new { message = "User updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user." });
            }
        }
        //[HttpDelete("users/{userId}")]
        //public async Task<IActionResult> DeleteUser(int userId)
        //{
        //    try
        //    {
        //        var user = await _context.Users
        //                                 .Include(u => u.GroupUsers) // Include related entities
        //                                 .FirstOrDefaultAsync(u => u.Id == userId);

        //        if (user == null)
        //        {
        //            return NotFound(new { message = "User not found." });
        //        }

        //        // If the user has related entities (e.g., Orders), delete them first
        //        if (user.Orders.Any())
        //        {
        //            _context.Orders.RemoveRange(user.Orders);
        //        }

        //        _context.Users.Remove(user);
        //        await _context.SaveChangesAsync();

        //        return Ok(new { message = "User deleted successfully." });
        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        return StatusCode(500, new { message = "Database update failed, foreign key constraint issue." });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "An error occurred while deleting the user." });
        //    }
        //}



    }
}
