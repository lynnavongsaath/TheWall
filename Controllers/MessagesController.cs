using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TheWall.Models;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Controllers
{
    public class MessagesController : Controller
    {
        private wallContext dbContext;
        public MessagesController(wallContext context)
        {
            dbContext = context;
        }
        private Users loggedInUser {
            get { return dbContext.Users.FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")); }
        }
        [HttpGet("Show")]
        public IActionResult Show()
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");
            
            ViewBag.CurrUser = loggedInUser;

            var AllMessages = dbContext.Messages
                .Include(msg => msg.Creator)
                .Include(msg => msg.Comments)
                    .ThenInclude(comment => comment.Creator)
                .OrderByDescending(a => a.CreatedAt)
                .ToList();
                
            return View(AllMessages);
        }
        [HttpPost("PostMessage")]
        public IActionResult PostMessage(Messages newMessage)
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");
            
            if(ModelState.IsValid)
            {
                newMessage.UserId = loggedInUser.UserId;

                dbContext.Messages.Add(newMessage);
                dbContext.SaveChanges();
                return RedirectToAction("Show");
            }   
            return RedirectToAction("Show");
        }
        [HttpPost("PostComment")]
        public IActionResult PostComment(Comments newComment)
        {
            if(loggedInUser == null)
                return RedirectToAction("Index", "Home");
            
            if(ModelState.IsValid)
            {
                newComment.UserId = loggedInUser.UserId;

                dbContext.Comments.Add(newComment);
                dbContext.SaveChanges();
                return RedirectToAction("Show");
            }   
            return RedirectToAction("Show");
        }
    }
}