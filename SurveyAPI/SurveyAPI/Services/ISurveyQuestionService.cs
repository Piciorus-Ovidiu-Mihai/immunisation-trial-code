using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public interface ISurveyQuestionService
    {
        void AddSurveyQuestion(SurveyQuestion surveyQuestion);

        SurveyQuestion GetSurveyQuestionByID(int id);

        void UpdateSurveyQuestion(SurveyQuestion surveyQuestion);

        void DeleteSurveyQuestion(int id);

        IEnumerable<SurveyQuestion> GetAllQuestions();

        IEnumerable<SurveyQuestion> GetQuestionsBySurveyID(int? id);

        SurveyQuestion GetSurveyQuestionById(int id);

        int GetSurveyID(int id);
    }
}
