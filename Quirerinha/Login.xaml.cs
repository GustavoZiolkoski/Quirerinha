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
                await DisplayAlert("Erro", $"Ocorreu um erro ao carregar os dados do usuário: {ex.Message}", "OK");
            }
        }

        private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            try
            {
                var confirmar = await DisplayAlert("Confirmação", "Você é um novo usuário com uma nova remuneração?", "Sim", "Não");

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
                    await DisplayAlert("ERRO!", "A remuneração deve ser um número válido.", "OK");
                    return;
                }

                var novoUsuario = new Usuarios
                {
                    Nome = tbUsuario.Text,
                    Remuneracao = remuneracao
                };

                await App.SQLiteDbUsuario.SaveItemAsync(novoUsuario);
                //await DisplayAlert("Sucesso!", "Usuário cadastrado com sucesso.", "OK");
                App.Current.MainPage = new AppShell();

                if (confirmar)
                {
                    tbUsuario.Text = "";
                    tbRemuneracao.Text = "";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Ocorreu um erro ao cadastrar o usuário: {ex.Message}", "OK");
            }
        }

    }
}
