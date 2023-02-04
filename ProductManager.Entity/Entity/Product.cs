using ProductManager.Base.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductManager.Entity.Entity
{
    [Serializable()]
    public class Product : BaseEntity
    {
        
        [MaxLength(50)]
        public string category { get; set; }
        [Required]
        [StringLength(100)]
        public string description { get; set; }
        [Required]
        public int price { get; set; }
        [Required]
        public int quantity { get; set;}
        [Required]
        public string suppliers { get; set; }

        [JsonConstructor]
        public Product(int id, string name, string category, string description, int price, int quantity, string suppliers)
        {

            Id = id;
            this.name = name;
            this.category = category;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
            this.suppliers = suppliers;
        }
    }
}
