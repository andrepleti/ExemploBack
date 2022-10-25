using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using API.Models;

namespace API.Services
{
    public class JWTService
    {
        public static string AuthenticationScheme
        {
            get
            {
                return JwtBearerDefaults.AuthenticationScheme;
            }
        }

        public static bool ValorTrue
        {
            get
            {
                return true;
            }
        }

        public static bool ValorFalso
        {
            get
            {
                return false;
            }
        }

        public static byte[] IssuerSigningKey
        {
            get
            {
                return Encoding.ASCII.GetBytes(ApiKey);
            }
        }

        private static readonly string ApiKey = "47cd9810a3f21da6efe96cf548ca6fe7e3452f4f9dbfd11b14b806d70dac0611";

        public static async Task<string> GerarToken(Pessoa usuario, bool recuperacaoSenha)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenDescriptor = await RetornarToken(usuario, recuperacaoSenha);

            var token = await Task.FromResult(tokenHandler.CreateToken(tokenDescriptor));
            return await Task.FromResult(tokenHandler.WriteToken(token));
        }

        private static async Task<SecurityTokenDescriptor> RetornarToken(Pessoa usuario, bool recuperacaoSenha)
        {
            var key = await Task.FromResult(Encoding.ASCII.GetBytes(ApiKey));

            if (recuperacaoSenha)
            {
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Login)
                }),
                    Expires = DateTime.Now.AddHours(4),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenDescriptor;
            }
            else
            {
                List<Claim> claims = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuario.Login)
                };

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddHours(12),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                return tokenDescriptor;
            }
        }

        public static async Task<Pessoa> DecodificarToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var objeto = await Task.FromResult(handler.ReadJwtToken(token));

            Pessoa usuario = new();

            List<Claim> lista = new();

            if (objeto != null)
            {
                DateTime validade = objeto.ValidTo;

                if (validade > DateTime.Now)
                {
                    lista = (List<Claim>)objeto.Claims;

                    usuario.Codigo = string.IsNullOrWhiteSpace(lista.FirstOrDefault(a => a.Type == "nameid")?.Value) ? 0 : int.Parse(lista.FirstOrDefault(a => a.Type == "nameid")?.Value);
                    usuario.Login = lista.FirstOrDefault(a => a.Type == "unique_name")?.Value.ToString();
                }
            }

            return usuario;
        }

        public static string RetornaChave()
        {
            return ApiKey;
        }
    }
}
