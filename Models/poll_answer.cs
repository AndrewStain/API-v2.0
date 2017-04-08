using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using API.Models.Linq;

namespace API.Models
{
    public class  poll_answer : Poll_entity
    {
        public string tableName()
        {
            return "pool_answers";
        }
        public int GetAnswerRelation(int answer_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            return context.answers_to_questions.Where(p => p.answer_id == answer_id).Count();
        }
        public int GetVotesCount(int answer_id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            return Convert.ToInt32(context.votes_total(answer_id));
        }
    }
}