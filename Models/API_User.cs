using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using API.Models.Linq;
using System.Configuration;

namespace API.Models
{
    public class API_User
    {
        public int id;
        public string name;
        public string email;
        public string acces_token;
        public int GetNumSubmission(int id)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            return context.user_submission.Where(p => p.user_id == id).Count();
        }
        public void GenerateAccesToken()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            acces_token = new string(Enumerable.Repeat(chars, 40 )
      .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public bool ValidatePassword(string password_hash)
        {
            var context = new API_DBDataContext(ConfigurationManager.ConnectionStrings["Connections string"].ConnectionString);
            var User = context.User.Where(p => p.Id == id).FirstOrDefault();
            return String.Equals(User.password_hash, password_hash);
        }
    }
}