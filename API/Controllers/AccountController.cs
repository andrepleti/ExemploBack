using API.DAO;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<ActionResult<Pessoa>> Autenticar([FromBody] Pessoa objeto)
        {
            try
            {
                Pessoa usuarioLogado = PessoaDAO.Autentica(objeto);

                if (usuarioLogado.Codigo == 0)
                    return NotFound(new Retorno { Mensagem = "Usuário ou senha inválidos" });

                usuarioLogado.Token = await JWTService.GerarToken(usuarioLogado, false);

                return Ok(usuarioLogado);
            }
            catch
            {
                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
        }
    }
}
