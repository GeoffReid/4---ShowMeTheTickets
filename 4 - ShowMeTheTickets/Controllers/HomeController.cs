using GogoKit;
using GogoKit.Enumerations;
using GogoKit.Models.Response;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace _4___ShowMeTheTickets.Controllers
{
    public class HomeController : Controller
    {
        private const string CLIENT_ID = "TaRJnBcw1ZvYOXENCtj5";
        private const string CLIENT_SECRET = "ixGDUqRA5coOHf3FQysjd704BPptwbk6zZreELW2aCYSmIT8XJ9ngvN1MuKV";
        private readonly IViagogoClient _viagogoClient;

        private static readonly string[] Scopes = { "(no scope)" };

        private static readonly Uri RedirectUri = new Uri("https://localhost:50442/getEvents");

        public HomeController()
        {
            Debug.WriteLine("Start\n");

            _viagogoClient = new ViagogoClient(new ProductHeaderValue("ShowMeTheTickets"), CLIENT_ID,
            CLIENT_SECRET);

            getEvents();
        }

        public ActionResult Index()
        {

            return View();
        }

        /* Aware that this shouldn't be a void. At the moment the only way I seem to be able to return a
         category without getting a permission error is by doing it this way*/
        public async void getEvents()
        {
            Debug.WriteLine("Getting client access token\n");
            var token = await _viagogoClient.OAuth2.GetClientAccessTokenAsync(Scopes);
            await _viagogoClient.TokenStore.SetTokenAsync(token);
            Debug.WriteLine(token);

            Debug.WriteLine("Getting all events in category\n");
            var events = await _viagogoClient.Events.GetAllByCategoryAsync(1207);
            var eventName = events[0].Name;
            
            Debug.WriteLine("Event 1 title: " + eventName + "\n");
        }


        












    }
}