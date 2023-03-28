using Asset_Management.ViewModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asset_Management.Repository
{
    [Table("asset")]
    public class AssetEntity
    {
        [Key]
        [Column("id")]
        public long Id { get; set; }

        [Column("asset_name")]
        public string AssetName { get; set; }

        [Column("specification")]
        public string Specification { get; set; }

        [Column("serial_number")]
        public string SerialNumber { get; set; }

        [Column("purchase_year")]
        public int PurchaseYear { get; set; }

        [Column("available")]
        public bool Used { get; set; }

        public List<AssetHistoryEntity> AssetHistory { get; set; }

        public AssetEntity() { }
        public AssetEntity(AssetModel model)
        {
            Id = model.Id;
            AssetName = model.AssetName;
            Specification = model.Specification;
            SerialNumber = model.SerialNumber;
            PurchaseYear = model.PurchaseYear;
        }
    }
}
