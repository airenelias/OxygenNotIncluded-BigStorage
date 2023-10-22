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
        [Option("Big Storage Bin Capacity (kg)", "Determines the capacity of the Big Storage Bin in kg.", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("Big Beautiful Storage Bin Capacity (kg)", "Determines the capacity of the Big Beautiful Storage Bin in kg.", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigBeautifulStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("Big Smart Storage Bin Capacity (kg)", "Determines the capacity of the Big Smart Storage Bin in kg.", Format = "F0")]
        [Limit(1000, 1000000)]
        public int BigSmartStorageLockerCapacity { get; set; } = 80000;

        [JsonProperty]
        [Option("Big Liquid Reservoir Capacity (kg)", "Determines the capacity of the Big Liquid Reservoir in kg.", Format = "F0")]
        [Limit(1000, 100000)]
        public int BigLiquidStorageCapacity { get; set; } = 20000;

        [JsonProperty]
        [Option("Big Gas Reservoir Capacity (kg)", "Determines the capacity of the Big Gas Reservoir in kg.", Format = "F0")]
        [Limit(100, 10000)]
        public int BigGasStorageCapacity { get; set; } = 600;
    }
}
