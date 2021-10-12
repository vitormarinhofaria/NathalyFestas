using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Models
{
    public class Endereco
    {
        public string Rua { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }

        public Endereco()
        {
            Rua = "";
            Bairro = "";
            CEP = "";
        }
        public Endereco(string rua, string bairro = "", string cep = "")
        {
            Rua = rua;
            Bairro = bairro;
            CEP = cep;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Bairro))
            {
                return Rua;
            }
            return $"{Rua}, {Bairro}";
        }
    }
}
