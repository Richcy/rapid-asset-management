using Asset_Management.Repository;
using Asset_Management.ViewModel;

namespace Asset_Management.Service
{
    public class AssetHistoryService
    {
        private readonly ApplicationDbContext _context;
        //context = database
        public AssetHistoryService(ApplicationDbContext context)
        {
            _context = context;

        }
        public AssetHistoryResponse EntityToResponse(AssetHistoryEntity entity)
        {
            var response = new AssetHistoryResponse();

            response.Id = entity.Id;
            response.AssetId = entity.AssetId;
            response.Location = entity.Location;
            response.PicId = entity.PicId;
            response.SendDate = entity.SendDate;
            response.ReturnDate = entity.ReturnDate;

            var asset = _context.AssetEntities.Find(entity.AssetId);
            response.AssetName = asset.AssetName;
            response.Specification = asset.Specification;
            response.PurchaseYear = asset.PurchaseYear;
            response.SerialNumber = asset.SerialNumber;

            var pic = _context.PicEntities.Find(entity.PicId);
            response.PicName = pic.FullName;

            return response;
        }
        //crud
        public void CreateAssetHistory(AssetHistoryReq req)
        {
            StatusEntity status = new StatusEntity();

            status.RequestAssetId = req.RequestId;
            status.RequestAsset = _context.RequestAssetEntities.Find(req.RequestId);
            status.Reason = "";
            status.Status = "Sent";
            status.Date = DateTime.Now;
            _context.ApprovalEntities.Add(status);
            _context.SaveChanges();

            var entity = new AssetHistoryEntity();
            entity.Id = req.Id;
            entity.AssetId = req.AssetId;
            entity.Location = req.Location;
            entity.PicId = req.PicId;
            entity.SendDate = (DateTime)req.SendDate;

            var asset = _context.AssetEntities.Find(req.AssetId);
            entity.Asset = asset;

            var pic = _context.PicEntities.Find(req.PicId);
            entity.Pic = pic;

            _context.AssetHistoryEntities.Add(entity);
            _context.SaveChanges();

            asset.Used = true;
            _context.AssetEntities.Update(asset);
            _context.SaveChanges();
        }

        public List<AssetHistoryResponse> GetAssetHistory()
        {
            var list = new List<AssetHistoryResponse>();
            foreach (var item in _context.AssetHistoryEntities.ToList())
            {
                list.Add(EntityToResponse(item));
            }
            return list;
        }

        public List<AssetHistoryResponse> GetPicFromAssetHistory()
        {
            var list = new List<AssetHistoryResponse>();
            var picList = _context.AssetHistoryEntities.Where(x => x.ReturnDate == null).ToList();
            foreach (var item in picList)
            {
                list.Add(EntityToResponse(item));
            }
            return list;
        }

        public AssetHistoryReq EntityToRequest(AssetHistoryEntity entity)
        {
            var req = new AssetHistoryReq();
            req.Id = entity.Id;
            req.AssetId = entity.AssetId;
            req.Location = entity.Location;
            req.PicId = entity.PicId;
            req.SendDate = (DateTime)entity.SendDate;
            req.ReturnDate = entity.ReturnDate;

            return req;
        }

        public void RequestToEntity(AssetHistoryEntity entity, AssetHistoryReq req)
        {
            entity.Id = req.Id;
            entity.AssetId = req.AssetId;
            entity.Location = req.Location;
            entity.PicId = req.PicId;
            entity.SendDate = (DateTime)req.SendDate;
            entity.ReturnDate = req.ReturnDate;
        }

        public AssetHistoryReq ReadAssetHistory(long? id)
        {
            var asset = _context.AssetHistoryEntities.Find(id);
            return EntityToRequest(asset);
        }

        public void UpdateAssetHistory(AssetHistoryReq assetHistory)
        {
            var entity = _context.AssetHistoryEntities.Find(assetHistory.Id);
            RequestToEntity(entity, assetHistory);
            _context.AssetHistoryEntities.Update(entity);
            _context.SaveChanges();

            var asset = _context.AssetEntities.Find(assetHistory.AssetId);
            asset.Used = false;
            _context.AssetEntities.Update(asset);
            _context.SaveChanges();

        }

    }
}
