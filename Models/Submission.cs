using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using API.Models.Linq;

namespace API.Models
{
    public struct AtoQS
    {
        public int answer_id;
        public int question_id;
    }
    public class Submission
    {
        public int user_id;
        public AtoQS[] answers;
        public Submission(int _user_id)
        {
            user_id = _user_id;
        }
        public Submission fetch(int user_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            Submission Res = new Submission(user_id);
            if (context.user_submission.Where(p => p.user_id == user_id).Any())
            {
                int n = Convert.ToInt32(context.user_submission.Where(p => p.user_id == user_id).Count());
                int[] mass = new int[n];
                for (int i = 0; i < n; i++)
                {
                    mass[i] = Convert.ToInt32(context.user_submission.Where(p => p.user_id == user_id).Select(p => p.answer_id).Skip(i).Take(1).Skip(n - i));
                    Res.answers[i].answer_id = Convert.ToInt32(context.answers_to_questions.Where(p => p.Id == mass[i]).Select(p => p.answer_id).Skip(i).Take(1).Skip(n - i));
                    Res.answers[i].question_id = Convert.ToInt32(context.answers_to_questions.Where(p => p.Id == mass[i]).Select(p => p.question_id).Skip(i).Take(1).Skip(n - i));
                }
                return Res;
            }
            else throw new Exception();
        }
        public void save(Submission sub)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);

            if (sub.user_id.ToString() == null)
                throw new Exception();
            else
                if (!sub.answers.Any())
                throw new Exception();
            else
                if (!context.user_submission.Where(p => p.user_id == user_id).Any())
                throw new Exception();
        }
    }
}