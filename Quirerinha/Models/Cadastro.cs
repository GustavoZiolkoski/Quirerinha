using SQLite;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirerinha.Models
{
    public class Usuarios
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int ID { get; set; }
        public string Nome { get; set; }
        public double Remuneracao { get; set; } 
    }
}
