using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.DAO
{
    public class PessoaDAO
    {
        private List<Pessoa> Pessoas()
        {
            List<Pessoa> lista = new();
            lista.Add(new Pessoa { Codigo = 1, Nome = "André Luis Pleti", Idade = 32 });
            lista.Add(new Pessoa { Codigo = 2, Nome = "José Reynaldo", Idade = 24 });
            lista.Add(new Pessoa { Codigo = 3, Nome = "Junior Martins", Idade = 9999 });
            lista.Add(new Pessoa { Codigo = 4, Nome = "Edgard", Idade = 56 });
            lista.Add(new Pessoa { Codigo = 5, Nome = "Felipe", Idade = 30 });
            lista.Add(new Pessoa { Codigo = 6, Nome = "Luiz Gustavo", Idade = 30 });
            lista.Add(new Pessoa { Codigo = 7, Nome = "Luiz Octávio", Idade = 48 });
            lista.Add(new Pessoa { Codigo = 8, Nome = "Fabio Henrique", Idade = 13 });

            return lista;
        }

        public List<Pessoa> RetornaLista(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Pessoas().ToList();
            else
                return Pessoas().Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
        }

        public Pessoa RetornaObjeto(int codigo)
        {
            return Pessoas().Where(x => x.Codigo == codigo).FirstOrDefault();
        }
    }
}
