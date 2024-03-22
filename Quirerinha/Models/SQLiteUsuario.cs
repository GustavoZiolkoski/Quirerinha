﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quirerinha.Models
{
    internal class SQLiteUsuario
    {
        SQLiteAsyncConnection db;

        public SQLiteUsuario(string dbPath)
        {
            db = new SQLiteAsyncConnection(dbPath);
            db.CreateTableAsync<Models.Usuario>().Wait();
        }

        public Task<int> SaveItemAsync(Models.Usuario registro)
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
        public Task<int> DeleteItemAsync(Models.Usuario registro)
        {
            return db.DeleteAsync(registro);
        }

        //Delete all
        public void DeleteAllUsers()
        {
            db.DropTableAsync<Models.Usuario>().Wait();
            db.CreateTableAsync<Models.Usuario>().Wait();
        }
        public Task<Models.Usuario> GetItemAsync(int personId)
        {
            return db.Table<Models.Usuario>().Where(i => i.ID == personId).FirstOrDefaultAsync();
        }

        //Read All Items  
        public Task<List<Models.Usuario>> GetItemsAsync()
        {
            return db.Table<Models.Usuario>().ToListAsync();
        }

        /*public async Task<Usuario> GetLastLoggedInUser()
        {
            return await db.Table<Usuario>()
            .OrderByDescending(u => u.UltimoLogin)
            .FirstOrDefaultAsync();
        }


        public Task<Models.Usuario> GetItemAsync2(string login, string senha)
        {
            return db.Table<Models.Usuario>()
                .Where(r => r.Nome == login)
                .Where(r => r.Senha == senha)
                .FirstOrDefaultAsync();
        }*/

        public Task<int> SaveItemAsyncLogin(Models.Usuario registro)
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

