﻿using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Management.Web.Controllers
{
    public class RequestAssetController : Controller
    {
        private readonly RequestAssetService _service;

        public RequestAssetController(ApplicationDbContext context)
        {
            _service = new RequestAssetService(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var request = _service.GetRequestAssets();
            return View(request);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Approve(long? id)
        {
            _service.ApproveRequest(id);
            return Redirect("/RequestAsset");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Reject(long? id)
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SaveRejection([Bind("Id, Reason")] StatusReq req)
        {
            if (ModelState.IsValid)
            {
                _service.SaveRejection(req);
                return Redirect("Index");
            }
            return View("Reject", req);
        }
    }
}
