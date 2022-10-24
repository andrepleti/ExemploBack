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
        PessoaDAO dao = new();


        [HttpGet("lista/{nome?}")]
        public ActionResult<List<Pessoa>> RetornaLista(string nome)
        {
            return Ok(dao.RetornaLista(nome));
        }

        [HttpGet("{codigo}")]
        public ActionResult<Pessoa> RetornaObjeto(int codigo)
        {
            return Ok(dao.RetornaObjeto(codigo));
        }

        
    }
}
