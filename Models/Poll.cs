using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Poll
    {
        public int id;
        public string quesstion;
        public Poll_entity[] answers;
    }
}