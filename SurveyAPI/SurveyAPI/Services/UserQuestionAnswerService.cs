using SurveyAPI.DAL;
using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public class UserQuestionAnswerService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserQuestionAnswerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddUserSurveyResponse(UserQuestionAnswer userQuestionAnswer)
        {
            unitOfWork.UserQuestionAnswerRepository.Insert(userQuestionAnswer);
            unitOfWork.Save();
        }

        public IEnumerable<UserQuestionAnswer> ListUserQuestionAnswer()
        {
            IEnumerable<UserQuestionAnswer> userQuestionAnswers = unitOfWork.UserQuestionAnswerRepository.Get();
            return userQuestionAnswers;
        }

        public UserQuestionAnswer GetByID(int? id)
        {
            return unitOfWork.UserQuestionAnswerRepository.GetByID(id);
        }

        public IEnumerable<UserQuestionAnswer> GetUserQuestionAnswersBySurveyResponseId(int surveyResponseID)
        {
            IEnumerable<UserQuestionAnswer> userQuestionsAnswer = unitOfWork.UserQuestionAnswerRepository.Get(userQuestionsAnswer => userQuestionsAnswer.UserSurveyResponseId == surveyResponseID);
            return userQuestionsAnswer;
        }


        public void Update(UserQuestionAnswer userQuestionAnswers)
        {
            unitOfWork.UserQuestionAnswerRepository.Update(userQuestionAnswers);
            unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            unitOfWork.UserQuestionAnswerRepository.Delete(GetByID(id));
            unitOfWork.Save();
        }
    }
}
