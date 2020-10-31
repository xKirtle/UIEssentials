using Terraria;
using Terraria.ModLoader;
using UIEssentials.UI;

namespace UIEssentials.Commands
{
    class EventCommand : ModCommand
    {
        public override string Command => "x";
        public override string Description => "Plays a random test event";
        public override CommandType Type => CommandType.Chat;

        public override void Action(CommandCaller caller, string input, string[] args)
        {
            Main.NewText("Before: " + TestMenu.testPanel.Size);
            TestMenu.testPanel.SetScale(TestMenu.testPanel.Scale + 0.25f);
            Main.NewText("After: " + TestMenu.testPanel.Size);
        }
    }
}
