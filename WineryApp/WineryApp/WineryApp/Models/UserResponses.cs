using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineryApp.Models
{
    //This model class represent users Reponse Entity
    //wich is a relation between users,questions,answers
    public class UserResponses
    {
        public int Id { get; set; }        
        //The users id
        public int User_Id { get; set; }        
        //The question title
        public int Question_Id { get; set; }
        //The answer given
        public int Answer_Id { get; set; }
    }
}
