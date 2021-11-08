using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This model call respresents an anwer entity
    public class Answers
    {
        //The primary key for the answers table
        [Key]
        public int AnswerId { get; set; }
        [Required]
        //The answer title
        public String AnswerText { get; set; }
        //Declare a foreign key creating relation with the question table
        [ForeignKey("QuestionId")]
        public Questions Question { get; set; }
        public int QuestionId { get; set; } 

    }
}