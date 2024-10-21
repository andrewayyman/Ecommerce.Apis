using Ecommerce.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Data
{
    public static class StoreContextSeed
    {


        public async static Task SeedAsync( StoreContext _dbContext )
        {

            #region SeedBrands
            // ---------------- Brands ---------------- // 
            var brandsData = File.ReadAllText(@"../Ecommerce.Repository\Data\DataSeeding\brands.json");

            // from json to any -->> deserialize 
            // any to json      -->> serialize 

            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

            if ( brands.Count() > 0 )
            {
                #region Another way to fix id conflict
                //brands = brands.Select(b=> new ProductBrand()
                //{
                //    Name = b.Name 
                //}).ToList(); 
                #endregion

                if ( _dbContext.ProductBrands.Count() == 0 )
                {
                    foreach ( var brand in brands )
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            #endregion


            #region SeedCategories
            var categoriesData = File.ReadAllText(@"../Ecommerce.Repository\Data\DataSeeding\categories.json");
            var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

            if ( categories.Count() > 0 )
            {

                if ( _dbContext.ProductCategories.Count() == 0 )
                {
                    foreach ( var category in categories )
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion


            #region SeedProducts
            var productsData = File.ReadAllText(@"../Ecommerce.Repository\Data\DataSeeding\products.json");
            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if ( products.Count() > 0 )
            {

                if ( _dbContext.Products.Count() == 0 )
                {
                    foreach ( var product in products )
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }
            #endregion



        }





    }
}
