package com.rapid.asset.assetmanagement.entity;

import com.rapid.asset.assetmanagement.model.UserModel;
import com.rapid.asset.assetmanagement.model.request.AssetOfAuditRequest;
import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;
import org.springframework.beans.BeanUtils;

import javax.persistence.*;
import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
@Table(name = "asset_table")
public class AssetEntity {
    @Id
    @TableGenerator(name = "asset_id_generator", table = "sequence_tab",
            pkColumnName ="gen_name", valueColumnName = "gen_value",
            pkColumnValue = "asset_id", initialValue = 0, allocationSize = 0)
    @GeneratedValue(strategy = GenerationType.TABLE, generator = "asset_id_generator")
    private Long id;
    @Column(name = "name", length = 50)
    private String name;
//    @Column(name = "type_processor", length = 20)
//    private String typeProcessor;
    @Column(name = "serial_number")
    private String serialNumber;
    @Column(name = "purchase_year")
    private Integer purchaseYear;
    @Column(name = "location", length = 20)
    private String location;
    @Column(name = "PIC_id", length = 50)
    private String picId;
    @Column(name = "status")
    private Boolean status;
    @Column(name = "create_by")
    private String createBy;
    @Column(name = "create_at")
    private LocalDate createAt;
    @Column(name = "update_by")
    private String updateBy;
    @Column(name = "updated_at")
    private LocalDate updatedAt;

    @OneToMany(mappedBy = "asset", cascade = CascadeType.ALL)
    private List<UsersEntity> users;

    @OneToMany(mappedBy = "asset", cascade = CascadeType.ALL)
    private List<AuditEntity> auditEntityList= new ArrayList<>();

}
