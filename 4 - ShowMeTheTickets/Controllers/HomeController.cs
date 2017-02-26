﻿using GogoKit;
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
        public static int categoryID = 1205;

        //private static readonly Uri RedirectUri = new Uri("https://localhost:50442/getEvents");

        public HomeController()
        {
            _viagogoClient = new ViagogoClient(new ProductHeaderValue("ShowMeTheTickets"), CLIENT_ID,
            CLIENT_SECRET);

            
        }

        public async Task<ActionResult> Index()
        {
            await getEvents();
            return View();
        }

       
        public async Task getEvents()
        {
            Debug.WriteLine("Obtaining client access token\n");
            var token = await _viagogoClient.OAuth2.GetClientAccessTokenAsync(Scopes);
            Debug.WriteLine("Token received\n");
            await _viagogoClient.TokenStore.SetTokenAsync(token);
            Debug.WriteLine(token);


            Debug.WriteLine("Getting all events in category\n");
            var events = await _viagogoClient.Events.GetAllByCategoryAsync(categoryID);
            ViewBag.categoryID = categoryID;
            Debug.WriteLine(events.Count + " events found\n");
            if (events.Count > 0)
            {
                ViewBag.eventList = events;

                Debug.WriteLine(events[0].ListingsLink + "\n");
                Debug.WriteLine(events[0].Venue.City + "\n");
                Debug.WriteLine(events[0].Venue.Country.Name + "\n");
                Debug.WriteLine(events[0].Venue.Name + "\n");
                Debug.WriteLine(events[0].MinTicketPrice.Display + "\n");
                Debug.WriteLine(events[0].MinTicketPrice.Amount + "\n");


            }


        }


    }
}