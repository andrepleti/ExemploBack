namespace API.Models
{
    public class Pessoa
    {
        public Pessoa()
        {
            Codigo = 0;
            Nome = string.Empty;
            Idade = 0;
        }

        public int Codigo { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
    }
}
