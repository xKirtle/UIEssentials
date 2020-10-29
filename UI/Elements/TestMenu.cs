using Microsoft.Xna.Framework;
using Terraria;
using Terraria.UI;
using Terraria.GameContent.UI.Elements;
using UIEssentials.UI.Elements;
using Terraria.ID;
using Terraria.ModLoader;
using UIEssentials.Utilities;

namespace UIEssentials.UI
{
    class TestMenu : UIState
    {
        public static TestMenu Instance;
        public static CustomUIPanel testPanel;
        public static CustomUIImage image;
        public override void OnInitialize()
        {
            Instance = this;
        }

        public static void CreateMenuPanel()
        {
            testPanel = new CustomUIPanel();
            testPanel.Width.Set(400f, 0);
            testPanel.Height.Set(200f, 0);
            testPanel.Left.Set(Main.screenWidth / 2 - testPanel.Width.Pixels / 2, 0);
            testPanel.Top.Set(Main.screenHeight / 4 - testPanel.Height.Pixels / 2, 0);
            testPanel.BorderColor = Color.Red;

            image = new CustomUIImage(Main.itemTexture[ItemID.AdamantiteSword], true);
            image.Width.Set(100f, 0);
            image.OnMouseDown += (__, _) => Main.NewText("down");
            testPanel.Append(image);

            Instance?.Append(testPanel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
