using API.DAO;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [AllowAnonymous]
    public class PessoaController : Controller
    {
        [HttpGet("lista/{nome?}")]
        public ActionResult<List<Pessoa>> RetornaLista(string nome)
        {
            return Ok(PessoaDAO.RetornaLista(nome));
        }

        [HttpGet("{codigo}")]
        public ActionResult<Pessoa> RetornaObjeto(int codigo)
        {
            return Ok(PessoaDAO.RetornaObjeto(codigo));
        }

        [HttpPost]
        public ActionResult Insere([FromBody] Pessoa objeto)
        {
            try
            {
                if (PessoaDAO.Insere(objeto))
                    return Ok(new Retorno { Mensagem = "Cadastrado com sucesso" });

                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
            catch
            {
                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
        }

        [HttpPut]
        public ActionResult Atualiza([FromBody] Pessoa objeto)
        {
            try
            {
    
                if (PessoaDAO.Atualiza(objeto))
                    return Ok(new Retorno { Mensagem = "Atualizado com sucesso" });

                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
            catch
            {
                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
        }

        [HttpDelete("{codigo}")]
        public ActionResult Deleta(int codigo)
        {
            try
            {

                if (PessoaDAO.Deleta(codigo))
                    return Ok(new Retorno { Mensagem = "Deletado com sucesso" });

                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
            catch
            {
                return BadRequest(new Retorno { Mensagem = "Não foi possível realizar esta operação, tente novamente mais tarde" });
            }
        }
    }
}
