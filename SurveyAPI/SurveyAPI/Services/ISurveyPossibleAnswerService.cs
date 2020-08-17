using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public interface ISurveyPossibleAnswerService
    {
        void AddPossibleAnswer(SurveyPossibleAnswer possibleAnswer);

        IEnumerable<SurveyPossibleAnswer> ListPossibleAnswer();

        SurveyPossibleAnswer GetAnswerByID(int id);

        void UpdatePossibleAnswer(SurveyPossibleAnswer possibleAnswers);

        void DeletePossibleAnswer(int id);

        IEnumerable<SurveyPossibleAnswer> GetAnswersByQuestionID(int id);

        SurveyPossibleAnswer GetAnserByID(int id);
    }
}
