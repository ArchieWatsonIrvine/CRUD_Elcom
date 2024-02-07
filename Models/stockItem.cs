using SQLite;
using System;
using System.Security.Cryptography.X509Certificates;

[Table("stockItem")]
public class stockItem
{
    [PrimaryKey, AutoIncrement]
    public int StockItemId { get; set; }
    [MaxLength(100), Indexed]
    public string ManufactureName { get; set; }
    [MaxLength(100), Indexed]
    public string ManufactureCode { get; set; }
    [Indexed]
    public int CartonQty { get; set; }
}