using System.Collections;
using System.Collections.Generic;

namespace Rocky_Models.ViewModels
{
    public class ProductUserVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IList<Product> ProductList { get; set; } = new List<Product>();
        //public ProductUserVM() {  ProductList = new List<Product>(); }
    }
}
