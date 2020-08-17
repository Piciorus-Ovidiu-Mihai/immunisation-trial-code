using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ClinicalSurveyApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SurveyClassLibrary.Models;

namespace ClinicalSurveyApp.Controllers
{
    
    public class UserSurveyResponseController : Controller
    {

        private readonly IConfiguration _Configure;
        private readonly IMapper _mapper;
        private readonly string apiBaseUrl;
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        
        public UserSurveyResponseController(IConfiguration configuration, IMapper mapper, IHttpClientFactory clientFactory, UserManager<ApplicationUser> userManager)
        {

            _Configure = configuration;
            _mapper = mapper;

            _clientFactory = clientFactory;
            _userManager = userManager;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");

        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/ListUserResponses";

            using var Response = await client.GetAsync(endpoint);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                List<UserSurveyResponse> responses = JsonConvert.DeserializeObject<List<UserSurveyResponse>>(content);
                List<UserSurveyResponseViewModel> responsesViewModel = new List<UserSurveyResponseViewModel>();

                foreach (UserSurveyResponse response in responses)
                {
                    var responseViewModel = _mapper.Map<UserSurveyResponseViewModel>(response);
                    responsesViewModel.Add(responseViewModel);
                }
                return View(responsesViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> IndexResponse()
        {
            var client = _clientFactory.CreateClient();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            string endpoint = apiBaseUrl + "/ListResponsesForUser?userId=" + user.Id;

            using var Response = await client.GetAsync(endpoint);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                List<UserSurveyResponse> responses = JsonConvert.DeserializeObject<List<UserSurveyResponse>>(content);
                List<UserSurveyResponseViewModel> responsesViewModel = new List<UserSurveyResponseViewModel>();

                foreach (UserSurveyResponse response in responses)
                {
                    var responseViewModel = _mapper.Map<UserSurveyResponseViewModel>(response);
                    responsesViewModel.Add(responseViewModel);
                }
                return View(responsesViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> ShowResponse(int id)
        {
            var client = _clientFactory.CreateClient();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string endpoint = apiBaseUrl + "/GetSurveyResponseForSurveyUser?idSurvey=" + id + "&userId=" + user.Id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                UserSurveyResponse userResponse = JsonConvert.DeserializeObject<UserSurveyResponse>(content);
                var userResponseViewModel = _mapper.Map<UserSurveyResponseViewModel>(userResponse);

                return View(userResponseViewModel);
            }
            else
            {
                return View();
            }

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> ShowResponseForUser(int id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetSurveyResponseById?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                UserSurveyResponse userResponse = JsonConvert.DeserializeObject<UserSurveyResponse>(content);
                var userResponseViewModel = _mapper.Map<UserSurveyResponseViewModel>(userResponse);

                return View("ShowResponse", userResponseViewModel);
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public async Task<IActionResult> GetUserSurveyResponse(int id)
        {
            var client = _clientFactory.CreateClient();
            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string endpoint = apiBaseUrl + "/GetSurveyResponseById?id=?" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                UserSurveyResponse userResponse = JsonConvert.DeserializeObject<UserSurveyResponse>(content);
                var userResponseViewModel = _mapper.Map<UserSurveyResponseViewModel>(userResponse);

                return View(userResponseViewModel);
            }
            else
            {
                return View();
            }
        }

    }
}
