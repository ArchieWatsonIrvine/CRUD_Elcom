using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

public class StockItemServices
{
    public static StockItemServices Instance = new StockItemServices();
    SQLiteAsyncConnection db;
    const string DbFileName = "ElcomSqLite.db3";

    //not used
    //const SQLiteOpenFlags Flags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;

    string getDbPath()
    {
            string basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(basePath, DbFileName);
    }
    //this should be called once when the application starts OnStart()
    public async Task initDatabase()
	{
        db = new SQLiteAsyncConnection(getDbPath());
        await db.CreateTableAsync<stockItem>();
    }

 //   public async Task<int> addItem(string manufactureName, string manufactureCode, int cartonQty)
	//{
 //       var item = new stockItem
 //       {
 //           ManufactureName = manufactureName,
 //           ManufactureCode = manufactureCode,
 //           CartonQty = cartonQty,
 //       };
 //       return await db.InsertAsync(item);
	//}

    public Task<int> removeItem(stockItem ID)
    {
        return db.DeleteAsync(ID);
    }

    public Task<List<stockItem>> getItemsAsync()
    {
        //returns all items in the table
        return db.Table<stockItem>().ToListAsync();
    }

    public Task<List<stockItem>> getItemsBelowQuantityLevelAsync(int quantity)
    {
        return db.QueryAsync<stockItem>("SELECT * FROM [stockItem] WHERE [CartonQty] < " + quantity.ToString());
    }

    public async Task<stockItem> getItemAsync(int ID)
    {
        //finds stock item record based on ID
        //these all work the same as the line below
        //db.FindWithQueryAsync<stockItem>("SELECT * FROM stockItem WHERE StockItemID = " + ID.ToString());
        //db.QueryAsync<stockItem>("SELECT * FROM stockItem WHERE StockItemID =" + ID.ToString());
        //return await db.Table<stockItem>().Where(i => i.StockItemId == ID).FirstOrDefaultAsync();

        return await db.GetAsync<stockItem>(ID);
    }

    //This handles adding and updating
    public Task<int> saveItemAsync(stockItem item)
    {
        if (item.StockItemId != 0)
        {
            return db.UpdateAsync(item);
        }
        else
        {
            return db.InsertAsync(item);
        }
    }


}