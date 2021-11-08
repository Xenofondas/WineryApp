using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This model class represent users Reponse Entity
    //wich is a relation between users,questions,answers
    public class UserResponses
    {
        //Primary key for UsersResponses Table
        public int Id { get; set; }

        //Foreign key for the users table
        [ForeignKey("User_Id")]
        public User user { get; set; }
        public int User_Id { get; set; }

        //Foreign key for the questions table
        [ForeignKey("Question_Id")]
        public virtual Questions Question { get; set; }
        public int Question_Id { get; set; }

        //Foreign key for the Answers table
        [ForeignKey("Answer_Id")]
        public virtual Answers Answer { get; set; }
        public int Answer_Id { get; set; }
    }
}