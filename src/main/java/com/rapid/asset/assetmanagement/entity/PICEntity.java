package com.rapid.asset.assetmanagement.entity;

import lombok.AllArgsConstructor;
import lombok.Data;
import lombok.NoArgsConstructor;

import javax.persistence.*;

@Entity
@Data
@AllArgsConstructor
@NoArgsConstructor
public class PICEntity {
    @Id
    @TableGenerator(name = "pic_id_generator", table = "sequence_tab",
            pkColumnName ="gen_name", valueColumnName = "gen_value",
            pkColumnValue = "pic_id", initialValue = 0, allocationSize = 0)
    @GeneratedValue(strategy = GenerationType.TABLE, generator = "pic_id_generator")
    private Long id;
    @Column(name = "name")
    private String name;
    @Column(name = "asset_id")
    private Long assetId;
//    @JoinColumn(name = "asset_id")
//    private AssetEntity assetEntity;
}
