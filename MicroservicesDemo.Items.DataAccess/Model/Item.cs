using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroservicesDemo.Items.DataAccess.Model
{
    public class Item
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public int ItemNumber { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
