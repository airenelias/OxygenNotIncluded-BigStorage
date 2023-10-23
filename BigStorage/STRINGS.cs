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
                    public static LocString NAME = UI.FormatAsLink("Big Storage Bin", "BIGSTORAGELOCKER");
                    public static LocString DESC = "We removed the unused tea kettle and added a moon roof!";
                    public static LocString EFFECT = "Stores a greater amount of the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.";
                }

                public static class BIGBEAUTIFULSTORAGELOCKER
                {
                    public static LocString NAME = UI.FormatAsLink("Big Beautiful Storage Bin", "BIGBEAUTIFULSTORAGELOCKER");
                    public static LocString DESC = "Bin that goes with any interior!\nIs it storage, or is it art?";
                    public static LocString EFFECT = "Stores a greater amount of the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.\n\nIncreases " + UI.FormatAsLink("Decor", "DECOR") + ", contributing to " + UI.FormatAsLink("Morale", "MORALE") + ".";
                }

                public static class BIGSMARTSTORAGELOCKER
                {
                    public static LocString NAME = UI.FormatAsLink("Big Smart Storage Bin", "BIGSMARTSTORAGELOCKER");
                    public static LocString DESC = "Even more space for your " + UI.FormatAsLink("smart solutions", "LOGIC") + "!";
                    public static LocString EFFECT = "Stores a greater amount of the " + UI.FormatAsLink("Solid Materials", "ELEMENTS_SOLID") + " of your choosing.\n\nSends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when bin is full.";
                    public static LocString LOGIC_PORT = "Full/Not Full";
                    public static LocString LOGIC_PORT_ACTIVE = "Sends a " + UI.FormatAsAutomationState("Green Signal", UI.AutomationState.Active) + " when full";
                    public static LocString LOGIC_PORT_INACTIVE = "Otherwise, sends a " + UI.FormatAsAutomationState("Red Signal", UI.AutomationState.Standby);
                }

                public static class BIGLIQUIDSTORAGE
                {
                    public static LocString NAME = UI.FormatAsLink("Big Liquid Reservoir", "BIGLIQUIDSTORAGE");
                    public static LocString DESC = "Compacted into buckyballs, maybe. Who knows?";
                    public static LocString EFFECT = "Stores a greater amount of the " + UI.FormatAsLink("Liquid", "ELEMENTS_LIQUID") + " resources piped into it.";
                }

                public static class BIGGASSTORAGE
                {
                    public static LocString NAME = UI.FormatAsLink("Big Gas Reservoir", "BIGGASSTORAGE");
                    public static LocString DESC = "Ten times the space at twenty times the pressure!";
                    public static LocString EFFECT = "Stores a greater amount of the " + UI.FormatAsLink("Gas", "ELEMENTS_GAS") + " resources piped into it.";
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
                    public static LocString TOOLTIP = "Determines the capacity of the Big Storage Bin";
                }

                public static class BIGBEAUTIFULSTORAGELOCKER
                {
                    public static LocString TITLE = "Big Beautiful Storage Bin Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Beautiful Storage Bin";
                }

                public static class BIGSMARTSTORAGELOCKER
                {
                    public static LocString TITLE = "Big Smart Storage Bin Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Smart Storage Bin";
                }

                public static class BIGLIQUIDSTORAGE
                {
                    public static LocString TITLE = "Big Liquid Reservoir Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Liquid Reservoir";
                }

                public static class BIGGASSTORAGE
                {
                    public static LocString TITLE = "Big Gas Reservoir Capacity (kg)";
                    public static LocString TOOLTIP = "Determines the capacity of the Big Gas Reservoir";
                }
            }
        }
    }
}
