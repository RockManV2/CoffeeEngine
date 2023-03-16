
using Microsoft.Xna.Framework.Input;
namespace CoffeeEngine.UI;

public class ButtonHoverState : ButtonStateBase
{
    private event ButtonEvent OnButtonHover;

    public override void Update()
    {
        OnButtonHover?.Invoke();
        if (!Button.Hitbox.Contains(Button.MousePosition))
            Button.SwitchState(Button.ButtonDefaultState);

        if (Button.MouseState.LeftButton == ButtonState.Pressed)
            Button.SwitchState(Button.ButtonPressedState);
    }

    public ButtonHoverState(Button pButton, ButtonEvent pOnButtonHover)
    {
        Button = pButton;
        OnButtonHover = pOnButtonHover;
    }
}