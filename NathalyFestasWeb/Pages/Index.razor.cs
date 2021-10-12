using Microsoft.AspNetCore.Components;
using NathalyFestasWeb.Models;
using NathalyFestasWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Pages
{
    partial class Index : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected PedidosRepository Repository { get; set; }
        private List<Pedido> Pedidos { get; set; }
        private List<Pedido> PedidosFiltered { get; set; }
        protected async override Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            Pedidos = await Repository.GetAll();
            PedidosFiltered = Pedidos;
        }

        private void NovoPedido()
        {
            Navigation.NavigateTo("pedido/create");
        }

        private void SearchChange(ChangeEventArgs args)
        {
            string search = ((string)args.Value).ToLower().Trim();
            PedidosFiltered = Pedidos.FindAll((p) =>
            {
                string nomeCliente = p.Cliente.Nome.Trim().ToLower();
                string endRua = p.Endereco.Rua.Trim().ToLower();
                string endBairro = p.Endereco.Bairro.Trim().ToLower();
                string dataEntrega = p.DataEntrega.ToShortDateString().ToLower().Trim();
                string dataRecolha = p.DataRecolha.ToShortDateString().ToLower().Trim();
                if (nomeCliente.Contains(search) ||
                    endRua.Contains(search) ||
                    endBairro.Contains(search) ||
                    dataEntrega.Contains(search) ||
                    dataRecolha.Contains(search))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }
    }
}
