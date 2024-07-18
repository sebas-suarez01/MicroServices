using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthenticationManager.Models;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace JwtAuthenticationManager;

public class JwtTokenHandler
{
    public const string JWT_SECURITY_KEY = "dcslvlibqwd3qwdac31fdc4wq2sfvf457rgbdwcvthb";
    public const int JWT_TOKEN_VALIDITY_MIN = 10;
    private readonly List<UserAccount> _userAccounts;

    public JwtTokenHandler()
    {
        _userAccounts = new List<UserAccount>()
        {
            new UserAccount(){Username = "admin", Password = "admin123", Role = "Administrator"},
            new UserAccount(){Username = "user01", Password = "user123", Role = "User"}
        };
    }

    public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            return null;

        var userAccount = _userAccounts
            .FirstOrDefault(u => u.Username == request.Username && u.Password == request.Password);
        
        if (userAccount is null)
            return null;

        var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MIN);
        var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
        var claimsIdentity = new ClaimsIdentity(new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Name, userAccount.Username),
            new Claim("Role", userAccount.Role)
        });

        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature);

        var securityTokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = claimsIdentity,
            Expires = tokenExpiryTimeStamp,
            SigningCredentials = signingCredentials
        };

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

        var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
        var token = jwtSecurityTokenHandler.WriteToken(securityToken);

        return new AuthenticationResponse
        {
            Username = userAccount.Username,
            ExpiresIn = JWT_TOKEN_VALIDITY_MIN,
            JwtToken = token
        };
    }
}