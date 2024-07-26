using CommerceBuilder.Catalog;
using CommerceBuilder.Common;
using CommerceBuilder.Products;
using CommerceBuilder.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using TPIPlugin;
using TPIPlugin.Models;

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

           return View("~/Plugins/TPIPlugin/Views/GenericAPI.cshtml");
                                                
        }
        public async Task<ActionResult> DemoAPI(string ApiUrlParam, string requiredParameter)
    {
        try
        {
            string api;
            if (string.IsNullOrEmpty(requiredParameter))
            {
                api = ApiUrlParam;
            }
            else
            {
                api = ApiUrlParam + requiredParameter;
            }
            HttpResponseMessage response = await _client.GetAsync(api);
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var parsedJson = JToken.Parse(jsonResponse);
            string formattedJson = parsedJson.ToString(Formatting.Indented);

            return Json(formattedJson);
        }
        catch (Exception ex)
        {
            return Json( ex.Message+"\n It could be that a parameter was requried and none was given or the API Url was incorrect" );
        }
    }

        [HttpPost]
        public async Task<ActionResult> CallApi(APIModel apiModel)
        {
        string jsonResponse = "";
        if (!string.IsNullOrEmpty(apiModel.ApiResponse.Parameter))
        {
            apiModel.ApiUrl = apiModel.ApiUrl + apiModel.ApiResponse.Parameter;
        }
        try
        {
            HttpResponseMessage response = await _client.GetAsync(apiModel.ApiUrl);
            response.EnsureSuccessStatusCode();
            jsonResponse = await response.Content.ReadAsStringAsync();

        }
        catch (Exception ex) {

            ViewBag.Error = ex.Message.ToString();
            return View("~/Plugins/TPIPlugin/Views/GenericAPI.cshtml");

        }
        JArray jArray = JArray.Parse(jsonResponse);
        if (jArray.Count == 0)
        {
            ViewBag.Error = "No Results Found";
            return View("~/Plugins/TPIPlugin/Views/GenericAPI.cshtml");
        }
        MappedResponse mappedResponse = new MappedResponse();
        if (jArray.Count > 0)
        {
            mappedResponse.Name = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Name,
                Prefix = Utilities.FindNamePropertyPrefix(jArray[0], "", apiModel.ApiResponse?.Name)
            };
            mappedResponse.Price = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Price,
                Prefix = Utilities.FindNamePropertyPrefix(jArray[0], "", apiModel.ApiResponse?.Price)
            };
            mappedResponse.Description = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Description,
                Prefix = Utilities.FindNamePropertyPrefix(jArray[0], "", apiModel.ApiResponse?.Description)
            };
            mappedResponse.Summary = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Summary,
                Prefix = Utilities.FindNamePropertyPrefix(jArray[0], "", apiModel.ApiResponse?.Summary)
            };
            mappedResponse.Image = new JsonEntity
            {
                EntityName = apiModel.ApiResponse?.Image,
                Prefix = Utilities.FindNamePropertyPrefix(jArray[0], "", apiModel.ApiResponse?.Image)
            };
        }
        mappedResponse.AddPrefixes();
   
        List<JsonArray> list = new List<JsonArray>();

        foreach(JObject item in jArray)
        {
            JsonArray jsonArray = new JsonArray();
            jsonArray.MappedResponse = mappedResponse;
            jsonArray.JsonObject = item;
            list.Add(jsonArray);
        }
      
        
        return View("~/Plugins/TPIPlugin/Views/_ShowsListDynamic.cshtml", list);

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

