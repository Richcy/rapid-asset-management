package com.rapid.asset.assetmanagement.service.impl;

import com.rapid.asset.assetmanagement.entity.AuditEntity;
import com.rapid.asset.assetmanagement.model.AuditModel;
import com.rapid.asset.assetmanagement.model.request.AuditRequest;
import com.rapid.asset.assetmanagement.repository.AuditRepository;
import com.rapid.asset.assetmanagement.service.AuditService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.Optional;
@Slf4j
@Service
public class AuditServiceImpl implements AuditService {
    private AuditRepository repository;
    @Autowired
    public AuditServiceImpl(AuditRepository repository) {
        this.repository = repository;
    }

    @Override
    public Optional<AuditRequest> save(AuditRequest auditRequest) {
        if(auditRequest == null || auditRequest.getLicenseWindows().isEmpty() || auditRequest.getLicenseOffice().isEmpty()) {
            return Optional.empty();
        }
        AuditEntity entity = new AuditEntity(auditRequest);

        try {
            repository.save(entity);
            return Optional.of(auditRequest);
        } catch (Exception e){
            log.error("Failed to save audit. Error: \n{}", e.getMessage());
            return Optional.empty();
        }
    }
}
