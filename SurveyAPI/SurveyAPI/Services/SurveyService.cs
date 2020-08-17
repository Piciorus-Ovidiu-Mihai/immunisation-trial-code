using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using SurveyAPI.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SurveyAPI.Services
{
    public class SurveyService : ISurveyService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork unitOfWork;

        public SurveyService(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            this.unitOfWork = unitOfWork;
        }

        public void AddSurvey(Survey survey)
        {
            _context.Attach<ApplicationUser>(survey.SurveyOwner);
            survey.GuidLink = Guid.NewGuid();
            unitOfWork.SurveyRepository.Insert(survey);
            unitOfWork.Save();
        }

        public IEnumerable<Survey> ListSurveys()
        {
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get();
            return surveys;
        }

        public IEnumerable<Survey> ListOpenSurveys()
        {
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get(survey => (survey.AvailableFromDate <= DateTime.Now && survey.ClosesOnDate >= DateTime.Now));
            return surveys;
        }

        public IEnumerable<Survey> GetByNameSurveys(string surveyName)
        {
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get(survey => survey.SurveyName == surveyName);
            return surveys;
        }

        public IEnumerable<Survey> GetByDate(string date)
        {
            DateTime date1 = DateTime.Parse(date);
            date1 = date1.Date;
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get(survey => survey.DateCreated.Date.Equals(date1));
            return surveys;
        }

        public bool IsOpenSurvey(DateTime date)
        {
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get(survey => survey.AvailableFromDate >= date && survey.ClosesOnDate <= date);
            if (surveys.ToList().Count != 0)
                return false;
            else
                return true;
        }

        public IEnumerable<Survey> AllOpenSurvey(DateTime date)
        {
            IEnumerable<Survey> surveys = unitOfWork.SurveyRepository.Get(survey => survey.AvailableFromDate >= date && survey.ClosesOnDate <= date);
            return surveys;
        }

        public Survey GetByGuid(Guid? id)
        {
            return unitOfWork.SurveyRepository.Get(survey => survey.GuidLink == id.Value, null, "QuestionsList,QuestionsList.SurveyPossibleAnswersList").FirstOrDefault();
        }

        public Survey GetByID(int? id)
        {
            return unitOfWork.SurveyRepository.Get(survey => survey.SurveyId == id.Value, null, "QuestionsList,QuestionsList.SurveyPossibleAnswersList").FirstOrDefault();
        }

        public IEnumerable<SurveyQuestion> GetQuestionsForSurveyByID(int? id)
        {
            var survey = unitOfWork.SurveyRepository.GetByID(id);
            return survey.QuestionsList;
        }

        public void Update(Survey survey)
        {
            _context.Attach<ApplicationUser>(survey.ModifiedByUser);
            unitOfWork.SurveyRepository.Update(survey);
            unitOfWork.Save();
        }

        public void Delete(int? id)
        {
            unitOfWork.SurveyRepository.Delete(GetByID(id));
            unitOfWork.Save();
        }

    }
}
