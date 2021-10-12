using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public Endereco Endereco { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; set; }

        public Cliente()
        {
            Nome = "";
            Endereco = new Endereco();
            Email = "";
            Telefone = "";
            CPF = "";
        }
        public Cliente(string nome, string email = "", string telefone = "", string cpf = "")
        {
            Nome = nome; Telefone = telefone; Email = email; CPF = cpf;
        }
    }
}
