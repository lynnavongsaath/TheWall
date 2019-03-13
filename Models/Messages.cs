using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TheWall.Models
{   
    public class Messages
    {
        [Key]
        public int MessageId {get;set;}
        public int UserId {get;set;}
        [Required(ErrorMessage="Please provide a message")]
        [MinLength(2, ErrorMessage="Message must be at least 2 characters long.")]
        public string Message {get;set;}
        // [DisplayFormat(DataFormatString = "{0:yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        //navigation property
        public Users Creator {get;set;} 
        public List<Comments> Comments {get;set;}
    }
    public class Comments
    {
        [Key]
        public int CommentId {get;set;}
        public int MessageId {get;set;}
        public int UserId {get;set;}
        [Required(ErrorMessage="Please provide a comment")]
        [MinLength(2, ErrorMessage="Comment must be at least 2 characters long.")]
        public string Comment {get;set;}
        // [DisplayFormat(DataFormatString = "{0:MM/D/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
        //navigation property
        public Users Creator {get;set;}
        public Messages Message {get;set;}
    }
}