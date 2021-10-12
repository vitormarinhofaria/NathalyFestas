using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Models
{
    public class Material
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public Unidade Unidade { get; set; }
        public decimal Quantidade { get; set; }
        public Material()
        {
            Nome = "";
            Preco = 0;
            Unidade = Unidade.Unit;
            Quantidade = 0;
        }
        public Material(string nome, decimal preco = 0, decimal quantidade = 0, Unidade unidade = Unidade.Unit)
        {
            Nome = nome; Preco = preco; Quantidade = quantidade; Unidade = unidade;
        }
        public string UnidadeString()
        {
            return this.Unidade switch
            {
                Unidade.Duz => "Duz.",
                Unidade.Unit => "Unit.",
                Unidade.Dez => "Dez.",
                Unidade.Jogo => "Jogo",
                _ => "N/A",
            };
        }
    }

    public enum Unidade
    {
        Duz,
        Unit,
        Dez,
        Jogo,
    }
}
