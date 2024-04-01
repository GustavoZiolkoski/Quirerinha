using Quirerinha.Models;
using System.Globalization;

namespace Quirerinha.Views;

public partial class Cadastro : ContentPage
{
    public int idCadastroValue { get; set; }
    public Boolean TipoPagina { get; set; }
    public Cadastro()
    {
		InitializeComponent();
        btnSalvar.Clicked += BtnSalvar_Clicked;
        TipoPagina = true;
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        if (pckDespesas.SelectedItem == null)
        {
            await DisplayAlert("Atenção", "Você não pode salvar seu cadastro, pois o campo despesas está vazio.", "OK");
            return;
        }
        if (lbValor.Text == null)
        {
            await DisplayAlert("Atenção", "Você não pode salvar seu cadastro, pois o campo valor está vazio.", "OK");
            return;
        }

        CadastroElemento cadastroElemento = new CadastroElemento()
        {
            ID = idCadastroValue,
            Despesa = pckDespesas.SelectedItem.ToString(),
            Valor = (string.IsNullOrEmpty(lbValor.Text)) ? "" : lbValor.Text,
            Data = dpDataCadastro.Date.ToString("dd-MM-yyyy")
        };

        await App.SQLiteDbCadastroElemento.SaveItemAsync(cadastroElemento);

        await DisplayAlert("Sucesso", "Dados salvos com sucesso.", "OK");
        await (TipoPagina ? Navigation.PopToRootAsync() : Navigation.PopAsync());
    }
}