using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirerinha
{
    internal class Global
    {
        public static string Usuario { get; set; }
        public static string Remuneracao { get; set; }
    }

    public static class IdCadastroElemento
    {
        private static int CadastroElemento_ID = 0;
        public static int ValorIdCadastroElemento
        {
            get { return CadastroElemento_ID; }
            set { CadastroElemento_ID = value; }
        }
    }
    public static class Despesa
    {
        private static string despesa = "";
        public static string GlobalDespesa
        {
            get { return despesa; }
            set { despesa = value; }
        }
    }
    public static class Valor
    {
        private static string valor = "";
        public static string GlobalValor
        {
            get { return valor; }
            set { valor = value; }
        }
    }

    public static class Data
    {
        private static string data = "";
        public static string GlobalData
        {
            get { return data; }
            set { data = value; }
        }
    }
}