using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.Models;

namespace WineryApp.Models
{
    //This model call respresents an anwer entity
    public class Answers
    {       
        public int AnswerId { get; set; }

        ////The answer title
        public string AnswerText { get; set; }

        ////Declare the relation with a question
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
