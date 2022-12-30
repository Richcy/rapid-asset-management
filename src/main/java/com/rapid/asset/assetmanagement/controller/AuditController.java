package com.rapid.asset.assetmanagement.controller;

import com.rapid.asset.assetmanagement.model.request.AuditRequest;
import com.rapid.asset.assetmanagement.service.AuditService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/audit")
public class AuditController {
    private AuditService auditService;

    @Autowired
    public AuditController(AuditService auditService) {
        this.auditService = auditService;
    }

    @PostMapping
    public ResponseEntity<Object> save(@RequestBody AuditRequest auditRequest){
        return ResponseEntity.ok().body(auditService.save(auditRequest));
    }
    //    @PostMapping
//    public ResponseEntity<Object> save(@RequestBody AuditRequest auditRequest){
//        return ResponseEntity.ok().body(auditRequest);
//    }
}
