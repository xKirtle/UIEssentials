using Terraria;
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
        /// Get/Set the current background color.
        /// </summary>
        public Color BackgroundColor { get; set; }
        /// <summary>
        /// Get/Set the current border color.
        /// </summary>
        public Color BorderColor { get; set; }

        /// <summary></summary>
        /// <param name="scale">UIPanel's drawing scale</param>
        /// <param name="opacity">UIPanel's opacity. (higher value, higher opacity)</param>
        /// <param name="isRendered">Whether the UIPanel is rendered or not.</param>
        /// <param name="backgroundColor">UIPanel's background color.</param>
        /// <param name="borderColor">UIPanel's border color.</param>
        public CustomUIPanel(float scale = 1f, float opacity = 1f, bool isRendered = true)
        {
            SetScale(scale);
            SetOpacity(opacity);

            if (isRendered) Show();
            else Hide();

            SetBackgroundTexture(null);
            SetBorderTexture(null);
            BackgroundColor = new Color(63, 82, 151);
            BorderColor = Color.Black;
            SetPadding(0);
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
                DrawPanel(spriteBatch, BackgroundTexture, BackgroundColor * Opacity);

            if (BorderTexture != null)
                DrawPanel(spriteBatch, BorderTexture, BorderColor * Opacity);
        }

        public override void SetScale(float scale)
        {
            base.SetScale(scale);

            if (BackgroundTexture != null && BorderTexture != null)
            {
                Width.Set((BackgroundTexture.Width + BorderTexture.Width) * Scale, 0);
                Height.Set((BackgroundTexture.Height + BorderTexture.Height) * Scale, 0);
            }
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
    }
}
