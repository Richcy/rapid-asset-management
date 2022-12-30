package com.rapid.asset.assetmanagement.repository;

import com.rapid.asset.assetmanagement.entity.AuditEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface AuditRepository extends JpaRepository<AuditEntity, Long> {
}
