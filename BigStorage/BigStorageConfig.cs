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
        [Option("BigStorage.STRINGS.CONFIG.CAPACITY.BIGSTORAGELOCKER.TITLE",
            "BigStorage.STRINGS.CONFIG.CAPACITY.BIGSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("BigStorage.STRINGS.CONFIG.CAPACITY.BIGBEAUTIFULSTORAGELOCKER.TITLE",
            "BigStorage.STRINGS.CONFIG.CAPACITY.BIGBEAUTIFULSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigBeautifulStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("BigStorage.STRINGS.CONFIG.CAPACITY.BIGSMARTSTORAGELOCKER.TITLE",
            "BigStorage.STRINGS.CONFIG.CAPACITY.BIGSMARTSTORAGELOCKER.TOOLTIP", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigSmartStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("BigStorage.STRINGS.CONFIG.CAPACITY.BIGLIQUIDSTORAGE.TITLE",
            "BigStorage.STRINGS.CONFIG.CAPACITY.BIGLIQUIDSTORAGE.TOOLTIP", Format = "F0")]
        [Limit(1000, 100000)]
        public int BigLiquidStorageCapacity { get; set; } = 20000;

        [JsonProperty]
        [Option("BigStorage.STRINGS.CONFIG.CAPACITY.BIGGASSTORAGE.TITLE",
            "BigStorage.STRINGS.CONFIG.CAPACITY.BIGGASSTORAGE.TOOLTIP", Format = "F0")]
        [Limit(100, 10000)]
        public int BigGasStorageCapacity { get; set; } = 600;
    }
}
