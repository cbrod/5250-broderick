﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Mine.Models;

namespace Mine.Services
{
    public class DatabaseService
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() =>
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

        public DatabaseService()
        {
            InitializeAsync().SafeFireAndForget(false);
        }

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType.Name == typeof(ItemModel).Name))
                {
                    await Database.CreateTablesAsync(CreateFlags.None, typeof(ItemModel)).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<int> CreateAsync(ItemModel item)
        {
            return Database.InsertAsync(item);
        }

        public Task<ItemModel> ReadAsync(string id)
        {
            return Database.Table<ItemModel>().Where(i => i.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public Task<int> UpdateAsync(ItemModel item)
        {
            return Database.UpdateAsync(item);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var item = await ReadAsync(id);
            if (item == null)
            {
                return false;
            }

            var result = await Database.DeleteAsync(item);
            return (result == 1);
        }

        public Task<List<ItemModel>> IndexAsync()
        {
            return Database.Table<ItemModel>().ToListAsync();
        }
    }
}