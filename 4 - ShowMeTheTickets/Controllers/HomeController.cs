/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GogoKit;
*/


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

        private static readonly Uri RedirectUri = new Uri("https://localhost:50442/yo");

        public HomeController()
        {
            Debug.WriteLine("My debug string here\n");

            _viagogoClient = new ViagogoClient(new ProductHeaderValue("ShowMeTheTickets"), CLIENT_ID,
            CLIENT_SECRET);

            getEvents();



        }

        public async void getEvents()
        {
            var token = await _viagogoClient.OAuth2.GetClientAccessTokenAsync(Scopes);
            await _viagogoClient.TokenStore.SetTokenAsync(token);
            Debug.WriteLine(token);

            var events = await _viagogoClient.Events.GetAllByCategoryAsync(1207);
            var eventName = events[0].Name;
            
            Debug.WriteLine("Event " + eventName + "\n");


            /*foreach (object o in searchResults)
            {
                Debug.WriteLine(o);

            }*/



        }

        [Route("")]
        public ActionResult Index()
        {
            return View();
        }



        /*[Route("hello")]
        public async Task<ActionResult> hello(string code, string state)
        {
            Debug.WriteLine("hit\n");
            var token = await _viagogoClient.OAuth2.GetAuthorizationCodeAccessTokenAsync(code, RedirectUri, Scopes);

            // Save the token for making API requests later. 
            await _viagogoClient.TokenStore.SetTokenAsync(token);

            var searchResults = await _viagogoClient.Search.GetAllAsync("lady gaga");

            return Content(token.AccessToken);
        }*/





        



    }
}