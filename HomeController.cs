using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialMediaApplication.Data;
using SocialMediaApplication.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaApplication.Controllers
{
    public class HomeController : Controller
    {

        public int counter = 0;
        private readonly SMDBContext _context;
        public int currentUserID = -1;

        public HomeController(SMDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewModel feed = new ViewModel();
            if (TempData.ContainsKey("vmUserTemp"))
            {
                int currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                ViewBag.UserId = currentUserID;
                Array Users = _context.Users.ToArray();
                foreach (User User in Users)
                {
                    if (User.UserId == currentUserID)
                    {
                        feed.CurrentUser = User;
                    }
                }
            }
            if (feed.CurrentUser == null)
            {
                feed.CurrentUser = new User();
                ViewBag.UserId = -1;
            }
            feed.Posts = _context.Posts.ToArray();
            feed.Users = _context.Users.ToArray();
            TempData["vmUserTemp"] = feed.CurrentUser.UserId;
            return View(feed);
        }


        public IActionResult Users()
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
            }
            ViewModel feed = new ViewModel();
            feed.Posts = _context.Posts.ToArray();
            feed.Users = _context.Users.ToArray();
            TempData["vmUserTemp"] = currentUserID;
            return View(feed);
        }
        public IActionResult Register()
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
            }
            User user = new User();
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Login));
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user, ViewModel vmUser)
        {
            bool userFound = false;
            Array Users = _context.Users.ToArray();
            foreach (User User in Users)
            {
                if (User.UserName == user.UserName)
                {
                    if (User.Password == user.Password)
                    {
                        ViewBag.Errors = "";
                        vmUser.CurrentUser = User;
                        userFound = true;
                        break;
                    }
                    else
                    {
                        ViewBag.Errors = "Username or Password is incorrect.";
                    }

                }
                else
                {
                    vmUser.CurrentUser = new User();
                    ViewBag.Errors = "Username or Password is incorrect.";
                }
            }

            //if(user is found)
            if (userFound)
            {
                TempData["vmUserTemp"] = vmUser.CurrentUser.UserId;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(ViewBag.Error);
            }
            //else return to login page with errors.
        }
        public IActionResult Privacy()
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
            }
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpGet]
        public IActionResult EditPost(Post post)
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
                ViewBag.UserId = currentUserID;
            }
            return View(post);
        }
        [HttpPost]
        public IActionResult EditPost(Post post, int? num)
        {
            _context.Update(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [HttpDelete]
        public IActionResult DeletePost(Post post, int? num)
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
            }
            _context.Remove(post);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Logout()
        {
            currentUserID = -1;
            TempData["vmUserTemp"] = currentUserID;
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult CreatePost(int? id)
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
                ViewBag.UserId = currentUserID;
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Post model)
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
                ViewBag.UserId = currentUserID;
            }

            if (ModelState.IsValid)
            {
                Post post = new Post();
                post.PostBody = model.PostBody;
                post.UserId = model.UserId;
                post.NumberOfLikes = 0;
                post.NumberOfComments = 0;

                _context.Add(post);
                await _context.SaveChangesAsync();

                
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> LikePost(Post postLiked)
        {
            if (TempData.ContainsKey("vmUserTemp"))
            {
                currentUserID = int.Parse(TempData["vmUserTemp"].ToString());
                TempData["vmUserTemp"] = currentUserID;
                ViewBag.UserId = currentUserID;
            }
            Array posts = _context.Posts.ToArray();
            foreach(Post post in posts)
            {
                if (post.PostId == postLiked.PostId)
                {
                    post.NumberOfLikes++;
                    postLiked = post;
                }
            }
            _context.Update(postLiked);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
