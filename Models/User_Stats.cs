using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using API.Models.Linq;

namespace API.Models
{
    public struct QtoA
    {
        public Poll_entity question;
        public Poll_entity answer;
    }
    public class User_Stats
    {
        private API_User user;
        public int user_id;
        public QtoA[] answers;
        public User_Stats(API_User User)
        {
            user_id = User.id;
            user = User;
            answers = CollectStats(user_id);
        }
        public QtoA[] CollectStats(int user_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            int n = Convert.ToInt32(context.user_submission.Where(p => p.user_id == user_id).Count());
            QtoA[] Res = new QtoA[n];
            int[] mass = new int[n];
            for(int i = 0; i<n; i++)
            {
                mass[i] = Convert.ToInt32(context.user_submission.Where(p => p.user_id == user_id).Select(p => p.answer_id).Skip(i).Take(1).Skip(n - i));
            }
            for(int i = 0; i<n; i++)
            {
                int answer_id = Convert.ToInt32(context.answers_to_questions.Where(p => p.Id == mass[i]).Select(p => p.answer_id).Skip(i).Take(1).Skip(n - i));
                int question_id = Convert.ToInt32(context.answers_to_questions.Where(p => p.Id == mass[i]).Select(p => p.question_id).Skip(i).Take(1).Skip(n - i));
                Res[i].answer.id = answer_id;
                Res[i].answer.text = Convert.ToString(context.pool_answer.Where(p => p.answer_id == answer_id).Select(p => p.text));
                Res[i].question.id = question_id;
                Res[i].question.text = Convert.ToString(context.pool_question.Where(p => p.question_id == question_id).Select(p => p.text));
            }
            return Res;
        }

    }
}