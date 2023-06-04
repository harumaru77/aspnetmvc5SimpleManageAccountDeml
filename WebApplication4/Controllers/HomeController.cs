using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        AccountManager _accountManager;

        public HomeController()
        {
            _accountManager = new AccountManager();
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            // 요청한 사용자의 인증 여부를 확인한다.
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Profile");

            return View();
        }

        [Authorize(Roles = "Admin, Customer")]
        public ActionResult Profile()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Contact()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(UserInfo userInfo)
        {
            string message;
            UserInfo findUserInfo;

            findUserInfo = _accountManager.Login(userInfo.UserName, userInfo.UserPassword);

            if (findUserInfo == null)
            {
                message = "ID 또는 Password가 일치하지 않습니다.";
            }
            else
            {
                // 인증된 사용자의 폼인증 쿠키를 만든다.
                FormsAuthentication.SetAuthCookie(userInfo.UserName, false);
                return RedirectToAction("Profile");
            }

            ViewBag.Message = message;
            return View(userInfo);
        }

        public ActionResult Logout()
        {
            // 사용자의 폼인증 쿠키를 삭제하여 로그아웃 처리한다.
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}