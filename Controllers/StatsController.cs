using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using API.Models;
using API.Models.Linq;
using System.Configuration;

namespace API.Controllers
{
    public class StatsController : Controller
    {
        // GET: QuestionStats
        public User_Stats GetUserStats(API_User _User)
        {
            User_Stats Res = new User_Stats(_User);
            Res.user_id = _User.id;
            Res.answers = Res.CollectStats(Res.user_id);
            return Res;
        }
        public Question_Stats GetQuestionStart(int _question_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            poll_question A = new poll_question();
            A.id = _question_id;
            A.text = Convert.ToString(context.pool_question.Where(p => p.question_id == _question_id).Select(p => p.text).FirstOrDefault());
            Question_Stats Res = new Question_Stats(A);
            return Res;
        }
    }
}