using Quirerinha.Models;
using System;
using System.Globalization;

namespace Quirerinha.Views
{
    public partial class Home : ContentPage
    {
        public Home()
        {
            InitializeComponent();
            LoadUserData();
            btnCadastrar.Clicked += BtnCadastrar_Clicked;
            btnGestao.Clicked += BtnGestao_Clicked;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            LoadUserData();
        }

        private async void LoadUserData()
        {
            try
            {
                var usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();

                if (usuario != null)
                {
                    lblUsuario.Text = usuario.Nome;

                    lblRemuneracao.Text = usuario.Remuneracao.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")) + " reais";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao carregar os dados do usuário: {ex.Message}", "OK");
            }
        }
        private void BtnGestao_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Gestao());
        }

        private void BtnCadastrar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Cadastro());
        }
    }
}
