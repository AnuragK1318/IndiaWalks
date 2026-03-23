using IndiaWalks.APi.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IndiaWalks.APi.Concrete
{
    public class TokenRepo : ITokenRepo
    {
        private readonly IConfiguration _configuration;
        public TokenRepo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This is the "ID Card Generator" function. 
        // It takes a 'User' and a 'List of Roles' (like Admin or Member)
        // to create a security token
        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // 1. Prepare a blank list of "Claims" (Facts about the person).
            // Think of this as filling out the blanks on a driver's license application.
            var claims = new List<Claim>();
            // 2. Write down the user's Email on the application
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            // 3. For every role the person has (like "Manager" and "Editor"), 
            // we add each one to the application list so the system knows their permissions.
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            // 4. Create a "Secret Stamp" (Security Key). 
            // This is like a special wax seal or a watermark that only our company knows.
            // It prevents people from forging their own fake ID cards.
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            // 5. Create the "Ink" and "Signature Tool" using our secret key and a secure math formula (HmacSha256)
            var credentials =new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            // 6. Assemble the actual ID Card (The Token)
            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],   // Who issued this card? (Your Website)
                _configuration["Jwt:Audience"], // Who is allowed to use it? (Your Website's users)
                claims,                         // The list of facts we wrote down earlier
                expires:DateTime.Now.AddMinutes(15), // How long until the card expires? (15 Minutes)
                signingCredentials:credentials);    // Apply the "Secret Stamp" to make it official

            // 7. Finally, turn that digital object into a long string of random-looking text 
            // so it can be sent over the internet.
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
