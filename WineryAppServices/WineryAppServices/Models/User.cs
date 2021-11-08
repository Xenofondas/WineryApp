using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WineryAppServices.Models
{
    //This model class represent the User entity
    public class User
    {
        //Primary key for users Table
        public int Id { get; set; }
        [Required]
        //Users facebook id
        public Int64 FbId { get; set; }
        //Users facebook First name
        public String FirstName { get; set; }
        //Users facebook Last name
        public String LastName { get; set; }
        //Users email
        public String UserEmail { get; set; }
        //Users gender
        public String Gender { get; set; }
    }
}