using Microsoft.Build.Framework;


public class BasicProductModel
    {
        [Required]
        public string Name { get; set; }
        public string Sku { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string Unit { get; set; }
        public int CatrgoryId  { get; set; }

        


    }
