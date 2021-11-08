using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineryApp.Models
{
    //This model class describes the facebook profile of a user
    public class FacebookUserProfile
    {
        public int Id { get; set; }
        //The users facebook id
        public Int64 FbId { get; set; }
        //The users Facebook first name
        public String FirstName { get; set; }
        //The users Facebook last name
        public String LastName { get; set; }
        //The users email
        public String UserEmail { get; set; }
        //The users gender
        public String Gender { get; set; }
    }
}
