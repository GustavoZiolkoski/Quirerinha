using Quirerinha.Models;
using System;
using System.Globalization;

namespace Quirerinha.Views
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            btnLogin.Clicked += BtnLogin_Clicked;
            PreencherCamposUsuario();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            PreencherCamposUsuario();
        }

        private async void PreencherCamposUsuario()
        {
            try
            {
                var usuario = await App.SQLiteDbUsuario.GetLastLoggedInUser();

                if (usuario != null)
                {
                    tbUsuario.Text = usuario.Nome;
                    tbRemuneracao.Text = usuario.Remuneracao.ToString();
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao carregar os dados do usu�rio: {ex.Message}", "OK");
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var confirmar = await DisplayAlert("Confirma��o", "Voc� � um novo usu�rio com uma nova remunera��o?", "Sim", "N�o");

                if (confirmar)
                {
                    App.SQLiteDbUsuario.DeleteAllUsers();
                }

                if (string.IsNullOrEmpty(tbUsuario.Text) || string.IsNullOrEmpty(tbRemuneracao.Text))
                {
                    await DisplayAlert("ERRO!", "Por favor, preencha todos os campos.", "OK");
                    return;
                }

                if (!double.TryParse(tbRemuneracao.Text, out double remuneracao))
                {
                    await DisplayAlert("ERRO!", "A remunera��o deve ser um n�mero v�lido.", "OK");
                    return;
                }

                var novoUsuario = new Usuarios
                {
                    Nome = tbUsuario.Text,
                    Remuneracao = remuneracao
                };

                await App.SQLiteDbUsuario.SaveItemAsync(novoUsuario);
                //await DisplayAlert("Sucesso!", "Usu�rio cadastrado com sucesso.", "OK");
                App.Current.MainPage = new AppShell();

                if (confirmar)
                {
                    tbUsuario.Text = "";
                    tbRemuneracao.Text = "";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar o usu�rio: {ex.Message}", "OK");
            }
        }

    }
}
