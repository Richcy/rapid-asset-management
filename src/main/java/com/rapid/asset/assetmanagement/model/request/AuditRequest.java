package com.rapid.asset.assetmanagement.model.request;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AuditRequest {
//    private Long assetId;
//    @ManyToOne
//    @JoinColumn(name = "asset_id")
    private AssetOfAuditRequest asset;
    private String assetCondition;

    private String operationSystem;

    private String licenseWindows;

    private String typeOffice;

    private String licenseOffice;

    private Boolean antiVirus;

//    private String result;
//
//    private String createdBy;
//    private LocalDate createAt;
//    private String updatedBy;
//    private LocalDate updatedAt;
}
