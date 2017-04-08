using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Poll_entity
    {
        public int id;
        public string text;

        public string ToJSon(bool _isQuestion)
        {
            return id.ToString() + text;
        }
    }
}