using IndiaWalks.APi.Abstract;
using IndiaWalks.APi.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IndiaWalks.APi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //When a user register he will enter username and 
        //password.The below controller is for
        //Registering the User

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepo _tokenRepo;
        public AuthController(UserManager<IdentityUser> userManager,ITokenRepo tokenRepo)
        {
            _userManager = userManager;
            _tokenRepo = tokenRepo;
        }

        //Https/api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterReqDto registerReqDto)
        {
            // 1.Create a new User object using the Identity model
            var identityUser = new IdentityUser
            {
                UserName= registerReqDto.Username,
                Email = registerReqDto.Username
            };

            // 2. Use the 'UserManager' to actually save the user to the database.
            // It also handles hashing the password (scrambling it for safety)
            var identityResult = await _userManager.CreateAsync(identityUser, registerReqDto.Password);
            
            //Check if user creation was successful
            if(identityResult.Succeeded)
            {
                //4. Check if any roles like (Reader/Writer/Admin) was provided in request
                if (registerReqDto.Roles != null && registerReqDto.Roles.Any())
                {
                    // 5. Link the newly created user to specific roles in the database
                    identityResult = await _userManager.AddToRolesAsync(identityUser, registerReqDto.Roles);
                    // 6. If adding roles worked, return a success message
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login");
                    }
                }
            }
            // 7. If anything failed (User already exists, password too weak, etc.), return an error
            return BadRequest("Something went wrong");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginReqDto loginReqDto)
        {
            var user = await _userManager.FindByEmailAsync(loginReqDto.UserName);
            if (user != null)
            {
                bool checkPassword = await _userManager.CheckPasswordAsync(user,loginReqDto.Password);
                if (checkPassword) 
                {
                    var roles= await _userManager.GetRolesAsync(user);
                    if(roles!=null)
                    {
                     //Create a token for th user along with roles it has
                        var jwtToken = _tokenRepo.CreateJwtToken(user,roles.ToList());
                        var response = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(response);
                    }
                    return Ok("Login successfull");
                }
                
            }
            return BadRequest("Invalid Username or Password");
        }
    }
}
