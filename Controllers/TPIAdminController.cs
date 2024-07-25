using CommerceBuilder.Web.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using TPIPlugin;

public class TPIAdminController : AbleAdminController
    {
        HttpClient _client;

        string _url = "http://api.tvmaze.com/search/shows?q=";
        public TPIAdminController()
        {
            _client = new HttpClient();
        }
        public ActionResult Index()
        {

           return View("~/Plugins/TPIPlugin/Views/Index.cshtml");

        }
        public async Task<ActionResult> GetShow(string query)
        {
            HttpResponseMessage response = await _client.GetAsync($"http://api.tvmaze.com/search/shows?q={query}");
            response.EnsureSuccessStatusCode();
            string jsonResponse = await response.Content.ReadAsStringAsync();
            List<TvMaze> tvMazeList = JsonConvert.DeserializeObject<List<TvMaze>>(jsonResponse);

            return PartialView("~/Plugins/TPIPlugin/Views/_ShowsList.cshtml");
        }

    }

