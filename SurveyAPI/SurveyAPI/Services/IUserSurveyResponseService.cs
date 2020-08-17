using SurveyClassLibrary.Models;
using System.Collections.Generic;

namespace SurveyAPI.Services
{
    public interface IUserSurveyResponseService
    {
        void AddUserSurveyResponse(UserSurveyResponse userSurveyResponse);

        IEnumerable<UserSurveyResponse> ListUserSurveyResponsesForUser(string userId);

        IEnumerable<UserSurveyResponse> ListUserSurveyResponses();

        UserSurveyResponse GetByID(int? id);

        UserSurveyResponse GetByIdAndUserId(int id, string userId);

        void Update(UserSurveyResponse userSurveyResponse);

        void Delete(int? id);
    }
}
