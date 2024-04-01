using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirerinha.Models
{
    public class SQLiteCadastroElemento
    {
        SQLiteAsyncConnection db;

        public SQLiteCadastroElemento(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<CadastroElemento>(CreateFlags.None).Wait();
        }
        public Task<int> SaveItemAsync(Models.CadastroElemento registro)
        {
            if (registro.ID != 0)
            {
                return db.UpdateAsync(registro);
            }
            else
            {
                return db.InsertAsync(registro);
            }
        }
        //Delete
        public Task<int> DeleteItemAsync(Models.CadastroElemento registro)
        {
            return db.DeleteAsync(registro);
        }

        public Task<int> DeleteItem(Models.CadastroElemento cadastroElemento)
        {
            /*return db.DeleteAsync(ID);*/
            return db.DeleteAsync(cadastroElemento);
        }


        //Delete all - deleta a tabela e cria outra
        public void DeleteAllCadastroElemento()
        {
            db.DropTableAsync<Models.CadastroElemento>().Wait();
            db.CreateTableAsync<Models.CadastroElemento>().Wait();
        }

        // Deleta todos os itens da tabela, mas não a tabela
        public void DeleteAsyncCadastroElemento()
        {
            db.DeleteAllAsync<Models.CadastroElemento>().Wait();

        }


        //Read All Items  
        public Task<List<Models.CadastroElemento>> GetItemsAsync()
        {
            return db.Table<Models.CadastroElemento>().ToListAsync();
        }

        // Lista itens na ordem inversa
        public Task<List<Models.CadastroElemento>> GetItemsAsyncOrder()
        {
            /*return db.Table<Models.CadastroElemento>().OrderByDescending(d => d.DataAgora).ToListAsync();*/
            return db.Table<Models.CadastroElemento>().OrderByDescending(d => d.ID).ToListAsync();
        }

        public Task<Models.CadastroElemento> GetItemAsync(int id)
        {
            return db.Table<Models.CadastroElemento>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }


        public Task<Models.CadastroElemento> GetItemAsyncLast()
        {
            return db.Table<Models.CadastroElemento>().OrderByDescending(i => i.ID).Take(1).FirstOrDefaultAsync();
        }


        // verificação
        public Task<int> SaveItemAsyncCadastroElemento(Models.CadastroElemento registro)
        {

            if (registro.ID != 0)
            {
                return db.UpdateAsync(registro);
            }
            else
            {
                return db.InsertAsync(registro);
            }
        }

        public Task<int> UpdateCadastroElemento(Models.CadastroElemento cadastroElemento)
        {
            return db.UpdateAsync(cadastroElemento);
        }
    }
}
