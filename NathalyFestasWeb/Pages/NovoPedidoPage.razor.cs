using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using NathalyFestasWeb.Models;
using NathalyFestasWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NathalyFestasWeb.Pages
{
    partial class NovoPedidoPage : ComponentBase
    {
        [Inject]
        protected IJSRuntime Js { get; set; }
        [Inject]
        protected NavigationManager Navigation { get; set; }
        [Inject]
        protected PedidosRepository Pedidos { get; set; }
        private Pedido Pedido { get; set; }
        private Material CurrentInsertMat { get; set; }

        private bool HideDeleteConfirmation = true;
        private bool InsertPopUpHidden = true;
        private bool HideDeleteItem = true;

        [Parameter]
        public string IdPedido { get; set; }
        protected override async Task OnInitializedAsync()
        {
            if (IdPedido is not null)
            {
                var pedido = await Pedidos.GetByID(MongoDB.Bson.ObjectId.Parse(IdPedido));
                if (pedido is not null)
                {
                    Pedido = pedido;
                }
                else
                {
                    Pedido = new Pedido() { Id = MongoDB.Bson.ObjectId.GenerateNewId() };
                }
            }
            else
            {
                Pedido = new Pedido() { Id = MongoDB.Bson.ObjectId.GenerateNewId() };
            }
            await base.OnInitializedAsync();
        }
        protected async void SalvarPedido()
        {
            bool inserted = await Pedidos.Insert(Pedido);
            if (!inserted)
            {
                await Pedidos.Replace(Pedido);
            }
            Navigation.NavigateTo("/");
        }

        protected async void AddMaterial()
        {
            CurrentInsertMat = new Material() { Unidade = Unidade.Unit, Quantidade = 1 };
            InsertPopUpHidden = false;
            await Js.InvokeVoidAsync("blurBg");
            StateHasChanged();
        }

        private void ShowDeleteMaterial()
        {
            HideDeleteItem = false;
            StateHasChanged();
        }

        private async void CancelInsert()
        {
            InsertPopUpHidden = true;
            CurrentInsertMat = null;
            await Js.InvokeVoidAsync("removeBlurBg");
            StateHasChanged();
        }

        private async void SaveInsert()
        {
            InsertPopUpHidden = true;
            Pedido.Materiais.Add(CurrentInsertMat);
            CurrentInsertMat = null;
            await Js.InvokeVoidAsync("removeBlurBg");
            StateHasChanged();
        }

        private void CancelEditNew()
        {
            Navigation.NavigateTo("/");
        }

        private void AskDeletePedido()
        {
            HideDeleteConfirmation = false;
            StateHasChanged();
        }
        private async void ConfirmDeletion()
        {
            if (!Pedidos.Delete(Pedido))
            {
                await Js.InvokeVoidAsync("alert", "Erro ao deletar pedido.\n Tente novamente mais tarde.");
            }
            Navigation.NavigateTo("/");
        }
        private void CancelDeletion()
        {
            HideDeleteConfirmation = true;
            StateHasChanged();
        }
    }
}
