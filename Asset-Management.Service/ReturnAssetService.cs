using Asset_Management.Repository;
using Asset_Management.ViewModel;

namespace Asset_Management.Service
{
    public class ReturnAssetService
    {
        private readonly ApplicationDbContext _context;
        public ReturnAssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AssetHistoryReq EntityToReq(AssetHistoryEntity entity)
        {
            AssetHistoryReq req = new AssetHistoryReq();
            req.Id = entity.Id;
            req.AssetId = entity.AssetId;
            req.Location = entity.Location;
            req.PicId = entity.PicId;
            req.SendDate = entity.SendDate;
            req.ReturnDate = entity.ReturnDate;

            return req;
        }

        public AssetHistoryEntity ReadAssetHistory(AssetHistoryReq req)
        {
            var updateHistory = _context.AssetHistoryEntities.Where(x => x.AssetId == req.AssetId).OrderBy(x => x.Id).Last();

            return updateHistory;
        }
        public void UpdateAssetHistory(AssetHistoryReq req, AssetHistoryEntity updated)
        {
            req.Id = updated.Id;
            req.AssetId = updated.AssetId;
            req.Location = updated.Location;
            req.PicId = updated.PicId;
            req.SendDate = updated.SendDate;

            updated.ReturnDate = req.ReturnDate;

            _context.AssetHistoryEntities.Update(updated);
            _context.SaveChanges();

            var asset = _context.AssetEntities.Find(updated.AssetId);
            asset.Used = false;
            _context.AssetEntities.Update(asset);
            _context.SaveChanges();
        }
    }
}
