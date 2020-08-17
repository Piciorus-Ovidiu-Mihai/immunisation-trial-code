using Microsoft.AspNetCore.Mvc;
using SurveyAPI.Data;
using SurveyAPI.Services;
using SurveyClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace SurveyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyApiController : ControllerBase
    {

        private readonly ISurveyService _surveyService;
        private readonly ISurveyQuestionService _surveyQuestionService;
        private readonly ISurveyPossibleAnswerService _surveyPossibleAnswerService;
        private readonly IUserSurveyResponseService _userSurveyResponseService;

        public SurveyApiController(
            ISurveyService surveyService,
            ISurveyQuestionService surveyQuestionService,
            ISurveyPossibleAnswerService surveyPossibleAnswerService,
            IUserSurveyResponseService userSurveyResponseService)
        {
            _surveyService = surveyService;
            _surveyQuestionService = surveyQuestionService;
            _surveyPossibleAnswerService = surveyPossibleAnswerService;
            _userSurveyResponseService = userSurveyResponseService;
        }

        [HttpPost("CreateSurvey")]
        public void CreateSurvey(Survey survey)
        {
            survey.QuestionsList = new List<SurveyQuestion>();
            _surveyService.AddSurvey(survey);
        }

        [HttpPost("AddEmptyQuestion")]
        public void AddEmptyQuestion(int id, string userId)
        {
            var survey = GetByID(id);

            if (survey.QuestionsList == null)
            {
                survey.QuestionsList = new List<SurveyQuestion>();
            }

            survey.QuestionsList.Add(new SurveyQuestion { Type = "textbox" });
            survey.ModifiedByUser = new ApplicationUser { Id = userId };

            _surveyService.Update(survey);
        }

        [HttpPost("AddNewAnswer")]
        public void AddEmptyAnswer(int id)
        {
            var question = GetQuestionById(id);

            if (question.SurveyPossibleAnswersList == null)
                question.SurveyPossibleAnswersList = new List<SurveyPossibleAnswer>();

            if (question.Type.CompareTo("textbox") == 0)
                question.SurveyPossibleAnswersList.Add(new SurveyPossibleAnswer { SurveyPossibleAnswerText = "Your answer here" });
            else
                question.SurveyPossibleAnswersList.Add(new SurveyPossibleAnswer());

            _surveyQuestionService.UpdateSurveyQuestion(question);

        }

        [HttpGet("ListSurveys")]
        public IEnumerable<Survey> ListSurveys()
        {
            return _surveyService.ListSurveys();
        }

        [HttpGet("ListOpenSurveys")]
        public IEnumerable<Survey> ListOpenSurveys()
        {
            return _surveyService.ListOpenSurveys();
        }

        [HttpGet("ListQuestions")]
        public IEnumerable<SurveyQuestion> ListQuestions()
        {
            return _surveyQuestionService.GetAllQuestions();
        }

        [HttpGet("GetByID")]
        public Survey GetByID(int? id)
        {
            return _surveyService.GetByID(id);
        }

        [HttpGet("GetByGuid")]
        public Survey GetByGuid(Guid? id)
        {
            return _surveyService.GetByGuid(id);
        }

        [HttpDelete("DeleteSurvey")]
        public void DeleteSurvey(int id)
        {
            var survey = GetByID(id);
            List<int> questionIDs = new List<int>();
            List<int> answerIDs = new List<int>();
            foreach (var question in survey.QuestionsList)
            {
                foreach (var answer in question.SurveyPossibleAnswersList)
                {
                    answerIDs.Add(answer.SurveyPossibleAnswerId);
                }
                questionIDs.Add(question.SurveyQuestionId);

            }

            foreach (var answerID in answerIDs)
            {
                _surveyPossibleAnswerService.DeletePossibleAnswer(answerID);
            }

            foreach (var questionID in questionIDs)
            {
                _surveyQuestionService.DeleteSurveyQuestion(questionID);
            }

            _surveyService.Delete(id);
        }

        [HttpDelete("DeleteQuestion")]
        public void DeleteQuestion(int id)
        {
            var question = _surveyQuestionService.GetSurveyQuestionByID(id);
            var answers = _surveyPossibleAnswerService.GetAnswersByQuestionID(question.SurveyQuestionId);
            question.SurveyPossibleAnswersList = answers.ToList();

            List<int> answerIDs = new List<int>();

            if (question.SurveyPossibleAnswersList != null)
            {
                foreach (var answer in question.SurveyPossibleAnswersList)
                    answerIDs.Add(answer.SurveyPossibleAnswerId);

                foreach (var answerID in answerIDs)
                    _surveyPossibleAnswerService.DeletePossibleAnswer(answerID);
            }

            _surveyQuestionService.DeleteSurveyQuestion(id);
        }

        [HttpDelete("DeleteAnswer")]
        public void DeleteAnswer(int id)
        {
            _surveyPossibleAnswerService.DeletePossibleAnswer(id);
        }

        [HttpPost("UpdateSurvey")]
        public void UpdateSurvey(Survey survey)
        {
            _surveyService.Update(survey);
        }

        [HttpGet("GetSurveyIdByGuid")]
        public int GetSurveyIdByGuid(Guid guid)
        {
            return _surveyService.GetByGuid(guid).SurveyId;
        }

        [HttpPost("UpdateQuestion")]
        public void UpdateQuestion(SurveyQuestion surveyQuestion)
        {
            _surveyQuestionService.UpdateSurveyQuestion(surveyQuestion);
            foreach (var answer in surveyQuestion.SurveyPossibleAnswersList)
            {
                _surveyPossibleAnswerService.UpdatePossibleAnswer(answer);
            }
        }

        [HttpGet("GetAnswersForQuestion")]
        public IEnumerable<SurveyPossibleAnswer> GetAnswersForQuestion(int id)
        {
            return _surveyPossibleAnswerService.GetAnswersByQuestionID(id);
        }

        [HttpGet("GetSurveyIdForQuestion")]
        public int GetSurveyIdForQuestion(int id)
        {
            return _surveyQuestionService.GetSurveyQuestionByID(id).SurveyId;
        }

        [HttpGet("GetQuestionIdForAnswer")]
        public int GetQuestionIdForAnswer(int id)
        {
            return _surveyPossibleAnswerService.GetAnswerByID(id).SurveyQuestionId;
        }

        [HttpGet("GetQuestionById")]
        public SurveyQuestion GetQuestionById(int id)
        {
            return _surveyQuestionService.GetSurveyQuestionByID(id);
        }

        [HttpGet("GetSurveyResponseForSurveyUser")]
        public UserSurveyResponse GetSurveyResponseForSurveyUser(int idSurvey, string userId)
        {
            return _userSurveyResponseService.GetByIdAndUserId(idSurvey, userId); 
        }

        [HttpPost("AddSurveyResponse")]
        public void AddSurveyResponse(UserSurveyResponse userSurveyResponse)
        {
            _userSurveyResponseService.AddUserSurveyResponse(userSurveyResponse);
        }

        [HttpGet("GetSurveyResponseById")]
        public UserSurveyResponse GetSurveyResponseById(int id)
        {
            return _userSurveyResponseService.GetByID(id);
        }

        [HttpGet("ListResponsesForUser")]
        public IEnumerable<UserSurveyResponse> ListResponsesForUser(string userId)
        {
            return _userSurveyResponseService.ListUserSurveyResponsesForUser(userId);
        }

        [HttpGet("ListUserResponses")]
        public IEnumerable<UserSurveyResponse> ListUserResponses()
        {
            return _userSurveyResponseService.ListUserSurveyResponses();
        }

        [HttpGet("ExistResponse")]
        public bool ExistResponse(int idSurvey, string idUser)
        {
            var userResponses = _userSurveyResponseService.ListUserSurveyResponsesForUser(idUser);
            
            foreach(var userResponse in userResponses)
            {
                if (userResponse.SurveyId == idSurvey)
                    return true;
            }
            return false;

        }

    }
}
