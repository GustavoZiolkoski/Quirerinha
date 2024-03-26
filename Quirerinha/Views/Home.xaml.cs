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
                // Busca o usu�rio mais recentemente cadastrado
                var usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();

                if (usuario != null)
                {
                    // Define os valores dos campos na p�gina
                    lblUsuario.Text = usuario.Nome;

                    // Formata a remunera��o usando uma cultura espec�fica para exibir a v�rgula como separador decimal
                    lblRemuneracao.Text = usuario.Remuneracao.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")) + " Reais";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao carregar os dados do usu�rio: {ex.Message}", "OK");
            }
        }
    }
}
