package com.rapid.asset.assetmanagement.entity;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;
import java.util.Date;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
@Table(name = "users_table")
public class UsersEntity {
    @Id
    @TableGenerator(name = "users_id_generator", table = "sequence_tab",
            pkColumnName ="gen_name", valueColumnName = "gen_value",
            pkColumnValue = "users_id", initialValue = 0, allocationSize = 0)
    @GeneratedValue(strategy = GenerationType.TABLE, generator = "users_id_generator")
    private Long id;
    @Column(name = "asset_id")
    private Long assetId;
    @ManyToOne
    @JoinColumn(name = "asset_id", insertable = false, updatable = false)
    private AssetEntity asset;

    @Column(name = "user")
    private String user;
    @Column(name = "send_date")
    private Date sendDate;
}
