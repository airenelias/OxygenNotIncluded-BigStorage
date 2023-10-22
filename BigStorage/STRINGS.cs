using STRINGS;

namespace BigStorage
{
    public static class STRINGS
    {
        public static class BUILDINGS
        {
            public static class PREFABS
            {
                public static class BIGSTORAGELOCKER
                {
                    public static LocString NAME = "Big Storage Bin";
                    public static LocString DESC = "We removed the unused tea kettle and added a moon roof!";
                    public static LocString EFFECT = "Extra space to clean up your place!";
                }

                public static class BIGBEAUTIFULSTORAGELOCKER
                {
                    public static LocString NAME = "Big Beautiful Storage Bin";
                    public static LocString DESC = "Is it storage, or is it art?";
                    public static LocString EFFECT = "Storage bin goes with any " + UI.FormatAsLink("decor", "DECOR") + "!";
                }

                public static class BIGSMARTSTORAGELOCKER
                {
                    public static LocString NAME = "Big Smart Storage Bin";
                    public static LocString DESC = "Any sufficiently smart storage bin is indistinguishable from magic!";
                    public static LocString EFFECT = "Even more space for your " + UI.FormatAsLink("smart solutions", "LOGIC") + "!";
                }

                public static class BIGLIQUIDSTORAGE
                {
                    public static LocString NAME = "Big Liquid Reservoir";
                    public static LocString DESC = "Compacted into buckyballs, maybe. Who knows?";
                    public static LocString EFFECT = "More storage through the magic of metallurgy!";
                }

                public static class BIGGASSTORAGE
                {
                    public static LocString NAME = "Big Gas Reservoir";
                    public static LocString DESC = "Using more metal gives you more space!";
                    public static LocString EFFECT = "Many times the space at twenty times the pressure!";
                }
            }
        }

        public static class CONFIG
        {
            public static class CAPACITY
            {
                public static class BIGSTORAGELOCKER
                {
                    public static LocString TITLE = "Big Storage Bin Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Storage Bin in kg";
                }

                public static class BIGBEAUTIFULSTORAGELOCKER
                {
                    public static LocString TITLE = "Big Beautiful Storage Bin Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Beautiful Storage Bin in kg";
                }

                public static class BIGSMARTSTORAGELOCKER
                {
                    public static LocString TITLE = "Big Smart Storage Bin Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Smart Storage Bin in kg";
                }

                public static class BIGLIQUIDSTORAGE
                {
                    public static LocString TITLE = "Big Liquid Reservoir Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Liquid Reservoir in kg";
                }

                public static class BIGGASSTORAGE
                {
                    public static LocString TITLE = "Big Gas Reservoir Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Gas Reservoir in kg";
                }
            }
        }
    }
}
