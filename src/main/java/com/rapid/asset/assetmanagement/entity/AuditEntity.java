package com.rapid.asset.assetmanagement.entity;

import com.rapid.asset.assetmanagement.model.request.AssetOfAuditRequest;
import com.rapid.asset.assetmanagement.model.request.AuditRequest;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.time.LocalDate;

@Entity
@AllArgsConstructor
@NoArgsConstructor
@Data
@Table(name = "audit_table")
public class AuditEntity {
    @Id
    @TableGenerator(name = "audit_id_generator", table = "sequence_tab",
            pkColumnName ="gen_name", valueColumnName = "gen_value",
            pkColumnValue = "audit_id", initialValue = 0, allocationSize = 0)
    @GeneratedValue(strategy = GenerationType.TABLE, generator = "audit_id_generator")
    private Long id;
    @Column(name = "asset_id", insertable = false, updatable = false)
    private Long assetId;
    @ManyToOne
    @JoinColumn(name = "asset_id")
    private AssetEntity asset;

    @Column(name = "asset_condition", length = 100)
    private String assetCondition; // error menggunakan nama condition
//    @Column(name = "operation_system", length = 20)
//    private String operationSystem;
    @Column(name = "license_windows")
    private String licenseWindows;
//    @Column(name = "type_office", length = 20)
//    private String typeOffice;
    @Column(name = "license_office")
    private String licenseOffice;
    @Column(name = "antivirus")
    private Boolean antiVirus;
    @Column(name = "result", length = 100)
    private String result;
    @Column(name = "create_by")
    private String createdBy;
    @Column(name = "create_at")
    private LocalDate createAt;
    @Column(name = "updated_by")
    private String updatedBy;
    @Column(name = "updated_at")
    private LocalDate updatedAt;

    public AuditEntity(AuditRequest auditRequest) {
        this.assetCondition = auditRequest.getAssetCondition();
        this.licenseWindows = auditRequest.getLicenseWindows();
        this.licenseOffice = auditRequest.getLicenseOffice();
        this.antiVirus = auditRequest.getAntiVirus();
    }

}
