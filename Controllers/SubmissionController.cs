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
    public class SubmissionController : Controller
    {
        // GET: Submission
        public Submission GetUserSubmission(API_User _user)
        {
            Submission A = new Submission(_user.id);
            return A;
        }
        public void PostUserSubmission(API_User _user, int _question_id, int _answer_id )
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            int answers_id = Convert.ToInt32(context.answers_to_questions.Where(p => p.answer_id == _answer_id).Where(p => p.question_id == _question_id).Select(p => p.Id).FirstOrDefault());
            var k = new user_submission
            {
                user_id = _user.id,
                answer_id = answers_id
            };
            context.user_submission.InsertOnSubmit(k);
            context.user_submission.Context.SubmitChanges();
        }
    }
}