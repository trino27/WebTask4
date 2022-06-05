using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuissnesLayer;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer;
using PresentationLayer.Models;
using static DataLayer.Enums.DataEnums;

namespace Task4_web.Controllers
{
    public class UserController : Controller
    {
        private DataManager _datamanager;
        private ServicesManager _servicesmanager;

        public UserController(DataManager dataManager)
        {
            _datamanager = dataManager;
            _servicesmanager = new ServicesManager(dataManager);
        }
        public IActionResult Index(int pageId, UserType pageType)
        {
            UserViewModel _viewModel;
            switch (pageType)
            {
                case UserType.User: _viewModel = _servicesmanager.Users.UserDBToViewModelById(pageId); break;
                default: _viewModel = null; break;
            }
            ViewBag.UserType = pageType;
            return View(_viewModel);
        }

        [HttpGet]
        public IActionResult UserEditor(int pageId, UserType pageType, int userId = 0)
        {
            UserEditModel _editModel;

            switch (pageType)
            {
                case UserType.User:
                    if (pageId != 0) _editModel = _servicesmanager.Users.GetUserEditModel(pageId);
                    else _editModel = _servicesmanager.Users.CreateNewUserEditModel();
                    break;
                default: _editModel = null; break;
            }

            ViewBag.UserType = pageType;
            return View(_editModel);
        }

        [HttpPost]
        public IActionResult SaveUser(UserEditModel model)
        {
            _servicesmanager.Users.SaveUserEditModelToDb(model);
            return RedirectToAction("UserEditor", "User", new { pageId = model.Id, pageType=UserType.User });
        }
    }
}