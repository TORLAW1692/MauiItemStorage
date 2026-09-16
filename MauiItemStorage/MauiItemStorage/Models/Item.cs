using SQLite;

namespace MauiItemStorage.Models
{
    public class Item
    {
        [PrimaryKey]
        public int ItemID { get; set; }

        public string ItemName { get; set; }

        public string ItemDescription { get; set; }
    }
}