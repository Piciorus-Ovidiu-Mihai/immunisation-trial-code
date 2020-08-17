using SurveyClassLibrary.Models;
using System;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public interface ISurveyService
    {
        void AddSurvey(Survey survey);

        IEnumerable<Survey> ListSurveys();

        IEnumerable<Survey> ListOpenSurveys();

        IEnumerable<Survey> GetByNameSurveys(string surveyName);

        IEnumerable<Survey> GetByDate(string date);

        bool IsOpenSurvey(DateTime date);

        IEnumerable<Survey> AllOpenSurvey(DateTime date);

        Survey GetByGuid(Guid? id);

        Survey GetByID(int? id);

        IEnumerable<SurveyQuestion> GetQuestionsForSurveyByID(int? id);

        void Update(Survey survey);

        void Delete(int? id);
    }
}
