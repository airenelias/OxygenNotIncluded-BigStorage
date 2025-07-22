using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace ResourcesSearchHotkey
{
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    internal class ResourcesSearchHotkeyConfig : SingletonOptions<ResourcesSearchHotkeyConfig>
    {
        [JsonProperty]
        [Option("STRINGS.UI.FOCUS.TITLE",
            "STRINGS.UI.FOCUS.TOOLTIP",
            "STRINGS.UI.CATEGORY.CODEX.TITLE")]
        public bool FocusCodex { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.UI.REMEMBER.TITLE",
            "STRINGS.UI.REMEMBER.TOOLTIP",
            "STRINGS.UI.CATEGORY.CODEX.TITLE")]
        public bool RememberCodex { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.UI.FOCUS.TITLE",
            "STRINGS.UI.FOCUS.TOOLTIP",
            "STRINGS.UI.CATEGORY.DIAGNOSTICS.TITLE")]
        public bool FocusDiagnostics { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.UI.REMEMBER.TITLE",
            "STRINGS.UI.REMEMBER.TOOLTIP",
            "STRINGS.UI.CATEGORY.DIAGNOSTICS.TITLE")]
        public bool RememberDiagnostics { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.UI.FOCUS.TITLE",
            "STRINGS.UI.FOCUS.TOOLTIP",
            "STRINGS.UI.CATEGORY.RESOURCES.TITLE")]
        public bool FocusResources { get; set; } = true;

        [JsonProperty]
        [Option("STRINGS.UI.REMEMBER.TITLE",
            "STRINGS.UI.REMEMBER.TOOLTIP",
            "STRINGS.UI.CATEGORY.RESOURCES.TITLE")]
        public bool RememberResources { get; set; } = true;
    }
}
