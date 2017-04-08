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
    public class PoolQuestionController : Controller
    {
        // GET: PoolQuestion
        public Poll[]  GetPool()
        {
            poll_question a = new poll_question();
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            int n = context.pool_question.Count();
            int[] mass = new int[n];
            for(int i =0; i< n; i++)
            {
                mass[i] = Convert.ToInt32(context.pool_question.Select(p => p.question_id).Skip(i).Take(1).Skip(n - i));
            }
            return a.ToPoll(mass);
        }
        public Poll GetPoolForID(int _question_id)
        {
            poll_question a = new poll_question();
            Poll Res = new Poll();
            Res.id = _question_id;
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            Res.quesstion = Convert.ToString(context.pool_question.Where(p => p.question_id == _question_id).Select(p => p.text).First());
            Res.answers = a.GetAnswers(_question_id);
            return Res;
        }
    }
}