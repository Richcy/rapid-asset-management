using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asset_Management.Web.Controllers
{
    public class AssetHistoryController : Controller
    {
        private readonly AssetHistoryService _assetHistoryService;
        private readonly AssetService _assetService;
        private readonly PicService _picService;

        public AssetHistoryController(ApplicationDbContext context)
        {
            _assetHistoryService = new AssetHistoryService(context);
            _assetService = new AssetService(context);
            _picService = new PicService(context);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var data = _assetHistoryService.GetAssetHistory();
            return View(data);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Asset = new SelectList(_assetService.GetAssets(), "Id", "AssetNameWithSnSpec ");
            ViewBag.Pic = new SelectList(_picService.GetPics(), "Id", "FullName");
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult AddModal()
        {
            ViewBag.Asset = new SelectList(_assetService.GetAssets(), "Id", "AssetNameWithSnSpec");
            ViewBag.Pic = new SelectList(_picService.GetPics(), "Id", "FullName");
            return PartialView("_Add");
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Save([Bind("AssetId, RequestId, Location, PicId, SendDate")] AssetHistoryReq req)
        {
            if (ModelState.IsValid)
            {
                _assetHistoryService.CreateAssetHistory(req);
                return Redirect("Index");
            }
            return View("Add", req);
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Edit(long? id)
        {
            var assetHistory = _assetHistoryService.ReadAssetHistory(id);
            return View(assetHistory);
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult Update([Bind("Id, AssetId, Location, PicId, SendDate, ReturnDate")] AssetHistoryReq req)
        {
            if (ModelState.IsValid)
            {
                _assetHistoryService.UpdateAssetHistory(req);
                return Redirect("Index");
            }
            return View("Edit", req);
        }


    }
}
