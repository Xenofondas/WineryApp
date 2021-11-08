using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This model class represent the Question entity
    public class Questions
    {
        //Primary key for questions table
        [Key]
        public int QuestionID { get; set; }
        [Required]
        //The question title
        public String Title { get; set; }
        //The list of answers for a question
        public virtual ICollection<Answers> AnswersList { get; set; }
    }
}