using System.Collections.Generic;
using System.Threading.Tasks;
using AVosCocktails.Model;
using SQLite;


namespace AVosCocktails
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<BDCocktail>().Wait();
        }

        public Task<List<BDCocktail>> GetCocktailAsync()
        {
            return _database.Table<BDCocktail>().ToListAsync();
        }


        public Task<int> SaveCocktailAsync(BDCocktail LCocktail)
        {
            return _database.InsertAsync(LCocktail);
        }

        public Task<int> DeleteAllCocktails()
        {
            return _database.DeleteAllAsync<BDCocktail>();
        }

    }
}
