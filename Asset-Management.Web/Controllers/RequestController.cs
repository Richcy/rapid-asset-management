using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Management.Web.Controllers
{
    public class RequestController : Controller
    {
        private readonly RequestAssetService _service;

        public RequestController(ApplicationDbContext context)
        {
            _service = new RequestAssetService(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult AddModal()
        {
            return PartialView("_Add");
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Detail(long? id)
        {
            var requestAsset = _service.ReadRequestAsset(id);
            return View(requestAsset);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Save([Bind("PicName, PicAddress, Specification, RequestDate")] RequestAssetModel req)
        {
            if (ModelState.IsValid)
            {
                _service.CreateRequestAsset(req);
                return Redirect("Index");
            }
            return View("Add", req);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            var request = _service.GetRequests();
            return View(request);
        }
    }
}
