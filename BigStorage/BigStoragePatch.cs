using HarmonyLib;
using KMod;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System;
using System.IO;
using System.Reflection;

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
                ModUtil.AddBuildingToPlanScreen("Base", BigStorageLockerConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Base", BigBeautifulStorageLockerConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Base", BigSmartStorageLockerConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Base", BigLiquidStorageConfig.ID);
                ModUtil.AddBuildingToPlanScreen("Base", BigGasStorageConfig.ID);
            }

            [HarmonyPatch(typeof(Db), "Initialize")]
            public class BigStorageDbPatch
            {
                public static void Postfix()
                {
                    Db.Get().Techs.Get("RefinedObjects").unlockedItemIDs.Add(BigStorageLockerConfig.ID);
                    Db.Get().Techs.Get("Smelting").unlockedItemIDs.Add(BigBeautifulStorageLockerConfig.ID);
                    Db.Get().Techs.Get("SolidTransport").unlockedItemIDs.Add(BigSmartStorageLockerConfig.ID);
                    Db.Get().Techs.Get("LiquidTemperature").unlockedItemIDs.Add(BigLiquidStorageConfig.ID);
                    Db.Get().Techs.Get("Catalytics").unlockedItemIDs.Add(BigGasStorageConfig.ID);
                }
            }

            [HarmonyPatch(typeof(Localization), "Initialize")]
            public class Localization_Initialize_Patch
            {
                public static void Postfix() => Translate(typeof(STRINGS));

                public static void Translate(Type root)
                {
                    // Basic intended way to register strings, keeps namespace
                    Localization.RegisterForTranslation(root);
                    // Load user created translation files
                    LoadStrings();
                    // Register strings without namespace
                    // because we already loaded user transltions, custom languages will overwrite these
                    LocString.CreateLocStringKeys(root, null);
                    // Creates template for users to edit
                    Localization.GenerateStringsTemplate(root, Path.Combine(Manager.GetDirectory(), "strings_templates"));
                }

                private static void LoadStrings()
                {
                    string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "translations", Localization.GetLocale()?.Code + ".po");
                    if (File.Exists(path))
                        Localization.OverloadStrings(Localization.LoadStringsFile(path, false));
                }
            }
        }
    }
}
