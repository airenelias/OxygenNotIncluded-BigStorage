using HarmonyLib;
using KMod;
using PeterHan.PLib.Actions;
using PeterHan.PLib.AVC;
using PeterHan.PLib.Core;
using PeterHan.PLib.Options;
using System;
using System.IO;
using System.Reflection;

namespace ResourcesSearchHotkey
{
    public class ResourcesSearchHotkeyPatch : UserMod2
    {
        private static KInputTextField
            CodexSearchField,
            DiagnosticsSearchField,
            ResourcesSearchField;

        private static string
            CodexFilter,
            DiagnosticsFilter,
            ResourcesFilter;

        private static bool
            CodexShown = false,
            DiagnosticsShown = false,
            ResourcesShown = false;

        private static PAction
            DiagnosticsShowAction,
            ResourcesShowAction;

        private static ResourcesSearchHotkeyConfig Config;

        private static void StopCamera()
        {
            // disable camera control for a moment to force stop panning
            if (CameraController.Instance != null)
            {
                CameraController.Instance.DisableUserCameraControl = true;
                CameraController.Instance.DisableUserCameraControl = false;
            }
        }

        public override void OnLoad(Harmony harmony)
        {
            base.OnLoad(harmony);
            PUtil.InitLibrary();
            new PVersionCheck().Register(this, new SteamVersionChecker());
            new POptions().RegisterOptions(this, typeof(ResourcesSearchHotkeyConfig));

            PActionManager pActionManager = new PActionManager();
            DiagnosticsShowAction = pActionManager.CreateAction(
                "ResourcesSearchHotkey.DiagnosticsShowAction", STRINGS.UI.HOTKEY.DIAGNOSTICS.TITLE, new PKeyBinding(KKeyCode.D, Modifier.Ctrl));
            ResourcesShowAction = pActionManager.CreateAction(
                "ResourcesSearchHotkey.ResourcesShowAction", STRINGS.UI.HOTKEY.RESOURCES.TITLE, new PKeyBinding(KKeyCode.F, Modifier.Ctrl));
        }

        [HarmonyPatch(typeof(Game), "OnSpawn")]
        public static class OnSpawnPatch
        {
            public static void Prefix()
            {
                // read the config file each time the game is loaded - so we don't need to restart all the game
                Config = POptions.ReadSettings<ResourcesSearchHotkeyConfig>() ?? new ResourcesSearchHotkeyConfig();
            }
        }

        [HarmonyPatch(typeof(KScreen), "OnActivate")]
        public class KScreenOnActivatePatch
        {
            public static void Postfix(KScreen __instance)
            {
                // saving search text when screen closed with escape button
                string screenType = __instance.GetType().Name;
                switch (screenType)
                {
                    case "CodexScreen":
                        CodexSearchField = Traverse.Create(__instance).Field("searchInputField").GetValue<KInputTextField>();
                        if (Config.RememberCodex)
                        {
                            CodexSearchField.restoreOriginalTextOnEscape = false;
                        }
                        return;

                    case "AllDiagnosticsScreen":
                        DiagnosticsSearchField = Traverse.Create(__instance).Field("searchInputField").GetValue<KInputTextField>();
                        if (Config.RememberDiagnostics)
                        {
                            DiagnosticsSearchField.restoreOriginalTextOnEscape = false;
                        }
                        return;

                    case "AllResourcesScreen":
                        ResourcesSearchField = Traverse.Create(__instance).Field("searchInputField").GetValue<KInputTextField>();
                        if (Config.RememberResources)
                        {
                            ResourcesSearchField.restoreOriginalTextOnEscape = false;
                        }
                        return;
                }
            }
        }
        
        [HarmonyPatch(typeof(KScreen), "OnShow")]
        public class KScreenOnShowPatch
        {
            public static void Prefix(KScreen __instance, bool show)
            {
                if (!show)
                {
                    string screenType = __instance.GetType().Name;
                    switch (screenType)
                    {
                        case "CodexScreen":
                            if (Config.RememberCodex && CodexShown)
                            {
                                CodexFilter = CodexSearchField.text;
                                CodexShown = false;
                            }
                            return;

                        case "AllDiagnosticsScreen":
                            if (Config.RememberDiagnostics && DiagnosticsShown)
                            {
                                DiagnosticsFilter = DiagnosticsSearchField.text;
                                DiagnosticsShown = false;
                            }
                            return;

                        case "AllResourcesScreen":
                            if (Config.RememberResources && ResourcesShown)
                            {
                                ResourcesFilter = ResourcesSearchField.text;
                                ResourcesShown = false;
                            }
                            return;
                    }
                }
            }

            public static void Postfix(KScreen __instance, bool show)
            {
                if (show)
                {
                    string screenType = __instance.GetType().Name;
                    switch (screenType)
                    {
                        case "CodexScreen":
                            StopCamera();

                            if (Config.RememberCodex)
                            {
                                CodexShown = true;
                                CodexSearchField.text = CodexFilter;
                            }
                            else
                            {
                                CodexSearchField.text = string.Empty;
                            }

                            if (Config.FocusCodex)
                            {
                                CodexSearchField.Select();
                            }
                            return;

                        case "AllDiagnosticsScreen":
                            StopCamera();

                            if (Config.RememberDiagnostics)
                            {
                                DiagnosticsShown = true;
                                DiagnosticsSearchField.text = DiagnosticsFilter;
                            }
                            else
                            {
                                DiagnosticsSearchField.text = string.Empty;
                            }

                            if (Config.FocusDiagnostics)
                            {
                                DiagnosticsSearchField.Select();
                            }
                            return;

                        case "AllResourcesScreen":
                            StopCamera();

                            if (Config.RememberResources)
                            {
                                ResourcesShown = true;
                                ResourcesSearchField.text = ResourcesFilter;
                                // call for filtering right away to avoid flicker
                                Traverse.Create(__instance).Method("SearchFilter", [ResourcesFilter]).GetValue();
                            }

                            if (Config.FocusResources)
                            {
                                ResourcesSearchField.Select();
                            }
                            return;
                    }
                }
            }
        }

        [HarmonyPatch(typeof(KScreen), "OnKeyDown")]
        private class KScreenOnKeyDownPatch
        {
            private static void Postfix(KScreen __instance, KButtonEvent e)
            {
                if (e.TryConsume(DiagnosticsShowAction.GetKAction()))
                {
                    if (!DiagnosticsShown)
                    {
                        KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click"));
                        AllDiagnosticsScreen.Instance?.Show();
                    }
                    e.Consumed = true;
                }

                if (e.TryConsume(ResourcesShowAction.GetKAction()))
                {
                    if (!ResourcesShown)
                    {
                        KMonoBehaviour.PlaySound(GlobalAssets.GetSound("HUD_Click"));
                        AllResourcesScreen.Instance?.Show();
                    }
                    e.Consumed = true;
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
