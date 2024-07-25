using AbleCommerce.Areas.Admin.Models;
using CommerceBuilder.Catalog;
using CommerceBuilder.Common;
using CommerceBuilder.Products;
using CommerceBuilder.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.Mvc;
using TPIPlugin;

public class TPIAdminController : AbleAdminController
    {
        HttpClient _client;
        IProductRepository _productRepository;
        ICategoryRepository _categoryRepository;
        public TPIAdminController(IProductRepository Repository,ICategoryRepository categoryRepository)
        {
            _client = new HttpClient();
            _productRepository = Repository;
            _categoryRepository = categoryRepository;
        }
        public ActionResult Index()
        {

           return View("~/Plugins/TPIPlugin/Views/Index.cshtml");

        }
        public async Task<ActionResult> GetShow(string query="")
        {
            HttpResponseMessage response = await _client.GetAsync($"http://api.tvmaze.com/search/shows?q={query}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<TvMaze> tvMazeList = JsonConvert.DeserializeObject<List<TvMaze>>(jsonResponse);
            return PartialView("~/Plugins/TPIPlugin/Views/_ShowsList.cshtml",tvMazeList);
        }
        public ActionResult AddNewProduct(BasicProductModel model) 
        {
        if (model == null)
        {
            return Json(new { success = false });
        }
        var category = _categoryRepository.FindByName(model.Category).FirstOrDefault();
        if (category == null)
        {
            category = new Category();
            category.Name = model.Category;
            category.Store = AbleContext.Current.Store;
            _categoryRepository.Save(category);
        }
        Product product = new Product();
        product.Name = model.Name;
        product.Sku = model.Sku;
        product.ImageUrl = model.ImageUrl;
        product.ThumbnailUrl = model.ImageUrl;
        product.Categories.Add(category.Id);
        product.Description = model.Description;
        product.Price = model.Price;
        product.CostOfGoods = model.Cost;
        product.Unit = model.Unit;
        product.Store = AbleContext.Current.Store;
        _productRepository.Save(product);

        return Json(new { success = true });
    }


    }

