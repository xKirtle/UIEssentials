using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        /// <summary>
        /// Get the curent mouse hovering state.
        /// </summary>
        private UIElement _parent;

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsMouseHovering)
                Main.LocalPlayer.mouseInterface = true;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!IsRendered) return;
            base.Draw(spriteBatch);
        }

        //GET/SET METHODS

        /// <summary>
        /// Sets the UIElement drawing scale.
        /// </summary>
        /// <param name="scale">Negative values will mirror the UIElement drawing by the x and y vertices</param>
        public virtual void SetScale(float scale)
        {
            Scale = scale;
        }

        /// <summary>
        /// Sets the UIElement drawing scale.
        /// </summary>
        /// <param name="scale">Negative values will mirror the UIElement drawing by the x and y vertices</param>
        public virtual void SetOpacity(float opacity)
        {
            Opacity = opacity;
        }

        #region Events
        //Hiding vanilla delegates to return a CustomUIElement rather than a UIElement
        new public delegate void MouseEvent(UIMouseEvent evt, CustomUIElement listeningElement);
        new public delegate void ScrollWheelEvent(UIScrollWheelEvent evt, CustomUIElement listeningElement);
        public delegate void ElementEvent(CustomUIElement affectedElement);


        public event ElementEvent OnShow;
        public event ElementEvent OnHide;

        /// <summary>
        /// Makes the element render and re-enables all of its functionality
        /// </summary>
        public virtual void Show()
        {
            IsRendered = true;
            _parent?.Append(this);

            OnShow?.Invoke(this);
        }

        /// <summary>
        /// Makes the element not render and disables all of its functionality. (still takes up space)
        /// </summary>
        public virtual void Hide()
        {
            IsRendered = false;
            _parent = Parent;
            Remove();

            OnHide?.Invoke(this);
        }

        #endregion
    }
}