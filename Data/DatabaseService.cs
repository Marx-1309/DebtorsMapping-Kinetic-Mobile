using DebtorsMapping.Models;
using SQLite;

namespace DebtorsMapping.Data
{
    public class DatabaseService
    {
        private SQLiteAsyncConnection _connection;
        private readonly string _dbPath;

        public DatabaseService(string dbPath)
        {
            _dbPath = dbPath;
        }

        private async Task Init()
        {
            if (_connection != null)
                return;

            _connection = new SQLiteAsyncConnection(_dbPath);
            await _connection.CreateTableAsync<Customer>();

            // Seed data only if the database is empty
            if (await _connection.Table<Customer>().CountAsync() == 0)
            {
                var seedData = new List<Customer>
                {
                    new Customer { Name = "ABC Pharmacy (No Location)" },
                    new Customer { Name = "Eros Airport", Latitude = -22.6000, Longitude = 17.0833 },
                    new Customer { Name = "Grove Mall of Namibia", Latitude = -22.6133, Longitude = 17.0986 },
                    new Customer { Name = "Joe's Beerhouse", Latitude = -22.5598, Longitude = 17.0935 },
                    new Customer { Name = "Katutura State Hospital", Latitude = -22.5332, Longitude = 17.0601 },
                    new Customer { Name = "Maerua Mall", Latitude = -22.5833, Longitude = 17.0928 },
                    new Customer { Name = "Namibia Craft Centre", Latitude = -22.5694, Longitude = 17.0827 },
                    new Customer { Name = "Shoprite Independence Ave", Latitude = -22.5682, Longitude = 17.0831 },
                    new Customer { Name = "Wernhil Park", Latitude = -22.5649, Longitude = 17.0822 },
                    new Customer { Name = "Windhoek Stationers (No Location)" }
                };
                await _connection.InsertAllAsync(seedData);
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            await Init();
            return await _connection.Table<Customer>().OrderBy(c => c.Name).ToListAsync();
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            await Init();
            return await _connection.UpdateAsync(customer);
        }
    }
}