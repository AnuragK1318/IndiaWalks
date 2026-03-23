using Microsoft.AspNetCore.Identity;

namespace IndiaWalks.APi.Abstract
{
    public interface ITokenRepo
    {
        //it will give us a token which will have a return type as a string
        //Type of user is IdentityUser 
        //and it will also have List of roles
        string CreateJwtToken(IdentityUser user,List<string> roles);
    }
}
