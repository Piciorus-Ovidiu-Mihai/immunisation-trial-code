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
    public class SurveyQuestionController : Controller
    {
        private readonly IConfiguration _Configure;
        private readonly IMapper _mapper;
        private readonly string apiBaseUrl;
        private readonly IHttpClientFactory _clientFactory;

        public SurveyQuestionController(IConfiguration configuration, IMapper mapper, IHttpClientFactory clientFactory)
        {
            _Configure = configuration;
            _mapper = mapper;

            _clientFactory = clientFactory;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/ListQuestions";

            using var Response = await client.GetAsync(endpoint);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                List<SurveyQuestion> questions = JsonConvert.DeserializeObject<List<SurveyQuestion>>(content);
                List<SurveyQuestionViewModel> questionsViewModel = new List<SurveyQuestionViewModel>();

                foreach (SurveyQuestion question in questions)
                {
                    var questionViewModel = _mapper.Map<SurveyQuestionViewModel>(question);
                    questionsViewModel.Add(questionViewModel);
                }
                return View(questionsViewModel);
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> UpdateQuestion(int id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetQuestionById?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                SurveyQuestion surveyQuestion = JsonConvert.DeserializeObject<SurveyQuestion>(content);
                var surveyQuestionViewModel = _mapper.Map<SurveyQuestionViewModel>(surveyQuestion);

                return View(surveyQuestionViewModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateQuestion(SurveyQuestionViewModel surveyQuestionViewModel)
        {
            var client = _clientFactory.CreateClient();
            var surveyQuestion = _mapper.Map<SurveyQuestion>(surveyQuestionViewModel);
            var content = new StringContent(JsonConvert.SerializeObject(surveyQuestion), Encoding.UTF8, "application/json");
            var endpoint = apiBaseUrl + "/UpdateQuestion?survey=" + surveyQuestion;

            using var Response = await client.PostAsync(endpoint, content);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("CreateQuestion", "Survey", new { id = surveyQuestionViewModel.SurveyId });
            }
            else if (Response.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.Clear();
                return View();
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddNewAnswer(int id)
        {
            var client = _clientFactory.CreateClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            string endpoint = apiBaseUrl + "/AddNewAnswer?id=" + id;

            using var Response = await client.PostAsync(endpoint, content);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("UpdateQuestion", new { id });
            }
            else
            {
                return View("Error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetSurveyIdForQuestion?id=" + id;

            var surveyId = client.GetAsync(endpoint).Result.Content.ReadAsAsync<int>().Result;

            endpoint = apiBaseUrl + "/DeleteQuestion?id=" + id;

            using var Response = await client.DeleteAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("CreateQuestion", "Survey", new { id = surveyId });
            }
            else
            {
                return View("Error");
            }
        }
    }
}
