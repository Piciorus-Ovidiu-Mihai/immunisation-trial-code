using SurveyAPI.DAL;
using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System.Collections.Generic;
using System.Linq;

namespace SurveyAPI.Services
{
    public class SurveyQuestionService : ISurveyQuestionService
    {
        private readonly IUnitOfWork unitOfWork;

        public SurveyQuestionService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            unitOfWork.SurveyQuestionRepository.Insert(surveyQuestion);
            unitOfWork.Save();
        }

        public SurveyQuestion GetSurveyQuestionByID(int id)
        {
            return unitOfWork.SurveyQuestionRepository.Get(question => question.SurveyQuestionId == id, null, "SurveyPossibleAnswersList").FirstOrDefault();
        }

        public void UpdateSurveyQuestion(SurveyQuestion surveyQuestion)
        {
            unitOfWork.SurveyQuestionRepository.Update(surveyQuestion);
            unitOfWork.Save();
        }

        public void DeleteSurveyQuestion(int id)
        {
            unitOfWork.SurveyQuestionRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<SurveyQuestion> GetAllQuestions()
        {
            var surveyQuestions = unitOfWork.SurveyQuestionRepository.Get();
            return surveyQuestions;
        }

        public IEnumerable<SurveyQuestion> GetQuestionsBySurveyID(int? id)
        {
            IEnumerable<SurveyQuestion> questions = unitOfWork.SurveyQuestionRepository.Get(question => question.SurveyId == id);
            return questions;
        }

        public SurveyQuestion GetSurveyQuestionById(int id)
        {
            var surveyQuestion = unitOfWork.SurveyQuestionRepository.GetByID(id);
            return surveyQuestion;
        }

        public int GetSurveyID(int id)
        {
            var question = unitOfWork.SurveyQuestionRepository.GetByID(id);
            return question.SurveyId;
        }

    }
}
