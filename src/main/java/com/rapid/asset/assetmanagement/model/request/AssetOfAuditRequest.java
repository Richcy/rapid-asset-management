package com.rapid.asset.assetmanagement.model.request;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

@Data
@AllArgsConstructor
@NoArgsConstructor
public class AssetOfAuditRequest {
    private String name;
    private String typeProcessor;
    private String serialNumber;
    private String pic;

}
