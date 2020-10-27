using Terraria;

namespace UIEssentials.Utilities
{
    internal static class HelperMethods
    {
        public static Item SetItemType(this Item item, int itemType)
        {
            item.SetDefaults(itemType);
            return item;
        }
    }
}
