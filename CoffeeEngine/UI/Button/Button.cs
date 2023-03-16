
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using static CoffeeEngine.UI.ButtonStateBase;
namespace CoffeeEngine.UI;

public class Button : Component, IUpdateable
{
    internal Rectangle Hitbox;
    internal ButtonDefaultState ButtonDefaultState { get; private set; }

    internal ButtonHoldState ButtonHoldState { get; private set; }
    internal event ButtonEvent OnButtonHold;

    internal ButtonPressedState ButtonPressedState { get; private set; }
    internal event ButtonEvent OnButtonPressed;
    internal ButtonHoverState ButtonHoverState { get; private set; }
    internal event ButtonEvent OnButtonHover;
    internal ButtonReleasedState ButtonReleasedState { get; private set; }
    internal event ButtonEvent OnButtonReleased;

    private ButtonStateBase _currentButtonState;

    internal MouseState MouseState;
    internal Vector2 MousePosition;

    public override void Start()
    {
        ButtonDefaultState = new(this);
        ButtonHoldState = new(this, OnButtonHold);
        ButtonPressedState = new(this, OnButtonPressed);
        ButtonHoverState = new(this, OnButtonHover);
        ButtonReleasedState = new(this, OnButtonReleased);
        
        _currentButtonState = ButtonDefaultState;
        Hitbox = GameObject.GetComponent<SpriteRenderer>().Texture.Bounds;
    }

    public void Update()
    {
        MouseState = Mouse.GetState();
        MousePosition = new Vector2(MouseState.X, MouseState.Y);
        
        Hitbox.X = (int)GameObject.transform.Position.X;
        Hitbox.Y = (int)GameObject.transform.Position.Y;
        _currentButtonState.Update();
    }

    internal void SwitchState(ButtonStateBase pNewState)
    {
        _currentButtonState.ExitState();
        _currentButtonState = pNewState;
        _currentButtonState.EnterState();
    }
}