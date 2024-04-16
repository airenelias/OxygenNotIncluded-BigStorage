using PeterHan.PLib.Options;
using System.Collections.Generic;
using STRINGS;
using TUNING;
using UnityEngine;

public class BigLiquidStorageConfig : IBuildingConfig
{
    public const string ID = "BigLiquidStorage";

    public override BuildingDef CreateBuildingDef()
    {
        BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
            ID,
            2, 3,
            "bigliquidstorage_kanim",
            100,
            180f,  // increased construction time
            TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5.Concat(TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2), // increased price
            MATERIALS.ALL_METALS.Concat(MATERIALS.REFINED_METALS),
            800f,
            BuildLocationRule.OnFloor,
            TUNING.BUILDINGS.DECOR.NONE, // no decor penalty
            NOISE_POLLUTION.NOISY.TIER0);
        buildingDef.InputConduitType = ConduitType.Liquid;
        buildingDef.OutputConduitType = ConduitType.Liquid;
        buildingDef.Floodable = false;
        buildingDef.ViewMode = OverlayModes.LiquidConduits.ID;
        buildingDef.AudioCategory = "HollowMetal";
        buildingDef.UtilityInputOffset = new CellOffset(1, 2);
        buildingDef.UtilityOutputOffset = new CellOffset(0, 0);
        buildingDef.LogicOutputPorts = new List<LogicPorts.Port>
        {
            LogicPorts.Port.OutputPort(
                SmartReservoir.PORT_ID,
                new CellOffset(0, 0),
                STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT,
                STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT_ACTIVE,
                STRINGS.BUILDINGS.PREFABS.SMARTRESERVOIR.LOGIC_PORT_INACTIVE)
        };
        GeneratedBuildings.RegisterWithOverlay(OverlayScreen.LiquidVentIDs, ID);
        return buildingDef;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
    {
        go.AddOrGet<Reservoir>();
        Storage storage = BuildingTemplates.CreateDefaultStorage(go, false);
        storage.showDescriptor = true;
        storage.allowItemRemoval = false;
        storage.storageFilters = STORAGEFILTERS.LIQUIDS;
        storage.capacityKg = SingletonOptions<BigStorage.BigStorageConfig>.Instance.BigLiquidStorageCapacity; // custom capacity
        storage.SetDefaultStoredItemModifiers(GasReservoirConfig.ReservoirStoredItemModifiers);
        storage.showCapacityStatusItem = true;
        storage.showCapacityAsMainStatus = true;
        go.AddOrGet<SmartReservoir>();
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

    public override void DoPostConfigureComplete(GameObject go)
    {
        go.AddOrGetDef<StorageController.Def>();
        go.GetComponent<KPrefabID>().AddTag(GameTags.OverlayBehindConduits);
    }
}
