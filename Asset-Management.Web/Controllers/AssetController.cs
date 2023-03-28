using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Management.Web.Controllers
{
    public class AssetController : Controller
    {
        private readonly AssetService _service;

        public AssetController(ApplicationDbContext context)
        {
            _service = new AssetService(context);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Index()
        {
            var assets = _service.GetAssets();
            return View(assets);
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
        [HttpPost]
        public IActionResult Save([Bind("AssetName, Specification, SerialNumber, PurchaseYear, Used")] AssetModel req)
        {
            if (ModelState.IsValid)
            {
                _service.CreateAsset(new AssetEntity(req));
                return Redirect("Index");
            }
            return View("Add", req);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult History(long? id)
        {
            var asset = _service.ReadHistoryAssetById(id);
            return View(asset);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Edit(long? id)
        {
            var asset = _service.ReadAsset(id);
            return View(asset);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Update([Bind("Id, AssetName, Specification, SerialNumber, PurchaseYear, Used")] AssetModel req)
        {
            if (ModelState.IsValid)
            {
                _service.UpdateAsset(req);
                return Redirect("Index");
            }
            return View("Edit", req);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            _service.DeleteAsset(id);
            return Redirect("/Asset");
        }

    }
}
