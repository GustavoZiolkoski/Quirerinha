using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirerinha.Models
{
    public class SQLiteUsuario
    {
        SQLiteAsyncConnection db;

        public SQLiteUsuario(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Models.Usuarios>().Wait();
        }

        public Task<int> SaveItemAsync(Models.Usuarios registro)
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
        public Task<int> DeleteItemAsync(Models.Usuarios registro)
        {
            return db.DeleteAsync(registro);
        }

        //Delete all
        public void DeleteAllUsers()
        {
            db.DropTableAsync<Models.Usuarios>().Wait();
            db.CreateTableAsync<Models.Usuarios>().Wait();
        }
        public Task<Models.Usuarios> GetItemAsync(int personId)
        {
            return db.Table<Models.Usuarios>().Where(i => i.ID == personId).FirstOrDefaultAsync();
        }

        //Read All Items  
        public Task<List<Models.Usuarios>> GetItemsAsync()
        {
            return db.Table<Models.Usuarios>().ToListAsync();
        }

        public async Task<Usuarios> GetLastLoggedInUser()
        {
            return await db.Table<Usuarios>()
            //.OrderByDescending(u => u.UltimoLogin)
            .FirstOrDefaultAsync();
        }


        public Task<Models.Usuarios> GetItemAsync2(string login, double senha)
        {
            return db.Table<Models.Usuarios>()
                .Where(r => r.Nome == login)
                .Where(r => r.Remuneracao == senha)
                .FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsyncLogin(Models.Usuarios registro)
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
    }
}

