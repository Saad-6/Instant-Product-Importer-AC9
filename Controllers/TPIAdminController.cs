﻿using CommerceBuilder.Catalog;
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
using System.Runtime.Caching;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Mvc;
using System.Xml.Linq;
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
        public async Task<ActionResult> DemoAPI(string apiUrlParam)
        {
    
            try
            {
                // Build the API URL
                HttpResponseMessage response = await _client.GetAsync(apiUrlParam);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var jsonArray = JsonConvert.DeserializeObject<List<dynamic>>(jsonResponse);
                if (jsonArray?.Count > 0)
                {
                    var firstObject = jsonArray[0];
                    string formattedJson = JsonConvert.SerializeObject(firstObject, Formatting.Indented);
                    return Json(formattedJson);
                }
                else
                {
                    // Handle empty array or error case
                    return Json("No data found"); // Or return a specific error response
                }
        }
            catch (Exception ex)
            {
                return Json(ex.Message + "\n It could be that a parameter was required and none was given or the API URL was incorrect");
            }
        }

        [HttpPost]
        public async Task<ActionResult> CallApi(APIModel apiModel)
        {
            apiModel.DefaultValuesIfNotSet();
            // Declarations
            string jsonResponse = "";
            JArray jArray;
            JObject jObject;
            JToken parsedJson;
            JObjectMapped jObjectMapped;
            string view = apiModel.View;
            MappedResponse mappedResponse = new MappedResponse();
            // Calling the API
            try
            {
                HttpResponseMessage response = await _client.GetAsync(apiModel.ApiUrl);
                response.EnsureSuccessStatusCode();
                jsonResponse = await response.Content.ReadAsStringAsync();
                parsedJson = JToken.Parse(jsonResponse);

            }
            catch (Exception ex) {

                ViewBag.Error = ex.Message.ToString();
                return View("~/Plugins/TPIPlugin/Views/GenericAPI.cshtml");
            }
            // Checking if the data returned is a single json object
            if(parsedJson is JObject)
            {
                jObject = JObject.Parse(jsonResponse);
                mappedResponse = Utilities.GetMappedResponse(apiModel, jObject);
                jObjectMapped = new JObjectMapped(); 
                jObjectMapped.JsonObject = jObject;
                jObjectMapped.MappedResponse = mappedResponse;
                return View("~/Plugins/TPIPlugin/Views/Single.cshtml",jObjectMapped);

            }
            //  the data is an array of objects
            jArray = JArray.Parse(jsonResponse);
         
            if (jArray.Count == 0)
            {
                ViewBag.Error = "No Results Found";
                return View("~/Plugins/TPIPlugin/Views/GenericAPI.cshtml");
            }
            mappedResponse = Utilities.GetMappedResponse(apiModel, jArray[0].ToObject<JObject>());
       
            List<JObjectMapped> mappedlist = new List<JObjectMapped>();

            foreach(JObject item in jArray)
            {
                JObjectMapped jsonArray = new JObjectMapped();
                jsonArray.MappedResponse = mappedResponse;
                jsonArray.JsonObject = item;
                mappedlist.Add(jsonArray);
            }

            if (apiModel.View == "Tabular")
            {
                return View("~/Plugins/TPIPlugin/Views/Tabular.cshtml", mappedlist);
            }
        
            return View("~/Plugins/TPIPlugin/Views/Grid.cshtml", mappedlist);

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

