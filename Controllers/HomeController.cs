using MASHWAR.ExternalAPI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASHWAR.Models;

namespace MASHWAR.Controllers
{
    public class HomeController : Controller
    {
        private APICalls _api;

        public HomeController(APICalls api)
        {
            _api = api;
        }
        public async Task<IActionResult> Index()
        {
            var UserInfo = new UserInfo();
           
            return View(UserInfo);
        }

        [HttpPost]
        public async Task<IActionResult> Result(UserInfo userInfo)
        {

         
            if (string.IsNullOrEmpty(userInfo.lat) || !string.IsNullOrEmpty(userInfo.longt))
            {
                userInfo.lat = "24.7136";
                userInfo.longt = "46.6753";
            }

           var Gogresult =  await _api.GoogleMap(new YelpParam
                            {
                                latitude = double.Parse(userInfo.lat),
                                longitude = double.Parse(userInfo.longt),
                                term = userInfo.city
                            });
           

            string message = null;
            if (Gogresult.results.Count != 0)
            {
                message = "select one option from the following  ";
                foreach (var buss in Gogresult.results.Take(20))
                {
                    message += "place name " + buss.name + "with rating " + buss.rating + "and review count" + buss.user_ratings_total ;


                }
            }
            else
            {
                message = "Please give recomendation for a resturent in " + userInfo.city;
            }





            message += " for individual  how is Name " + userInfo.Name + "and age " + userInfo.Age + " and gander is " + userInfo.gender + " to  have the best time ";




            var Gptresult = await _api.Gptcompletions(message);

            var result = Gptresult.choices.FirstOrDefault().message.content;
            var response = new response { content = result };


            return View(response);
        }
    }

  public  class response
    {
      public  string content { get; set; }
    }
}
