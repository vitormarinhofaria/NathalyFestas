using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Models
{
    public class Pedido
    {
        public ObjectId Id { get; set; }
        public Endereco Endereco { get; set; }
        public bool Entregar { get; set; }
        public DateTime DataFesta { get; set; }
        public DateTime DataEntrega { get; set; }
        public DateTime DataRecolha { get; set; }
        public Cliente Cliente { get; set; }
        public List<Material> Materiais { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime LastModified { get; set; }

        public Pedido()
        {
            Endereco = new Endereco();
            Cliente = new Cliente();
            Materiais = new List<Material>();

            DataFesta = DateTime.UtcNow;
            DataEntrega = DateTime.UtcNow;
            DataRecolha = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
            LastModified = DateTime.UtcNow;
        }

        public decimal ValorTotal()
        {
            decimal total = 0;
            foreach (var mat in Materiais)
            {
                decimal soma = mat.Quantidade * mat.Preco;
                total += soma;
            }
            return total;
        }
    }
}
