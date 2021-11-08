using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineryApp.Models;

namespace WineryApp.Models
{
    //This model class represent the Question entity
    public class Question
    {
        //The question title
        public String Title { get; set; }
        //The list of answers for a question
        public virtual  List<Answers> AnswersList { get; set; }
    }
}
