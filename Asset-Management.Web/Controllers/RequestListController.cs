using Asset_Management.Repository;
using Asset_Management.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Asset_Management.Web.Controllers
{
    public class RequestListController : Controller
    {
        private readonly RequestAssetService _reqAssetService;
        private readonly AssetService _assetService;

        public RequestListController(ApplicationDbContext context)
        {
            _reqAssetService = new RequestAssetService(context);
            _assetService = new AssetService(context);
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            var result = _reqAssetService.GetRequestList();
            return View(result);
        }

        [AllowAnonymous]
        public IActionResult ChooseAsset(long? id)
        {
            ViewBag.Asset = new SelectList(_assetService.GetAssetNotUsed(), "Id", "AssetNameWithSnSpec");
            var history = _reqAssetService.ChooseAsset(id);
            return View(history);
        }
    }
}
