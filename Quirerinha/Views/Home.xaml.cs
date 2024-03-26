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
        }

        private async void LoadUserData()
        {
            try
            {
                // Busca o usuário mais recentemente cadastrado
                var usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();

                if (usuario != null)
                {
                    // Define os valores dos campos na página
                    lblUsuario.Text = usuario.Nome;

                    // Formata a remuneração usando uma cultura específica para exibir a vírgula como separador decimal
                    lblRemuneracao.Text = usuario.Remuneracao.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")) + " Reais";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao carregar os dados do usuário: {ex.Message}", "OK");
            }
        }
    }
}
