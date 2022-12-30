package com.rapid.asset.assetmanagement.model;

import com.rapid.asset.assetmanagement.entity.AssetEntity;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.time.LocalDate;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AuditModel {
    private Long id;
    private AssetEntity asset;
    private String assetCondition; // error menggunakan nama condition
    private String operationSystem;
    private String licenseWindows;
    private String typeOffice;
    private String licenseOffice;
    private Boolean antiVirus;
    private String result;
    private String createdBy;
    private LocalDate createAt;
    private String updatedBy;
    private LocalDate updatedAt;
}
