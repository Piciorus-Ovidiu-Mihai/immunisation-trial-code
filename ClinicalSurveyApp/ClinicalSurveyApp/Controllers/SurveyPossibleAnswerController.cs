using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClinicalSurveyApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SurveyClassLibrary.Models;

namespace ClinicalSurveyApp.Controllers
{
    public class SurveyPossibleAnswerController : Controller
    {

        private readonly IConfiguration _Configure;
        private readonly string apiBaseUrl;
        private readonly IHttpClientFactory _clientFactory;

        public SurveyPossibleAnswerController(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _Configure = configuration;
            _clientFactory = clientFactory;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAnswer(int id)
        {
            var client = _clientFactory.CreateClient();


            string endpoint = apiBaseUrl + "/GetQuestionIdForAnswer?id=" + id;

            var questionId = client.GetAsync(endpoint).Result.Content.ReadAsAsync<int>().Result;

            endpoint = apiBaseUrl + "/DeleteAnswer?id=" + id;

            using var Response = await client.DeleteAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
               return RedirectToAction("UpdateQuestion", "SurveyQuestion", new { id = questionId });
            }
            else
            {
                return View("Error");
            }
        }
    }
}
