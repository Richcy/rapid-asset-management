using Asset_Management.Repository;
using Asset_Management.Service;
using Asset_Management.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asset_Management.Web.Controllers
{
    [AllowAnonymous]
    public class ReturnAssetController : Controller
    {
        private readonly AssetService _assetService;
        private readonly ReturnAssetService _returnAssetService;

        public ReturnAssetController(ApplicationDbContext context)
        {
            _assetService = new AssetService(context);
            _returnAssetService = new ReturnAssetService(context);
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.Asset = new SelectList(_assetService.GetUsedAsset(), "Id", "AssetNameWithSnSpec");
            return View();
        }
        /*
                [HttpPost]
                public IActionResult Update([Bind("Id, AssetId, Location, PicId, SendDate, ReturnDate")] AssetHistoryReq req)
                {
                    if (ModelState.IsValid)
                    {
                        var returnAsset = _returnAssetService.ReadAssetHistory(req);
                        var entity = new AssetHistoryEntity();
                        _returnAssetService.UpdateAssetHistory(req);
                        return Redirect("Index");
                    }
                    return View("Edit", req);
                }*/

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Update(AssetHistoryReq req)
        {

            var entity = _returnAssetService.ReadAssetHistory(req);
            _returnAssetService.UpdateAssetHistory(req, entity);
            return Redirect("/Asset/Index");

        }

    }
}
