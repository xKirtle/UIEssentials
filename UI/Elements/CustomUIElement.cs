using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria.UI;

namespace UIEssentials.UI.Elements
{
    public class CustomUIElement : UIElement
    {
        /// <summary>
        /// Get the current drawing scale.
        /// </summary>
        public float Scale { get; private set; }
        /// <summary>
        /// Get the current opacity.
        /// </summary>
        public float Opacity { get; private set; }
        /// <summary>
        /// Get the current render state.
        /// </summary>
        public bool IsRendered { get; private set; }

        public delegate void CustomEventHandler(CustomUIElement sender, EventArgs e);
        public event CustomEventHandler OnShow;
        public event CustomEventHandler OnHide;

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            if (!IsRendered) return;
            base.DrawSelf(spriteBatch);
        }

        //GET/SET METHODS

        /// <summary>
        /// Sets the UIElement drawing scale.
        /// </summary>
        /// <param name="scale">Negative values will mirror the UIElement drawing by the x and y vertices</param>
        public void SetScale(float scale)
        {
            Scale = scale;
        }

        /// <summary>
        /// Sets the UIElement drawing scale.
        /// </summary>
        /// <param name="scale">Negative values will mirror the UIElement drawing by the x and y vertices</param>
        public void SetOpacity(float opacity)
        {
            Opacity = opacity;
        }

        /// <summary>
        /// Makes the element render and re-enables all of its functionality
        /// </summary>
        public virtual void Show()
        {
            IsRendered = true;
            OnShow?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// Makes the element not render and disables all of its functionality
        /// </summary>
        public virtual void Hide()
        {
            IsRendered = false;
            OnHide?.Invoke(this, EventArgs.Empty);
        }
    }
}
