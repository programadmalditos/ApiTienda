using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using ApiTienda.Model;
using ApiTienda.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ApiTienda.Controllers{
  [Route("api/[controller]")]
    public class JwtController:Controller{
    private readonly JwtIssuerOptions _jwtOptions;
    private readonly JsonSerializerSettings _serializerSettings;
    public ListaCompraConext Context { get; set; }

      public JwtController(IOptions<JwtIssuerOptions> jwtOptions,ListaCompraConext _context)
    {
      _jwtOptions = jwtOptions.Value;
      ThrowIfInvalidOptions(_jwtOptions);
   

      _serializerSettings = new JsonSerializerSettings
      {
        Formatting = Formatting.Indented
      };
      Context=_context;
    }
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Get([FromForm] Usuario usuario)
    {
      var identity = await GetClaimsIdentity(usuario);
      if (identity == null)
      {
        return BadRequest("Invalid credentials");
      }

      var claims = new List<Claim>()
      {
        new Claim(JwtRegisteredClaimNames.Sub, usuario.Login),
        new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
        new Claim(JwtRegisteredClaimNames.Iat, 
                  ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), 
                  ClaimValueTypes.Integer64),
        
      };
      _jwtOptions.UpdateToken();
      // Create the JWT security token and encode it.
      var jwt = new JwtSecurityToken(
          issuer: _jwtOptions.Issuer,
          audience: _jwtOptions.Audience,
          claims: claims,
          notBefore: DateTime.UtcNow,
          expires: _jwtOptions.Expiration,
          signingCredentials: _jwtOptions.SigningCredentials);

      var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

      // Serialize and return the response
      var response = new
      {
        access_token = encodedJwt,
        expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
      };

      var json = JsonConvert.SerializeObject(response, _serializerSettings);
      return new OkObjectResult(json);
    }  

        private Task<ClaimsIdentity> GetClaimsIdentity(Usuario user)
    {

      var us=Context.Usuario.FirstOrDefault(o=>o.Login==user.Login && o.Password==user.Password);

      if (us!=null)
      {
        return Task.FromResult(new ClaimsIdentity(
          new GenericIdentity(user.Login, "Token"),
          new Claim[] { }));
      }

      // Credentials are invalid, or account doesn't exist
      return Task.FromResult<ClaimsIdentity>(null);
    }
private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
    {
      if (options == null) throw new ArgumentNullException(nameof(options));

      if (options.ValidFor <= TimeSpan.Zero)
      {
        throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
      }

      if (options.SigningCredentials == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
      }

      if (options.JtiGenerator == null)
      {
        throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
      }
    }

 private static long ToUnixEpochDate(DateTime date)
      => (long)Math.Round((date.ToUniversalTime() - 
                           new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                          .TotalSeconds);
    }
    

}