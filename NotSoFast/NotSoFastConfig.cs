using Newtonsoft.Json;
using PeterHan.PLib.Options;

namespace NotSoFast
{
    [ConfigFile(IndentOutput: true, SharedConfigLocation: true)]
    internal class NotSoFastConfig : SingletonOptions<NotSoFastConfig>
    {
        [JsonProperty]
        [Option("STRINGS.UI.ANIMATION.SPEED.TITLE",
            "STRINGS.UI.ANIMATION.SPEED.TOOLTIP", Format = "F0")]
        [Limit(0, 200)]
        public int Speed { get; set; } = 80;

        [JsonProperty]
        [Option("STRINGS.UI.ANIMATION.OVERSPEED.TITLE",
            "STRINGS.UI.ANIMATION.OVERSPEED.TOOLTIP", Format = "F0")]
        [Limit(0, 200)]
        public int Overspeed { get; set; } = 50;
    }
}
