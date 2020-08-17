using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using ClinicalSurveyApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SurveyApiApplication.Data;
using SurveyClassLibrary.Models;

namespace ClinicalSurveyApp.Controllers
{
    public class SurveyController : Controller
    {
        private readonly IConfiguration _Configure;
        private readonly IMapper _mapper;
        private readonly string apiBaseUrl;
        private readonly IHttpClientFactory _clientFactory;
        private readonly UserManager<ApplicationUser> _userManager;

        public SurveyController(IConfiguration configuration, IMapper mapper, IHttpClientFactory clientFactory, UserManager<ApplicationUser> userManager)
        {
            _Configure = configuration;
            _mapper = mapper;

            _clientFactory = clientFactory;
            _userManager = userManager;

            apiBaseUrl = _Configure.GetValue<string>("WebAPIBaseUrl");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/ListSurveys";

            using var Response = await client.GetAsync(endpoint);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                List<Survey> surveys = JsonConvert.DeserializeObject<List<Survey>>(content);
                List<SurveyViewModel> surveysViewModel = new List<SurveyViewModel>();

                foreach (Survey survey in surveys)
                {
                    var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);
                    surveysViewModel.Add(surveyViewModel);
                }
                return View(surveysViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateSurvey()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateSurvey(SurveyViewModel model)
        {

            var survey = _mapper.Map<Survey>(model);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            survey.DateCreated = DateTime.Now;
            survey.SurveyOwner = new ApplicationUser { Id = user.Id };

            var client = _clientFactory.CreateClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(survey), Encoding.UTF8, "application/json");

            string endpoint = apiBaseUrl + "/CreateSurvey";

            using var Response = await client.PostAsync(endpoint, content);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
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

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> SurveyDetails(int? id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetByID?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                Survey survey = JsonConvert.DeserializeObject<Survey>(content);
                var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

                return View(surveyViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditSurveyDetails(int? id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetByID?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                Survey survey = JsonConvert.DeserializeObject<Survey>(content);
                var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

                return View(surveyViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> EditSurveyDetails(int id, SurveyViewModel model)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetByID?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            var content = await Response.Content.ReadAsStringAsync();

            Survey survey = JsonConvert.DeserializeObject<Survey>(content);
            var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

            surveyViewModel.SurveyName = model.SurveyName;
            surveyViewModel.SurveyDescription = model.SurveyDescription;
            surveyViewModel.AvailableFromDate = model.AvailableFromDate;
            surveyViewModel.ClosesOnDate = model.ClosesOnDate;

            survey = _mapper.Map<Survey>(surveyViewModel);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            survey.ModifiedByUser = new ApplicationUser { Id = user.Id };

            StringContent newcontent = new StringContent(JsonConvert.SerializeObject(survey), Encoding.UTF8, "application/json");
            endpoint = apiBaseUrl + "/UpdateSurvey?survey=" + survey;

            using var newResponse = await client.PostAsync(endpoint, newcontent);

            if (newResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else if (newResponse.StatusCode == System.Net.HttpStatusCode.Conflict)
            {
                ModelState.Clear();
                return View();
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            using HttpClient client = new HttpClient();

            string endpoint = apiBaseUrl + "/DeleteSurvey?id=" + id;

            using var Response = await client.DeleteAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View("Error");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddEmptyQuestion(int id)
        {
            var client = _clientFactory.CreateClient();

            StringContent content = new StringContent(JsonConvert.SerializeObject(id), Encoding.UTF8, "application/json");

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);

            string endpoint = apiBaseUrl + "/AddEmptyQuestion?id=" + id + "&userId=" + user.Id;

            using var Response = await client.PostAsync(endpoint, content);

            return RedirectToAction("CreateQuestion", new { id });

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> CreateQuestion(int? id)
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/GetByID?id=" + id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                Survey survey = JsonConvert.DeserializeObject<Survey>(content);
                var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

                return View(surveyViewModel);
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> GetSurveyAnswer(Guid? idGuid)
        {
            var client = _clientFactory.CreateClient();
            string endpoint1 = apiBaseUrl + "/GetSurveyIdByGuid?guid=" + idGuid;
            using var Response1 = await client.GetAsync(endpoint1);
            var content1 = await Response1.Content.ReadAsStringAsync();
            var id = JsonConvert.DeserializeObject<int>(content1);

            var user = await _userManager.FindByEmailAsync(User.Identity.Name);
            string endpoint = apiBaseUrl + "/ExistResponse?idSurvey=" + id + "&idUser=" + user.Id;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                bool existUserResponse = JsonConvert.DeserializeObject<bool>(content);
                if (existUserResponse == false)
                {
                    endpoint = apiBaseUrl + "/GetByGuid?id=" + idGuid;

                    using var Response2 = await client.GetAsync(endpoint);

                    if (Response2.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        content = await Response2.Content.ReadAsStringAsync();
                        Survey survey = JsonConvert.DeserializeObject<Survey>(content);
                        var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

                        return View(surveyViewModel);
                    }
                    else
                    {
                        return View();
                    }
                }
                else
                {
                    return RedirectToAction("ShowResponse", "UserSurveyResponse", new { id });
                }
            }
            else
            {
                return View();
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetSurveyAnswerForGuest(Guid? idGuid)
        {
            var client = _clientFactory.CreateClient();

            var endpoint = apiBaseUrl + "/GetByGuid?id=" + idGuid;

            using var Response = await client.GetAsync(endpoint);

            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();
                Survey survey = JsonConvert.DeserializeObject<Survey>(content);
                var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);

                return View("GetSurveyAnswer", surveyViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SaveSurveyAnswer(SurveyViewModel surveyViewModel)
        {

            bool modelError = false;
            UserSurveyResponseViewModel userSurveyResponseViewModel;
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByEmailAsync(User.Identity.Name);
                userSurveyResponseViewModel = new UserSurveyResponseViewModel
                {
                    UserId = user.Id,
                    User = user,
                    SurveyId = surveyViewModel.SurveyId,
                    CompletedOnDate = DateTime.Now,
                    UserQuestionAnswersList = new List<UserQuestionAnswerViewModel>()
                };
            }
            else
            {
                userSurveyResponseViewModel = new UserSurveyResponseViewModel
                {
                    SurveyId = surveyViewModel.SurveyId,
                    CompletedOnDate = DateTime.Now,
                    UserQuestionAnswersList = new List<UserQuestionAnswerViewModel>()
                };
            }

            foreach (var question in surveyViewModel.QuestionsList)
            {
                var userAnswersViewModel = new List<UserAnswersViewModel>();

                if (question.Type.Equals("checkbox"))
                {
                    foreach (var possibleAnswer in question.SurveyPossibleAnswersList)
                    {
                        if (possibleAnswer.Checked)
                        {
                            var userAnswer = new UserAnswersViewModel
                            {
                                SurveyPossibleAnswerId = possibleAnswer.SurveyPossibleAnswerId,
                                Checked = true,
                            };
                            userAnswersViewModel.Add(userAnswer);
                        }
                    }
                }
                else if (question.Type.Equals("radiobox"))
                {
                    foreach (var possibleAnswer in question.SurveyPossibleAnswersList)
                    {
                        if (possibleAnswer.SurveyPossibleAnswerText.Equals(question.SelectedAnswer))
                        {
                            var userAnswer = new UserAnswersViewModel
                            {
                                SurveyPossibleAnswerId = possibleAnswer.SurveyPossibleAnswerId,
                                SelectedAnswer = question.SelectedAnswer,
                            };
                            userAnswersViewModel.Add(userAnswer);
                        }
                    }
                }
                else if (question.Type.Equals("textbox"))
                {
                    if (question.SelectedAnswer != null)
                    {
                        var userAnswer = new UserAnswersViewModel
                        {
                            UserInput = question.SelectedAnswer,
                            SurveyPossibleAnswerId = question.SurveyPossibleAnswersList[0].SurveyPossibleAnswerId
                        };
                        userAnswersViewModel.Add(userAnswer);
                    }

                }

                var userQuestionAnswers = new UserQuestionAnswerViewModel
                {
                    SurveyQuestionId = question.SurveyQuestionId,
                    SurveyQuestion = question,
                    UserQuestionResponse = userAnswersViewModel
                };

                if (question.IsMandatory == true && userAnswersViewModel.Count == 0 && !modelError)
                {
                    ModelState.AddModelError("", "All questions marked with * must be answered");
                    modelError = true;
                }

                userSurveyResponseViewModel.UserQuestionAnswersList.Add(userQuestionAnswers);
            }

            if (ModelState.IsValid)
            {
                var userSurveyResponse = _mapper.Map<UserSurveyResponse>(userSurveyResponseViewModel);

                var client = _clientFactory.CreateClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(userSurveyResponse), Encoding.UTF8, "application/json");
                string endpoint = apiBaseUrl + "/AddSurveyResponse";

                using var Response = await client.PostAsync(endpoint, content);

                if (User.Identity.IsAuthenticated)
                    return RedirectToAction("TakeSurvey");
                else
                    return View("Success");

            }
            else
            {

                return View("GetSurveyAnswer", surveyViewModel);

            }

        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> TakeSurvey()
        {
            var client = _clientFactory.CreateClient();

            string endpoint = apiBaseUrl + "/ListOpenSurveys";

            using var Response = await client.GetAsync(endpoint);
            if (Response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await Response.Content.ReadAsStringAsync();

                List<Survey> surveys = JsonConvert.DeserializeObject<List<Survey>>(content);
                List<SurveyViewModel> surveysViewModel = new List<SurveyViewModel>();

                foreach (Survey survey in surveys)
                {
                    var surveyViewModel = _mapper.Map<SurveyViewModel>(survey);
                    surveysViewModel.Add(surveyViewModel);
                }
                return View(surveysViewModel);
            }
            else
            {
                return View();
            }
        }

    }



}
