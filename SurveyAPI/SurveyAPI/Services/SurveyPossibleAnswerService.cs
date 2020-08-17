using SurveyAPI.DAL;
using SurveyAPI.Data;
using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public class SurveyPossibleAnswerService : ISurveyPossibleAnswerService
    {

        private readonly IUnitOfWork unitOfWork;

        public SurveyPossibleAnswerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddPossibleAnswer(SurveyPossibleAnswer possibleAnswer)
        {
            unitOfWork.SurveyPossibleAnswerRepository.Insert(possibleAnswer);
            unitOfWork.Save();
        }

        public IEnumerable<SurveyPossibleAnswer> ListPossibleAnswer()
        {
            IEnumerable<SurveyPossibleAnswer> possibleAnswers = unitOfWork.SurveyPossibleAnswerRepository.Get();
            return possibleAnswers;
        }

        public SurveyPossibleAnswer GetAnswerByID(int id)
        {
            return unitOfWork.SurveyPossibleAnswerRepository.GetByID(id);
        }

        public void UpdatePossibleAnswer(SurveyPossibleAnswer possibleAnswers)
        {
            unitOfWork.SurveyPossibleAnswerRepository.Update(possibleAnswers);
            unitOfWork.Save();
        }

        public void DeletePossibleAnswer(int id)
        {
            unitOfWork.SurveyPossibleAnswerRepository.Delete(id);
            unitOfWork.Save();
        }

        public IEnumerable<SurveyPossibleAnswer> GetAnswersByQuestionID(int id)
        {
            IEnumerable<SurveyPossibleAnswer> answers = unitOfWork.SurveyPossibleAnswerRepository.Get(answer => answer.SurveyQuestionId == id);
            return answers;
        }

        public SurveyPossibleAnswer GetAnserByID(int id)
        {
            return unitOfWork.SurveyPossibleAnswerRepository.GetByID(id);
        }

    }
}
