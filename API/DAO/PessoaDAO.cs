using API.Models;
using System.Collections.Generic;
using System.Linq;

namespace API.DAO
{
    public static class PessoaDAO
    {
        public static List<Pessoa> Pessoas { get; set; } = new List<Pessoa>();

        public static List<Pessoa> RetornaLista(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return Pessoas.ToList();
            else
                return Pessoas.Where(x => x.Nome.ToUpper().Contains(nome.ToUpper())).ToList();
        }

        public static Pessoa RetornaObjeto(int codigo)
        {
            Pessoa objeto = Pessoas.Where(x => x.Codigo == codigo).FirstOrDefault();

            if (objeto == null)
                objeto = new Pessoa();

            return objeto;
        }

        public static bool Insere(Pessoa objeto)
        {
            int codigo = 0;

            if (Pessoas.Any())
                codigo = Pessoas.Max().Codigo + 1;
            else
                codigo = 1;

            objeto.Codigo = codigo;

            Pessoas.Add(objeto);

            return true;
        }

        public static bool Atualiza(Pessoa objeto)
        {
            Pessoa objetoBanco = Pessoas.Where(x => x.Codigo == objeto.Codigo).FirstOrDefault();

            if (objetoBanco != null)
            {
                objetoBanco.Nome = objeto.Nome;
                objetoBanco.Idade = objeto.Idade;
                objetoBanco.Login = objeto.Login;
                objetoBanco.Senha = objeto.Senha;

                return true;
            }

            return false;
        }

        public static bool Deleta(int codigo)
        {
            Pessoa objetoBanco = Pessoas.Where(x => x.Codigo == codigo).FirstOrDefault();

            if (objetoBanco != null)
            {
                Pessoas.Remove(objetoBanco);

                return true;
            }

            return false;
        }

        public static Pessoa Autentica(Pessoa objeto)
        {
            Pessoa objetoBanco = Pessoas.Where(x => x.Login == objeto.Login && x.Senha == objeto.Senha).FirstOrDefault();

            if (objetoBanco == null)
                objetoBanco = new Pessoa();

            return objetoBanco;
        }
    }
}
