using Asset_Management.Repository;
using Asset_Management.ViewModel;

namespace Asset_Management.Service
{
    public class RequestAssetService
    {
        private readonly ApplicationDbContext _context;
        public RequestAssetService(ApplicationDbContext context)
        {
            _context = context;
        }

        // method to convert class
        public RequestAssetModel EntityToModel(RequestAssetEntity entity)
        {
            RequestAssetModel model = new RequestAssetModel();
            model.Id = entity.Id;
            model.PicName = entity.PicName;
            model.PicAddress = entity.PicAddress;
            model.Specification = entity.Specification;
            model.RequestDate = entity.RequestDate;

            return model;
        }
        public RequestAssetDetailResponse EntityToModelDetail(RequestAssetEntity entity)
        {
            RequestAssetDetailResponse response = new RequestAssetDetailResponse();
            response.Id = entity.Id;
            response.PicName = entity.PicName;
            response.PicAddress = entity.PicAddress;
            response.Specification = entity.Specification;
            response.RequestDate = entity.RequestDate;

            var approvals = _context.ApprovalEntities.Where(x => x.RequestAssetId == entity.Id);

            foreach (StatusEntity approvalEntity in approvals)
            {
                response.approval.Add(ApprovalEntityToResponse(approvalEntity));
            }


            return response;
        }

        public RequestListResponse EntityToListResponse(RequestAssetEntity entity)
        {
            var list = new RequestListResponse();
            list.Id = entity.Id;
            list.PicName = entity.PicName;
            list.PicAddress = entity.PicAddress;
            list.Specification = entity.Specification;
            list.RequestDate = entity.RequestDate;

            var approvalList = _context.ApprovalEntities.Where(x => x.RequestAssetId == entity.Id).ToList();
            var approval = approvalList.Last();

            list.Status = approval.Status;

            return list;
        }

        public StatusResponse ApprovalEntityToResponse(StatusEntity entity)
        {
            StatusResponse response = new StatusResponse();
            response.Id = entity.Id;
            response.Status = entity.Status;
            response.Reason = entity.Reason;
            response.UpdatedDate = entity.Date;

            return response;
        }

        // CRUD + APPROVE/REJECT
        public List<RequestListResponse> GetRequests()
        {
            var result = new List<RequestListResponse>();
            foreach (RequestAssetEntity requestAssetEntity in _context.RequestAssetEntities.ToList())
            {
                result.Add(EntityToListResponse(requestAssetEntity));
            }
            return result;
        }

        public List<RequestAssetModel> GetRequestAssets()
        {
            var result = new List<RequestAssetModel>();
            // nampilin approve ilang
            var list = _context.RequestAssetEntities.Where(x => x.Approval.Count == 1).ToList();
            // foreach (RequestAssetEntity requestAssetEntity in _context.RequestAssetEntities.ToList())
            foreach (RequestAssetEntity requestAssetEntity in list)
            {
                result.Add(EntityToModel(requestAssetEntity));
            }
            return result;
        }

        public List<RequestListResponse> GetRequestList()
        {
            var result = new List<RequestListResponse>();
            var list = _context.RequestAssetEntities.Where(x => x.Approval.OrderBy(e => e.Id).Last().Status == "Approved").ToList();
            foreach (RequestAssetEntity requestAssetEntity in list)
            {
                result.Add(EntityToListResponse(requestAssetEntity));
            }
            return result;
        }

        public void CreateRequestAsset(RequestAssetModel req)
        {
            RequestAssetEntity requestEntity = new RequestAssetEntity();
            requestEntity.Id = req.Id;
            requestEntity.PicName = req.PicName;
            requestEntity.PicAddress = req.PicAddress;
            requestEntity.Specification = req.Specification;
            requestEntity.RequestDate = DateTime.Now;

            //save request asset
            _context.RequestAssetEntities.Add(requestEntity);
            _context.SaveChanges();

            // create approval
            StatusEntity status = new StatusEntity();
            status.RequestAsset = requestEntity;
            status.RequestAssetId = requestEntity.Id;
            status.Reason = "";
            status.Status = "In Process";
            status.Date = DateTime.Now;

            // save approval to database
            _context.ApprovalEntities.Add(status);
            _context.SaveChanges();

        }

        public RequestAssetDetailResponse ReadRequestAsset(long? id)
        {
            var request = _context.RequestAssetEntities.Find(id);
            return EntityToModelDetail(request);

        }

        public void ApproveRequest(long? id)
        {
            RequestAssetEntity entity = _context.RequestAssetEntities.Find(id);
            // create approval
            StatusEntity status = new StatusEntity();
            status.RequestAsset = entity;
            status.RequestAssetId = entity.Id;
            status.Reason = "";
            status.Status = "Approved";
            status.Date = DateTime.Now;
            _context.ApprovalEntities.Add(status);
            _context.SaveChanges();

            // create pic
            PicEntity picEntity = new PicEntity();
            picEntity.FullName = entity.PicName;
            picEntity.Address = entity.PicAddress;
            _context.PicEntities.Add(picEntity);
            _context.SaveChanges();

            // edit request asset
            entity.PicId = picEntity.Id;
            _context.RequestAssetEntities.Update(entity);
            _context.SaveChanges();
        }

        public void SaveRejection(StatusReq req)
        {
            var request = _context.RequestAssetEntities.Find(req.Id);
            StatusEntity status = new StatusEntity();
            status.RequestAsset = request;
            status.RequestAssetId = request.Id;
            status.Reason = req.Reason;
            status.Status = "Rejected";
            status.Date = DateTime.Now;

            _context.ApprovalEntities.Add(status);
            _context.SaveChanges();
        }

        public AssetHistoryReq ChooseAsset(long? id)
        {
            var history = new AssetHistoryReq();
            RequestAssetEntity entity = _context.RequestAssetEntities.Find(id);
            history.RequestId = entity.Id;

            // create asset history form
            var pic = _context.PicEntities.Find(entity.PicId);
            history.PicId = pic.Id;

            return history;
        }
    }
}
