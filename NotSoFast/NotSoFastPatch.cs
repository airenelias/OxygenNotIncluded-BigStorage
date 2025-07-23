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
        private const float DefaultAnimSpeed = 1.25f;
        // pole climbing animation speed multiplier
        private const float PoleAnimMultiplier = 5f;

        private static float SpeedMultiplier;
        private static float OverspeedMultiplier;

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PVersionCheck().Register(this, new SteamVersionChecker());
            new POptions().RegisterOptions(this, typeof(NotSoFastConfig));
        }

        [HarmonyPatch(typeof(Game), "OnSpawn")]
        public static class GameOnSpawnPatch
        {
            public static void Prefix()
            {
                // read the config file each time the game is loaded - so we don't need to restart all the game
                NotSoFastConfig config = POptions.ReadSettings<NotSoFastConfig>() ?? new NotSoFastConfig();
                SpeedMultiplier = config.Speed / 100f;
                OverspeedMultiplier = config.Overspeed / 100f;
            }
        }

        [HarmonyPatch(typeof(BipedTransitionLayer), "BeginTransition")]
        public class BeginTransitionPatch
        {
            public static void Postfix(ref Navigator.ActiveTransition transition)
            {
                // skipping transition animations/jumps/climbing tiles etc.
                if (transition.isLooping)
                {
                    bool running = (transition.start == NavType.Floor);
                    bool climbingLadder = (transition.start == NavType.Ladder);
                    bool climbingPole = (transition.start == NavType.Pole);

                    // skipping not movement animations
                    if (!running && !climbingLadder && !climbingPole)
                    {
                        return;
                    }

                    // speeding up weirdly slow pole climbing animation
                    if (climbingPole)
                    {
                        transition.animSpeed *= PoleAnimMultiplier;
                    }

                    // decreasing overall speed
                    if (SpeedMultiplier != 1)
                    {
                        transition.animSpeed *= SpeedMultiplier;
                    }

                    //further decreasing fast dupes
                    if (OverspeedMultiplier != 1 && transition.animSpeed > DefaultAnimSpeed)
                    {
                        transition.animSpeed = DefaultAnimSpeed + ((transition.animSpeed - DefaultAnimSpeed) * OverspeedMultiplier);
                    }
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
