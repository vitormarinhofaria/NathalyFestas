using Microsoft.AspNetCore.Components;
using NathalyFestasWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorDownloadFile;
using NathalyFestasWeb.Services;
using System.Threading;

namespace NathalyFestasWeb.Shared
{
    partial class PedidoListItem : ComponentBase
    {
        [Parameter]
        public Pedido Pedido { get; set; }
        [Inject]
        public NavigationManager Navigation { get; set; }
        [Inject]
        public IBlazorDownloadFileService DownloaderService { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
        private void EditPedido()
        {
            Navigation.NavigateTo("pedido/edit/" + Pedido.Id);
        }
        private async void DownloadAsPdf()
        {
            byte[] fileBytes = PdfManager.GetPedidoPdf(Pedido);
            await DownloaderService.DownloadFile($"{Pedido.Cliente.Nome} - {Pedido.DataEntrega.ToShortDateString()}.pdf", fileBytes, CancellationToken.None, "application/octet-stream");
        }
    }
}
