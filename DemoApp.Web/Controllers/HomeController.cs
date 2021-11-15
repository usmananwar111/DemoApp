using DemoApp.Data.Repository;
using DemoApp.Data.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DemoApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private UserRepository _repoUsers = new UserRepository();
        private Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {
            StatusMessage();

            List<UserViewModel> usersList = new List<UserViewModel>();

            usersList = _repoUsers.GetAllUsers();

            if (usersList != null)
                return View(usersList);

            return View();
        }

        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddUser(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repoUsers.AddUser(model))
                    {
                        TempData["Message"] = "User added successfully";

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            ViewBag.Message = "Error!";
            return View(model);
        }

        public ActionResult UpdateUser(string guid)
        {
            UserViewModel model = new UserViewModel();
            model = _repoUsers.GetUser(guid);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateUser(UserViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_repoUsers.UpdateUser(model))
                    {
                        TempData["Message"] = "User details updated successfully";

                        return RedirectToAction("Index");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Info(ex);
            }

            ViewBag.Message = "Error!";
            return View(model);
        }

        public ActionResult DeleteUser(string guid)
        {
            if (_repoUsers.DeleteUser(guid))
            {
                TempData["Message"] = "Record Deleted!";
            }
            else
            {
                TempData["Message"] = "Unable to Delete User!";
            }

            return RedirectToAction("Index");
        }

        #region Helpers

        private void StatusMessage()
        {
            if (TempData["Message"] != null && !String.IsNullOrEmpty(TempData["Message"].ToString()))
                ViewBag.Message = TempData["Message"].ToString();
        }

        #endregion
    }
}


