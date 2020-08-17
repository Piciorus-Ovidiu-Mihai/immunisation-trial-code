using SurveyAPI.DAL;
using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public class UserAnswersService
    {
        private readonly IUnitOfWork unitOfWork;

        public UserAnswersService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddUserAnswer(UserAnswers userAnswer)
        {
            unitOfWork.UserAnswersRepository.Insert(userAnswer);
            unitOfWork.Save();
        }

        public IEnumerable<UserAnswers> ListUserQuestionAnswer()
        {
            IEnumerable<UserAnswers> userAnswer = unitOfWork.UserAnswersRepository.Get();
            return userAnswer;
        }

        public UserAnswers GetByID(int? id)
        {
            return unitOfWork.UserAnswersRepository.GetByID(id);
        }

        public IEnumerable<UserAnswers> GetUserAnswerByUserQuestionAnswerId(int userQuestionResponseID)
        {
            IEnumerable<UserAnswers> userAnswers = unitOfWork.UserAnswersRepository.Get(userAnswers => userAnswers.UserQuestionAnswerId == userQuestionResponseID);
            //return unitOfWork.UserAnswersRepository.Get(userAnswer => userAnswer == id.Value, null, "QuestionsList,QuestionsList.SurveyPossibleAnswersList").FirstOrDefault();

            return userAnswers;
        }

        public void Update(UserAnswers userAnswer)
        {
            unitOfWork.UserAnswersRepository.Update(userAnswer);
            unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            unitOfWork.UserAnswersRepository.Delete(GetByID(id));
            unitOfWork.Save();
        }
    }
}
