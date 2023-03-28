using Asset_Management.Repository;
using Asset_Management.ViewModel;

namespace Asset_Management.Service
{
    public class AssetService
    {
        private readonly ApplicationDbContext _context;

        public AssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        public AssetModel EntityToModel(AssetEntity entity)
        {
            AssetModel model = new AssetModel();
            model.Id = entity.Id;
            model.AssetName = entity.AssetName;
            model.Specification = entity.Specification;
            model.SerialNumber = entity.SerialNumber;
            model.PurchaseYear = entity.PurchaseYear;
            model.Used = entity.Used;

            return model;
        }

        public void ModelToEntity(AssetModel model, AssetEntity entity)
        {
            entity.Id = model.Id;
            entity.AssetName = model.AssetName;
            entity.Specification = model.Specification;
            entity.SerialNumber = model.SerialNumber;
            entity.PurchaseYear = model.PurchaseYear;
            entity.Used = model.Used;
        }

        public AssetListResponse EntityToListResponse(AssetEntity entity, int num)
        {
            var response = new AssetListResponse();
            response.Id = entity.Id;
            response.Number = num;
            response.AssetId = "ASRT" + entity.Id.ToString().PadLeft(5, '0');
            response.AssetName = entity.AssetName;
            response.AssetNameWithSnSpec = entity.AssetName + "/" + entity.SerialNumber + "/" + entity.Specification + "/" + entity.PurchaseYear;
            response.Specification = entity.Specification;
            response.SerialNumber = entity.SerialNumber;
            response.PurchaseYear = entity.PurchaseYear;

            entity.AssetHistory = _context.AssetHistoryEntities.Where(x => x.AssetId == entity.Id).ToList();
            if (entity.Used == true)
            {
                var pic = _context.PicEntities.Find(entity.AssetHistory.OrderBy(x => x.Id).Last().PicId);
                response.Status = "Used By " + pic.FullName;
            }
            else
            {
                response.Status = "Available ";
            }
            return response;
        }

        public AssetHistoryResponse EntityToHistoryResponse(AssetHistoryEntity entity)
        {
            var response = new AssetHistoryResponse();
            response.Id = entity.Id;
            response.AssetId = entity.AssetId;

            var asset = _context.AssetEntities.Find(entity.AssetId);
            response.AssetName = asset.AssetName;
            response.Specification = asset.Specification;
            response.SerialNumber = asset.SerialNumber;
            response.PurchaseYear = asset.PurchaseYear;
            response.Location = entity.Location;
            response.PicId = entity.PicId;

            var pic = _context.PicEntities.Find(entity.PicId);
            response.PicName = pic.FullName;
            response.SendDate = entity.SendDate;
            response.ReturnDate = entity.ReturnDate;

            return response;

        }

        public List<AssetListResponse> GetAssets()
        {
            var entityList = _context.AssetEntities.ToList();
            int number = 1;

            var listRes = new List<AssetListResponse>();
            foreach (var entity in entityList)
            {
                listRes.Add(EntityToListResponse(entity, number));
                number++;
            }
            return listRes;
        }

        public List<AssetListResponse> GetAssetNotUsed()
        {
            var entityList = _context.AssetEntities.Where(x => x.Used == false).ToList();
            int number = 1;

            var listRes = new List<AssetListResponse>();
            foreach (var entity in entityList)
            {
                listRes.Add(EntityToListResponse(entity, number));
                number++;
            }
            return listRes;
        }

        public List<AssetListResponse> GetUsedAsset()
        {
            var entityList = _context.AssetEntities.Where(x => x.Used == true).ToList();
            int number = 1;

            var listRes = new List<AssetListResponse>();
            foreach (var entity in entityList)
            {
                listRes.Add(EntityToListResponse(entity, number));
                number++;
            }
            return listRes;
        }

        public List<AssetHistoryResponse> ReadHistoryAssetById(long? id)
        {
            var assetHistory = _context.AssetHistoryEntities.Where(x => x.AssetId == id).ToList();

            var listRes = new List<AssetHistoryResponse>();
            foreach (var entity in assetHistory)
            {
                listRes.Add(EntityToHistoryResponse(entity));
            }
            return listRes;
        }

        public void CreateAsset(AssetEntity newAsset)
        {
            _context.AssetEntities.Add(newAsset);
            _context.SaveChanges();
        }

        public AssetModel ReadAsset(long? id)
        {
            var asset = _context.AssetEntities.Find(id);
            return EntityToModel(asset);
        }

        public void UpdateAsset(AssetModel asset)
        {
            var entity = _context.AssetEntities.Find(asset.Id);
            ModelToEntity(asset, entity);
            _context.AssetEntities.Update(entity);
            _context.SaveChanges();
        }

        public void DeleteAsset(long? id)
        {
            var entity = _context.AssetEntities.Find(id);

            _context.AssetEntities.Remove(entity);
            _context.SaveChanges();
        }
    }
}