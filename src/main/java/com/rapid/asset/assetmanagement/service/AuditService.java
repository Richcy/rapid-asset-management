package com.rapid.asset.assetmanagement.service;

import com.rapid.asset.assetmanagement.model.AuditModel;
import com.rapid.asset.assetmanagement.model.request.AuditRequest;

import java.util.Optional;

public interface AuditService {
    Optional<AuditRequest> save(AuditRequest auditRequest);
}
