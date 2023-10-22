﻿using PeterHan.PLib.Options;
using System.Collections.Generic;
using TUNING;
using UnityEngine;

namespace BigStorage
{
    internal class BigSmartStorageLockerConfig : IBuildingConfig
    {
        public const string ID = "BigSmartStorageLocker";

        public static LocString NAME = new LocString(
            "Big Smart Storage Bin",
            "STRINGS.BUILDINGS.PREFABS." + ID.ToUpper() + ".NAME"
        );

        public static LocString DESC = new LocString(
            "Any sufficiently smart storage bin is indistinguishable from magic! Big Solid Storage by RoJCo™",
            "STRINGS.BUILDINGS.PREFABS." + ID.ToUpper() + ".DESC"
        );

        public static LocString EFFECT = new LocString(
            "Ever more space for your smart solutions!",
            "STRINGS.BUILDINGS.PREFABS." + ID.ToUpper() + ".EFFECT"
        );

        public override BuildingDef CreateBuildingDef()
        {
            BuildingDef obj = BuildingTemplates.CreateBuildingDef(
                ID,
                1, 2,
                "bigsmartstoragelocker_kanim",
                30,
                180f, // increased construction time
                BUILDINGS.CONSTRUCTION_MASS_KG.TIER4, // increased price
                MATERIALS.REFINED_METALS,
                1600f,
                BuildLocationRule.OnFloor,
                BUILDINGS.DECOR.NONE, // no decor penalty
                NOISE_POLLUTION.NONE
            );
            obj.Floodable = false;
            obj.AudioCategory = "Metal";
            obj.Overheatable = false;
            obj.ViewMode = OverlayModes.Logic.ID;
            obj.RequiresPowerInput = true;
            obj.AddLogicPowerPort = false;
            obj.EnergyConsumptionWhenActive = 60f;
            obj.ExhaustKilowattsWhenActive = 0.125f;
            obj.LogicOutputPorts = new List<LogicPorts.Port>
            {
                LogicPorts.Port.OutputPort(
                    FilteredStorage.FULL_PORT_ID,
                    new CellOffset(0, 1),
                    STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT,
                    STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT_ACTIVE,
                    STRINGS.BUILDINGS.PREFABS.STORAGELOCKERSMART.LOGIC_PORT_INACTIVE,
                    show_wire_missing_icon: true
                )
            };
            return obj;
        }

        public override void DoPostConfigureComplete(GameObject go)
        {
            SoundEventVolumeCache.instance.AddVolume("storagelocker_kanim", "StorageLocker_Hit_metallic_low", NOISE_POLLUTION.NOISY.TIER1);
            Prioritizable.AddRef(go);
            Storage storage = go.AddOrGet<Storage>();
            storage.showInUI = true;
            storage.allowItemRemoval = true;
            storage.showDescriptor = true;
            storage.capacityKg = SingletonOptions<BigStorageConfig>.Instance.BigSmartStorageLockerCapacity; // custom capacity
            storage.storageFilters = STORAGEFILTERS.NOT_EDIBLE_SOLIDS;
            storage.storageFullMargin = STORAGE.STORAGE_LOCKER_FILLED_MARGIN;
            storage.fetchCategory = Storage.FetchCategory.GeneralStorage;
            storage.showCapacityStatusItem = true;
            storage.showCapacityAsMainStatus = true;
            go.AddOrGet<CopyBuildingSettings>().copyGroupTag = GameTags.StorageLocker;
            go.AddOrGet<StorageLockerSmart>();
            go.AddOrGet<UserNameable>();
            go.AddOrGetDef<StorageController.Def>();
            go.AddOrGetDef<RocketUsageRestriction.Def>();
        }
    }
}
