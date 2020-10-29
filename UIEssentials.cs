using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.GameInput;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI;
using UIEssentials.UI;

namespace UIEssentials
{
    public class UIEssentials : Mod
    {
        internal static bool debugMode = true;
        internal static ModHotKey ToggleTestMenu;
        public override void Load()
        {
            ToggleTestMenu = RegisterHotKey("Toggle Uncraft Menu", "X");

            if (!Main.dedServ && Main.netMode != NetmodeID.Server)
            {
                if (!debugMode) return;

                UserInterface = new CustomUserInterface();
                TestMenu = new TestMenu();
                TestMenu.Activate();
                UserInterface.SetState(TestMenu);
            }
        }

        internal static CustomUserInterface UserInterface;
        internal static TestMenu TestMenu;
        private GameTime _lastUpdateUiGameTime;
        public override void UpdateUI(GameTime gameTime)
        {
            _lastUpdateUiGameTime = gameTime;
            if (UserInterface?.CurrentState != null)
                UserInterface.Update(gameTime);
        }

        public override void ModifyInterfaceLayers(List<GameInterfaceLayer> layers)
        {
            //https://github.com/tModLoader/tModLoader/wiki/Vanilla-Interface-layers-values
            int interfaceLayer = layers.FindIndex(layer => layer.Name.Equals("Vanilla: Interface Logic 2"));
            if (interfaceLayer != -1)
            {
                layers.Insert(interfaceLayer, new LegacyGameInterfaceLayer(
                    "Uncraft Items: Menu",
                    delegate
                    {
                        if (_lastUpdateUiGameTime != null && UserInterface?.CurrentState != null)
                            UserInterface.Draw(Main.spriteBatch, _lastUpdateUiGameTime);

                        return true;
                    },
                       InterfaceScaleType.Game));
            }
        }
    }

    class UncraftModPlayer : ModPlayer
    {
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            if (UIEssentials.debugMode && UIEssentials.ToggleTestMenu.JustPressed)
            {
                if (TestMenu.testPanel != null)
                {
                    TestMenu.testPanel.Remove();
                    TestMenu.testPanel = null;
                }
                else
                    TestMenu.CreateMenuPanel();
            }
        }
    }
}