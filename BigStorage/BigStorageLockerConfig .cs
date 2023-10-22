using PeterHan.PLib.Options;
using STRINGS;
using TUNING;
using UnityEngine;

public class BigStorageLockerConfig : IBuildingConfig
{
    public const string ID = "BigStorageLocker";

    public override BuildingDef CreateBuildingDef()
    {
        BuildingDef obj = BuildingTemplates.CreateBuildingDef(
            ID,
            1, 2,
            "bigstoragelocker_kanim",
            30,
            15f, // increased construction time
            TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER5.Concat(TUNING.BUILDINGS.CONSTRUCTION_MASS_KG.TIER2), // increased price
            MATERIALS.RAW_MINERALS.Concat(MATERIALS.RAW_METALS),
            1600f,
            BuildLocationRule.OnFloor,
            TUNING.BUILDINGS.DECOR.NONE, // no decor penalty
            NOISE_POLLUTION.NONE);
        obj.Floodable = false;
        obj.AudioCategory = "Metal";
        obj.Overheatable = false;
        return obj;
    }

    public override void ConfigureBuildingTemplate(GameObject go, Tag prefab_tag)
    {
        SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
        Prioritizable.AddRef(go);
        Storage storage = go.AddOrGet<Storage>();
        storage.showInUI = true;
        storage.allowItemRemoval = true;
        storage.showDescriptor = true;
        storage.capacityKg = SingletonOptions<BigStorage.BigStorageConfig>.Instance.BigStorageLockerCapacity; // custom capacity
        storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
        storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
        storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
        storage.showCapacityStatusItem = true;
        storage.showCapacityAsMainStatus = true;
        go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
        go.AddOrGet<StorageLocker>();
        go.AddOrGet<UserNameable>();
        go.AddOrGetDef<RocketUsageRestriction.Def>();
    }

    public override void DoPostConfigureComplete(GameObject go)
    {
        go.AddOrGetDef<StorageController.Def>();
    }
}
