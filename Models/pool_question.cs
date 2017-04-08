using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using API.Models.Linq;

namespace API.Models
{
    public class poll_question : Poll_entity
    {
        public string tableName()
        {
            return "poll_questions";
        }
        public Poll[] ToPoll(int[] question_ids)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            Poll[] A = new Poll[question_ids.Length];
            for (int i = 0; i < question_ids.Length; i++)
            {
                A[i].id = Convert.ToInt32(context.pool_question.Where(p => p.question_id == question_ids[i]).Select(p => p.question_id).Skip(i).Take(1).Skip(question_ids.Length - i));
                A[i].quesstion = Convert.ToString(context.pool_question.Where(p => p.question_id == question_ids[i]).Select(p => p.text).Skip(i).Take(1).Skip(question_ids.Length - i));
                A[i].answers = GetAnswers(question_ids[i]);
            }
            return A;
        }
        public poll_answer[] GetAnswers(int question_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            int[] a = GetAnswersLinks(question_id);
            poll_answer[] A = new poll_answer[a.Length];
            for(int i = 0; i<a.Length; i++)
            { 
                A[i].id = Convert.ToInt32(context.pool_answer.Where(p => p.answer_id == a[i]).Select(p => p.answer_id).Skip(i).Take(1).Skip(a.Length-i));
                A[i].text = Convert.ToString(context.pool_answer.Where(p => p.answer_id == a[i]).Select(p => p.text).Skip(i).Take(1).Skip(a.Length - i));
            }
            return A;
        }
        public int[] GetAnswersLinks(int question_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            int n = Convert.ToInt32(context.answers_to_questions.Where(p => p.question_id == question_id).Count());
            int[] a = new int[n];
            
            for (int i = 0; i<n; i++)
            {
                a[i] = Convert.ToInt32(context.answers_to_questions.Where(p=>p.question_id ==question_id).Select(p => p.answer_id).Skip(i).Take(1).Skip(n - i-1));
            }
            return a;
        }
    }
}