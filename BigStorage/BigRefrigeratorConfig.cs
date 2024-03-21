using PeterHan.PLib.Options;
using STRINGS;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

public class BigRefrigeratorConfig : IBuildingConfig
{
    public const string ID = "BigRefrigerator";

    public override BuildingDef CreateBuildingDef()
    {
        BuildingDef buildingDef = BuildingTemplates.CreateBuildingDef(
            ID,
            1, 2,
            "bigfridge_kanim",
            30,
            15f, // increased construction time
            TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5.Concat(TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2), // increased price
            MATERIALS.RAW_MINERALS.Concat(MATERIALS.RAW_METALS),
            800f,
            BuildLocationRule.OnFloor,
            TUNING.BUILDINGS.DECOR.BONUS.TIER1,
            NOISE_POLLUTION.NOISY.TIER0);
        buildingDef.RequiresPowerInput = true;
        buildingDef.AddLogicPowerPort = false;
        buildingDef.EnergyConsumptionWhenActive = 240f; // increased power consumption
        buildingDef.SelfHeatKilowattsWhenActive = 0.125f;
        buildingDef.ExhaustKilowattsWhenActive = 0.0f;
        buildingDef.LogicOutputPorts = new List<LogicPorts.Port>()
        {
            LogicPorts.Port.OutputPort(
                FilteredStorage.FULL_PORT_ID,
                new CellOffset(0, 1),
                STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.LOGIC_PORT,
                STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.LOGIC_PORT_ACTIVE,
                STRINGS.BUILDINGS.PREFABS.REFRIGERATOR.LOGIC_PORT_INACTIVE)
        };
        buildingDef.Floodable = false;
        buildingDef.ViewMode = OverlayModes.Power.ID;
        buildingDef.AudioCategory = "Metal";
        SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_open", NOISE_POLLUTION.NOISY.TIER1);
        SoundEventVolumeCache.instance.AddVolume("fridge_kanim", "Refrigerator_close", NOISE_POLLUTION.NOISY.TIER1);
        return buildingDef;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
    {
        go.GetComponent<KPrefabID>();
    }

    public override void DoPostConfigureComplete(GameObject go)
    {
        Storage storage = go.AddOrGet<Storage>();
        storage.showInUI = true;
        storage.showDescriptor = true;
        storage.storageFilters = STORAGEFILTERS.FOOD;
        storage.allowItemRemoval = true;
        storage.capacityKg = SingletonOptions<BigStorage.BigStorageConfig>.Instance.BigRefrigeratorCapacity; // custom capacity
        storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
        storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
        storage.showCapacityStatusItem = true;
        Prioritizable.AddRef(go);
        go.AddOrGet<TreeFilterable>();
        go.AddOrGet<FoodStorage>();
        go.AddOrGet<Refrigerator>();
        RefrigeratorController.Def def = go.AddOrGetDef<RefrigeratorController.Def>();
        def.powerSaverEnergyUsage = 20f;
        def.coolingHeatKW = 0.375f;
        def.steadyHeatKW = 0.0f;
        go.AddOrGet<UserNameable>();
        go.AddOrGet<DropAllWorkable>();
        go.AddOrGetDef<RocketUsageRestriction.Def>().restrictOperational = false;
        go.AddOrGetDef<StorageController.Def>();
    }
}
