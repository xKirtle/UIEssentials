using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using UIEssentials.UI.Elements;

namespace UIEssentials.UI
{
    class TestMenu : UIState
    {
        public static TestMenu Instance;
        public static UIPanel testPanel;
        public override void OnInitialize()
        {
            Instance = this;
        }
        public static void CreateMenuPanel()
        {
            float TPWidth = 213f;
            float TPHeight = 167f;

            testPanel = new UIPanel();
            testPanel.Width.Set(TPWidth, 0);
            testPanel.Height.Set(TPHeight, 0);
            testPanel.Left.Set(Main.screenWidth / 2 - TPWidth / 2, 0);
            testPanel.Top.Set(Main.screenHeight / 2 - TPHeight / 2, 0);
            testPanel.BorderColor = Color.Red;
            testPanel.BackgroundColor = Color.Transparent;

            CustomItemSlot customItemSlot = new CustomItemSlot(itemType: ItemID.AdamantiteSword);
            testPanel.Append(customItemSlot);

            Instance.Append(testPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
