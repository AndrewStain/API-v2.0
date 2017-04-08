using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using API.Models.Linq;

namespace API.Models
{
    public struct VtoA
    {
        public Poll_entity answer;
        public int votes_total;
    }
public class Question_Stats
    {
        public Poll_entity question;
        public int answers_total;
        public VtoA[] answers_stats;
        public Question_Stats(poll_question _question)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            question = _question;
            answers_total = Convert.ToInt32(context.answer_total(question.id));
            poll_answer[] A = _question.GetAnswers(_question.id);
            int[] B = new int[A.Length];
            for (int i = 0; i < A.Length; i++)
            {
                B[i] = Convert.ToInt32(context.votes_total(A[i].id));

            }
            VtoA[] Res = new VtoA[A.Length];
            for(int i = 0; i< A.Length; i++)
            {
                Res[i].answer = A[i];
                Res[i].votes_total = B[i];
            }
            answers_stats = Res;
        }
    }
}