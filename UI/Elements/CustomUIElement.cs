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
        new public bool IsMouseHovering { get; private set; }

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

        new public event MouseEvent OnMouseDown;
        new public event MouseEvent OnMouseUp;
        new public event MouseEvent OnClick;
        new public event MouseEvent OnMouseOver;
        new public event MouseEvent OnMouseOut;
        new public event MouseEvent OnDoubleClick;
        new public event ScrollWheelEvent OnScrollWheel;

        new public virtual void MouseDown(UIMouseEvent evt)
        {
            if (!IsRendered) return;

            OnMouseDown?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.MouseDown(evt);
            }
        }

        new public virtual void MouseUp(UIMouseEvent evt)
        {
            if (!IsRendered) return;

            OnMouseUp?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.MouseUp(evt);
            }
        }

        new public virtual void MouseOver(UIMouseEvent evt)
        {
            IsMouseHovering = true;
            if (!IsRendered) return;

            OnMouseOver?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.MouseOver(evt);
            }
        }

        new public virtual void MouseOut(UIMouseEvent evt)
        {
            IsMouseHovering = false;
            if (!IsRendered) return;

            OnMouseOut?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.MouseOut(evt);
            }
        }

        new public virtual void Click(UIMouseEvent evt)
        {
            if (!IsRendered) return;

            OnClick?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.Click(evt);
            }
        }

        new public virtual void DoubleClick(UIMouseEvent evt)
        {
            if (!IsRendered) return;

            OnDoubleClick?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.DoubleClick(evt);
            }
        }

        new public virtual void ScrollWheel(UIScrollWheelEvent evt)
        {
            if (!IsRendered) return;

            OnScrollWheel?.Invoke(evt, this);
            if (Parent != null)
            {
                Parent.ScrollWheel(evt);
            }
        }


        public event ElementEvent OnShow;
        public event ElementEvent OnHide;

        /// <summary>
        /// Makes the element render and re-enables all of its functionality
        /// </summary>
        public virtual void Show()
        {
            IsRendered = true;
            OnShow?.Invoke(this);
        }

        /// <summary>
        /// Makes the element not render and disables all of its functionality. (still takes up space)
        /// </summary>
        public virtual void Hide()
        {
            IsRendered = false;
            OnHide?.Invoke(this);
        }

        #endregion
    }
}