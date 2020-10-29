using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.GameInput;
using UIEssentials.UI.Elements;

namespace Terraria.UI
{
	public class CustomUserInterface
	{
		public static CustomUserInterface ActiveInstance = new CustomUserInterface();
		private List<UIState> _history = new List<UIState>();
		public Vector2 MousePosition;
		private bool _wasMouseDown;
		private CustomUIElement _lastElementHover;
		private CustomUIElement _lastElementDown;
		private CustomUIElement _lastElementClicked;
		private double _lastMouseDownTime;
		private double _clickDisabledTimeRemaining;
		private bool _isStateDirty;
		public bool IsVisible;
		private UIState _currentState;

		public void ResetLasts()
		{
			if (_lastElementHover != null)
			{
				_lastElementHover.MouseOut(new UIMouseEvent(_lastElementHover, MousePosition));
			}
			_lastElementHover = null;
			_lastElementDown = null;
			_lastElementClicked = null;
		}

		public void EscapeElements()
		{
			_lastElementHover = null;
		}

		public UIState CurrentState
		{
			get
			{
				return _currentState;
			}
		}

		public CustomUserInterface()
		{
			ActiveInstance = this;
		}

		public void Use()
		{
			if (ActiveInstance != this)
			{
				ActiveInstance = this;
				Recalculate();
				return;
			}
			ActiveInstance = this;
		}

		private void ResetState()
		{
			GetMousePosition();
			_wasMouseDown = Main.mouseLeft;
			if (_lastElementHover != null)
			{
				_lastElementHover.MouseOut(new UIMouseEvent(_lastElementHover, MousePosition));
			}
			_lastElementHover = null;
			_lastElementDown = null;
			_lastElementClicked = null;
			_lastMouseDownTime = 0.0;
			_clickDisabledTimeRemaining = Math.Max(_clickDisabledTimeRemaining, 200.0);
		}

		private void GetMousePosition()
		{
			MousePosition = new Vector2((float)Main.mouseX, (float)Main.mouseY);
		}

		public void Update(GameTime time)
		{
			if (_currentState != null)
			{
				GetMousePosition();
				bool flag = Main.mouseLeft && Main.hasFocus;
				CustomUIElement uielement = Main.hasFocus ? _currentState.GetElementAt(MousePosition) as CustomUIElement : null;
				_clickDisabledTimeRemaining = Math.Max(0.0, _clickDisabledTimeRemaining - time.ElapsedGameTime.TotalMilliseconds);
				bool flag2 = _clickDisabledTimeRemaining > 0.0;
				if (uielement != _lastElementHover)
				{
					if (_lastElementHover != null)
					{
						_lastElementHover.MouseOut(new UIMouseEvent(_lastElementHover, MousePosition));
					}
					if (uielement != null)
					{
						uielement.MouseOver(new UIMouseEvent(uielement, MousePosition));
					}
					_lastElementHover = uielement;
				}
				if (flag && !_wasMouseDown && uielement != null && !flag2)
				{
					_lastElementDown = uielement;
					uielement.MouseDown(new UIMouseEvent(uielement, MousePosition));
					if (_lastElementClicked == uielement && time.TotalGameTime.TotalMilliseconds - _lastMouseDownTime < 500.0)
					{
						uielement.DoubleClick(new UIMouseEvent(uielement, MousePosition));
						_lastElementClicked = null;
					}
					_lastMouseDownTime = time.TotalGameTime.TotalMilliseconds;
				}
				else if (!flag && _wasMouseDown && _lastElementDown != null && !flag2)
				{
					CustomUIElement lastElementDown = _lastElementDown;
					if (lastElementDown.ContainsPoint(MousePosition))
					{
						lastElementDown.Click(new UIMouseEvent(lastElementDown, MousePosition));
						_lastElementClicked = _lastElementDown;
					}
					lastElementDown.MouseUp(new UIMouseEvent(lastElementDown, MousePosition));
					_lastElementDown = null;
				}
				if (PlayerInput.ScrollWheelDeltaForUI != 0)
				{
					if (uielement != null)
					{
						uielement.ScrollWheel(new UIScrollWheelEvent(uielement, MousePosition, PlayerInput.ScrollWheelDeltaForUI));
					}
					PlayerInput.ScrollWheelDeltaForUI = 0;
				}
				_wasMouseDown = flag;
				if (_currentState != null)
				{
					_currentState.Update(time);
				}
			}
		}

		public void Draw(SpriteBatch spriteBatch, GameTime time)
		{
			Use();
			if (_currentState != null)
			{
				if (_isStateDirty)
				{
					_currentState.Recalculate();
					_isStateDirty = false;
				}
				_currentState.Draw(spriteBatch);
			}
		}

		public void SetState(UIState state)
		{
			if (state == _currentState)
			{
				return;
			}
			if (state != null)
			{
				AddToHistory(state);
			}
			if (_currentState != null)
			{
				if (_lastElementHover != null)
				{
					_lastElementHover.MouseOut(new UIMouseEvent(_lastElementHover, MousePosition));
				}
				_currentState.Deactivate();
			}
			_currentState = state;
			ResetState();
			if (state != null)
			{
				_isStateDirty = true;
				state.Activate();
				state.Recalculate();
			}
		}

		public void GoBack()
		{
			if (_history.Count < 2)
			{
				return;
			}
			UIState state = _history[_history.Count - 2];
			_history.RemoveRange(_history.Count - 2, 2);
			SetState(state);
		}

		private void AddToHistory(UIState state)
		{
			_history.Add(state);
			if (_history.Count > 32)
			{
				_history.RemoveRange(0, 4);
			}
		}

		public void Recalculate()
		{
			if (_currentState != null)
			{
				_currentState.Recalculate();
			}
		}

		public CalculatedStyle GetDimensions()
		{
			Vector2 originalScreenSize = new Vector2(Main.screenWidth, Main.screenHeight);
			return new CalculatedStyle(0f, 0f, originalScreenSize.X / Main.UIScale, originalScreenSize.Y / Main.UIScale);
		}

		internal void RefreshState()
		{
			if (_currentState != null)
			{
				_currentState.Deactivate();
			}
			ResetState();
			_currentState.Activate();
			_currentState.Recalculate();
		}

		public bool IsElementUnderMouse()
		{
			return IsVisible && _lastElementHover != null && !(_lastElementHover is UIState);
		}
	}
}
