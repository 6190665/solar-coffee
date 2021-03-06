using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarCoffee.WebAPI.ViewModels
{
    /// <summary>
    /// 
    /// Product Model DTO
    /// </summary>
    public class ProductModel
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsTaxable { get; set; }
        public bool IsArchived { get; set; }


    }
}
