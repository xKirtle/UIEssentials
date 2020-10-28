using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using Terraria.UI;

namespace UIEssentials.UI.Elements
{
    class CustomUIPanel : CustomUIElement
    {
        private readonly int _cornerSize = 12;
        private readonly int _barSize = 4;
        /// <summary>
        /// Get the current background texture.
        /// </summary>
        public Texture2D BackgroundTexture { get; private set; }
        /// <summary>
        /// Get the current border texture.
        /// </summary>
        public Texture2D BorderTexture { get; private set; }
        /// <summary>
        /// Get the current background color.
        /// </summary>
        public Color BackgroundColor { get; private set; }
        /// <summary>
        /// Get the current border color.
        /// </summary>
        public Color BorderColor { get; private set; }

        /// <summary></summary>
        /// <param name="backgroundColor">UIPanel's background color.</param>
        /// <param name="borderColor">UIPanel's border color.</param>
        /// <param name="scale">UIPanel's drawing scale</param>
        /// <param name="opacity">UIPanel's opacity (higher value, higher opacity)</param>
        /// <param name="isRendered">Whether the ItemSlot is rendered or not.</param>
        /// <param name="backgroundTexture">UIPanel's background texture (may break its appearance)</param>
        /// <param name="borderTexture">UIPanel's border texture (may break its appearance)</param>
        public CustomUIPanel(Color backgroundColor = default, Color borderColor = default, float scale = 1f, float opacity = 1f, bool isRendered = true, Texture2D backgroundTexture = null, Texture2D borderTexture = null)
        {
            SetScale(scale);
            SetOpacity(opacity);
            BackgroundColor = backgroundColor == default ? new Color(63, 82, 151) * Opacity : backgroundColor * Opacity;
            BorderColor = borderColor == default ? Color.Black * Opacity : borderColor * Opacity;
            SetPadding(0);

            if (isRendered) Show();
            else Hide();

            BorderTexture = borderTexture ?? ModContent.GetTexture("Terraria/UI/PanelBorder");
            BackgroundTexture = backgroundTexture ?? ModContent.GetTexture("Terraria/UI/PanelBackground");

            Width.Set((BackgroundTexture.Width + BorderTexture.Width) * Scale, 0);
            Height.Set((BackgroundTexture.Height + BorderTexture.Height) * Scale, 0);
        }

        private void DrawPanel(SpriteBatch spriteBatch, Texture2D texture, Color color)
        {
            //Vanilla code
            CalculatedStyle dimensions = GetDimensions();
            Point point = new Point((int)dimensions.X, (int)dimensions.Y);
            Point point2 = new Point(point.X + (int)dimensions.Width - _cornerSize, point.Y + (int)dimensions.Height - _cornerSize);
            int width = point2.X - point.X - _cornerSize;
            int height = point2.Y - point.Y - _cornerSize;
            spriteBatch.Draw(texture, new Rectangle(point.X, point.Y, _cornerSize, _cornerSize), new Rectangle?(new Rectangle(0, 0, _cornerSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y, _cornerSize, _cornerSize), new Rectangle?(new Rectangle(_cornerSize + _barSize, 0, _cornerSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point.X, point2.Y, _cornerSize, _cornerSize), new Rectangle?(new Rectangle(0, _cornerSize + _barSize, _cornerSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point2.Y, _cornerSize, _cornerSize), new Rectangle?(new Rectangle(_cornerSize + _barSize, _cornerSize + _barSize, _cornerSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point.Y, width, _cornerSize), new Rectangle?(new Rectangle(_cornerSize, 0, _barSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point2.Y, width, _cornerSize), new Rectangle?(new Rectangle(_cornerSize, _cornerSize + _barSize, _barSize, _cornerSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point.X, point.Y + _cornerSize, _cornerSize, height), new Rectangle?(new Rectangle(0, _cornerSize, _cornerSize, _barSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point2.X, point.Y + _cornerSize, _cornerSize, height), new Rectangle?(new Rectangle(_cornerSize + _barSize, _cornerSize, _cornerSize, _barSize)), color);
            spriteBatch.Draw(texture, new Rectangle(point.X + _cornerSize, point.Y + _cornerSize, width, height), new Rectangle?(new Rectangle(_cornerSize, _cornerSize, _barSize, _barSize)), color);
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!IsRendered) return;

            if (BackgroundTexture != null)
                DrawPanel(spriteBatch, BackgroundTexture, BackgroundColor);

            if (BorderTexture != null)
                DrawPanel(spriteBatch, BorderTexture, BorderColor);
        }

        //GET/SET METHODS

        /// <summary>
        /// Sets the UIPanel background texture.
        /// </summary>
        /// <param name="texture">A XNA Framework Texture2D object<./param>
        public void SetBackgroundTexture(Texture2D texture)
        {
            BackgroundTexture = texture ?? ModContent.GetTexture("Terraria/UI/PanelBackground");
        }

        /// <summary>
        /// Sets the UIPanel border texture.
        /// </summary>
        /// <param name="texture">A XNA Framework Texture2D object.</param>
        public void SetBorderTexture(Texture2D texture)
        {
            BorderTexture = texture ?? ModContent.GetTexture("Terraria/UI/PanelBorder");
        }

        /// <summary>
        /// Sets the UIPanel background color.
        /// </summary>
        /// <param name="color">A XNA Framework Color object.</param>
        public void SetBackgroundColor(Color color)
        {
            BackgroundColor = color == default ? new Color(63, 82, 151) * Opacity : color * Opacity; ;
        }

        /// <summary>
        /// Sets the UIPanel border color.
        /// </summary>
        /// <param name="color">A XNA Framework Color object</param>
        public void SetBorderColor(Color color)
        {
            BorderColor = color == default ? Color.Black * Opacity : color * Opacity;
        }
    }
}
