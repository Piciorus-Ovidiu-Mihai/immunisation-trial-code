using Microsoft.EntityFrameworkCore;
using SurveyAPI.DAL;
using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SurveyAPI.Services
{
    public class UserSurveyResponseService : IUserSurveyResponseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public UserSurveyResponseService(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }

        public void AddUserSurveyResponse(UserSurveyResponse userSurveyResponse)
        {
            _context.Attach(userSurveyResponse).State = EntityState.Added;

            unitOfWork.UserSurveyResponseRepository.Insert(userSurveyResponse);
            unitOfWork.Save();
        }

        public IEnumerable<UserSurveyResponse> ListUserSurveyResponsesForUser(string userId)
        {
            IEnumerable<UserSurveyResponse> userSurveyResponses = unitOfWork.UserSurveyResponseRepository.Get(userSurveyResponse => userSurveyResponse.UserId.Equals(userId), null, "Survey,Survey.QuestionsList,Survey.QuestionsList.SurveyPossibleAnswersList,User,UserQuestionAnswersList,UserQuestionAnswersList.UserQuestionResponse");
            return userSurveyResponses;
        }

        public IEnumerable<UserSurveyResponse> ListUserSurveyResponses()
        {
            return unitOfWork.UserSurveyResponseRepository.Get(null, null, "Survey,Survey.QuestionsList,Survey.QuestionsList.SurveyPossibleAnswersList,User,UserQuestionAnswersList,UserQuestionAnswersList.UserQuestionResponse");
        }

        public UserSurveyResponse GetByID(int? id)
        {
            var resp = 
            unitOfWork.UserSurveyResponseRepository.Get(userSurveyResponse => userSurveyResponse.UserSurveyResponseId == id, null, "Survey,Survey.QuestionsList,Survey.QuestionsList.SurveyPossibleAnswersList,User,UserQuestionAnswersList,UserQuestionAnswersList.UserQuestionResponse").FirstOrDefault();
            return resp;
        }

        public UserSurveyResponse GetByIdAndUserId(int id, string userId)
        {
            var response =  unitOfWork.UserSurveyResponseRepository.Get(userSurveyResponse => userSurveyResponse.SurveyId == id && userSurveyResponse.UserId == userId, null, "Survey,Survey.QuestionsList,Survey.QuestionsList.SurveyPossibleAnswersList,User,UserQuestionAnswersList,UserQuestionAnswersList.UserQuestionResponse").FirstOrDefault();
            return response;
        }

        public void Update(UserSurveyResponse userSurveyResponse)
        {
            unitOfWork.UserSurveyResponseRepository.Update(userSurveyResponse);
            unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            unitOfWork.UserSurveyResponseRepository.Delete(GetByID(id));
            unitOfWork.Save();
        }
    }
}
