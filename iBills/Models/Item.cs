using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace iBills.Models
{
    [Table("Items")]
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

    }
}
