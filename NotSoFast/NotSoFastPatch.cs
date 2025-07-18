using HarmonyLib;
using KMod;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System;
using System.IO;
using System.Reflection;

namespace NotSoFast
{
    public class NotSoFastPatch : UserMod2
    {
        // animation speed for dupes with zero athletic
        private const float DEFAULTANIMSPEED = 1.25f;

        private static float SPEEDMULTIPLIER;
        private static float OVERSPEEDMULTIPLIER;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PVersionCheck().Register(this, new SteamVersionChecker());
            new POptions().RegisterOptions(this, typeof(NotSoFastConfig));
        }

        [HarmonyPatch(typeof(Game), "Load")]
        public static class LoadPatch
        {
            public static void Prefix()
            {
                // read the config file each time the game is loaded - so we don't need to restart all the game
                NotSoFastConfig config = POptions.ReadSettings<NotSoFastConfig>() ?? new NotSoFastConfig();
                SPEEDMULTIPLIER = config.Speed / 100f;
                OVERSPEEDMULTIPLIER = config.Overspeed / 100f;
            }
        }

        [HarmonyPatch(typeof(BipedTransitionLayer), "BeginTransition")]
        public class BeginTransitionPatch
        {
            public static void Postfix(ref Navigator.ActiveTransition transition)
            {
                if (SPEEDMULTIPLIER != 1)
                {
                    transition.animSpeed *= SPEEDMULTIPLIER;
                }

                if (OVERSPEEDMULTIPLIER != 1 && transition.animSpeed > DEFAULTANIMSPEED)
                {
                    transition.animSpeed = DEFAULTANIMSPEED + ((transition.animSpeed - DEFAULTANIMSPEED) * OVERSPEEDMULTIPLIER);
                }
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
                // Creates template for users to edit
                Localization.GenerateStringsTemplate(root, Path.Combine(Manager.GetDirectory(), "strings_templates"));
                // Load user created translation files
                LoadStrings();
                // Register strings without namespace
                // because we already loaded user transltions, custom languages will overwrite these
                LocString.CreateLocStringKeys(root, null);
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
