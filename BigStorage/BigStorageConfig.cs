using Newtonsoft.Json;
using PeterHan.PLib.Options;
using System;

namespace BigStorage
{
    [ConfigFile]
    [RestartRequired]
    public class BigStorageConfig : SingletonOptions<BigStorageConfig>
    {
        // Capacity
        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGSTORAGELOCKER.TITLE",
            "STRINGS.UI.CAPACITY.BIGSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGBEAUTIFULSTORAGELOCKER.TITLE",
            "STRINGS.UI.CAPACITY.BIGBEAUTIFULSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigBeautifulStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGSMARTSTORAGELOCKER.TITLE",
            "STRINGS.UI.CAPACITY.BIGSMARTSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigSmartStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGLIQUIDSTORAGE.TITLE",
            "STRINGS.UI.CAPACITY.BIGLIQUIDSTORAGE.TOOLTIP", Format = "F0")]
        [Limit(1000, 100000)]
        public int BigLiquidStorageCapacity { get; set; } = 20000;

        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGGASSTORAGE.TITLE",
            "STRINGS.UI.CAPACITY.BIGGASSTORAGE.TOOLTIP", Format = "F0")]
        [Limit(100, 10000)]
        public int BigGasStorageCapacity { get; set; } = 600;

        [JsonProperty]
        [Option("STRINGS.UI.CAPACITY.BIGSTORAGETILE.TITLE",
            "STRINGS.UI.CAPACITY.BIGSTORAGETILE.TOOLTIP", Format = "F0")]
        [Limit(100, 1000000)]
        public int BigStorageTileCapacity { get; set; } = 4000;
    }
}
