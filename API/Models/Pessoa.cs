namespace API.Models
{
    public class Pessoa
    {
        public Pessoa()
        {
            Codigo = 0;
            Nome = string.Empty;
            Login = string.Empty;
            Senha = string.Empty;
            Idade = 0;
            Token = string.Empty;
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public int Idade { get; set; }
        public string Token { get; set; }
    }
}
