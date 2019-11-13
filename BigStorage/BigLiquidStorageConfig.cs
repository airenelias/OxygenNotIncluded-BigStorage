﻿using TUNING;
using UnityEngine;

namespace BigStorage
{
    // Token: 0x0200185C RID: 6236
    public class BigLiquidStorage : IBuildingConfig
    {

        // Token: 0x0400679A RID: 26522
        public const string ID = "BigLiquidStorage";

        // Token: 0x0400679B RID: 26523
        private const ConduitType CONDUIT_TYPE = ConduitType.Liquid;

        // Token: 0x0400679C RID: 26524
        private const int WIDTH = 2;

        // Token: 0x0400679D RID: 26525
        private const int HEIGHT = 3;

        // Token: 0x0600606A RID: 24682 RVA: 0x001D975C File Offset: 0x001D7B5C
        public override BuildingDef CreateBuildingDef()
        {
            BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef("BigLiquidStorage", WIDTH, HEIGHT, "liquidreservoir_kanim", 100, 120f, BUILDINGS.CONSTRUCTION_MASS_KG.TIER6, MATERIALS.ALL_METALS, 800f, BuildLocationRule.OnFloor, BUILDINGS.DECOR.PENALTY.TIER1, NOISE_POLLUTION.NOISY.TIER0, 0.2f);
            buildingDef.InputConduitType = ConduitType.Liquid;
            buildingDef.OutputConduitType = ConduitType.Liquid;
            buildingDef.Floodable = false;
            buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
            buildingDef.AudioCategory = "HollowMetal";
            buildingDef.UtilityInputOffset = new CellOffset(1, 2);
            buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
            return buildingDef;
        }

        // Token: 0x0600606B RID: 24683 RVA: 0x001D97E8 File Offset: 0x001D7BE8
        public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
        {
            go.AddOrGet<Reservoir>();
            Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
            storage.showDescriptor = true;
            storage.allowItemRemoval = false;
            storage.storageFilters = STORAGEFILTERS.LIQUIDS;
            storage.capacityKg = 25000f;
            storage.SetDefaultStoredItemModifiers(GasReservoirConfig.ReservoirStoredItemModifiers);
            ConduitConsumer conduitConsumer = go.AddOrGet<ConduitConsumer>();
            conduitConsumer.conduitType = ConduitType.Liquid;
            conduitConsumer.ignoreMinMassCheck = true;
            conduitConsumer.forceAlwaysSatisfied = true;
            conduitConsumer.alwaysConsume = true;
            conduitConsumer.capacityKG = storage.capacityKg;
            ConduitDispenser conduitDispenser = go.AddOrGet<ConduitDispenser>();
            conduitDispenser.conduitType = ConduitType.Liquid;
            conduitDispenser.elementFilter = null;
        }

        // Token: 0x0600606C RID: 24684 RVA: 0x001D9877 File Offset: 0x001D7C77
        public override void DoPostConfigureComplete(GameObject go)
        {
            go.AddOrGetDef<StorageController.Def>();
        }

    }


}
