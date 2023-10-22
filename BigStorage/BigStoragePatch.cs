using HarmonyLib;
using KMod;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;

namespace BigStorage
{
    public class BigStoragePatch : UserMod2
    {
        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PVersionCheck().Register(this, new SteamVersionChecker());
            new POptions().RegisterOptions(this, typeof(BigStorageConfig));
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public class BigStorageGeneratedBuildingsPatch
        {
            public static void Prefix()
            {
                Strings.Add(BigStorageLockerConfig.NAME.key.String, BigStorageLockerConfig.NAME.text);
                Strings.Add(BigStorageLockerConfig.DESC.key.String, BigStorageLockerConfig.DESC.text);
                Strings.Add(BigStorageLockerConfig.EFFECT.key.String, BigStorageLockerConfig.EFFECT.text);
                ModUtil.AddBuildingToPlanScreen("Base", BigStorageLockerConfig.ID);

                Strings.Add(BigBeautifulStorageLockerConfig.NAME.key.String, BigBeautifulStorageLockerConfig.NAME.text);
                Strings.Add(BigBeautifulStorageLockerConfig.DESC.key.String, BigBeautifulStorageLockerConfig.DESC.text);
                Strings.Add(BigBeautifulStorageLockerConfig.EFFECT.key.String, BigBeautifulStorageLockerConfig.EFFECT.text);
                ModUtil.AddBuildingToPlanScreen("Base", BigBeautifulStorageLockerConfig.ID);

                Strings.Add(BigSmartStorageLockerConfig.NAME.key.String, BigSmartStorageLockerConfig.NAME.text);
                Strings.Add(BigSmartStorageLockerConfig.DESC.key.String, BigSmartStorageLockerConfig.DESC.text);
                Strings.Add(BigSmartStorageLockerConfig.EFFECT.key.String, BigSmartStorageLockerConfig.EFFECT.text);
                ModUtil.AddBuildingToPlanScreen("Base", BigSmartStorageLockerConfig.ID);

                Strings.Add(BigLiquidStorage.NAME.key.String, BigLiquidStorage.NAME.text);
                Strings.Add(BigLiquidStorage.DESC.key.String, BigLiquidStorage.DESC.text);
                Strings.Add(BigLiquidStorage.EFFECT.key.String, BigLiquidStorage.EFFECT.text);
                ModUtil.AddBuildingToPlanScreen("Base", BigLiquidStorage.ID);

                Strings.Add(BigGasStorageConfig.NAME.key.String, BigGasStorageConfig.NAME.text);
                Strings.Add(BigGasStorageConfig.DESC.key.String, BigGasStorageConfig.DESC.text);
                Strings.Add(BigGasStorageConfig.EFFECT.key.String, BigGasStorageConfig.EFFECT.text);
                ModUtil.AddBuildingToPlanScreen("Base", BigGasStorageConfig.ID);
            }

            [HarmonyPatch(typeof(Db), "Initialize")]
            public class BigStorageDbPatch
            {
                public static void Postfix() // Prefix ==TO==> Postfix
                {
                    Db.Get().Techs.Get("RefinedObjects").unlockedItemIDs.Add(BigStorageLockerConfig.ID);
                    Db.Get().Techs.Get("Smelting").unlockedItemIDs.Add(BigBeautifulStorageLockerConfig.ID);
                    Db.Get().Techs.Get("SolidTransport").unlockedItemIDs.Add(BigSmartStorageLockerConfig.ID);
                    Db.Get().Techs.Get("LiquidTemperature").unlockedItemIDs.Add(BigLiquidStorage.ID);
                    Db.Get().Techs.Get("Catalytics").unlockedItemIDs.Add(BigGasStorageConfig.ID);
                }
            }
        }
    }
}
