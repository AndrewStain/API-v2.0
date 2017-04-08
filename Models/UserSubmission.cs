using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    
  public struct AtoQ
    {
        public int question_id;
        public int answer_id;
    }
    public class UserSubmission
    {
        public int user_id;
        public AtoQ[] answers;
        public string TableName()
        {
            return "user_submission";
        }
    }
    
}