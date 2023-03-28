namespace Asset_Management.ViewModel
{
    public class StatusReq
    {
        public long Id { get; set; }
        public long? RequestAssetId { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
