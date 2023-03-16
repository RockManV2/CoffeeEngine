
using Microsoft.Xna.Framework.Input;
namespace CoffeeEngine.UI;

public class ButtonHoldState : ButtonStateBase
{
    private readonly ButtonEvent _onButtonHold;

    public override void Update()
    {
        _onButtonHold?.Invoke();


        if (Button.MouseState.LeftButton == ButtonState.Released && Button.Hitbox.Contains(Button.MousePosition))
        {
            Button.SwitchState(Button.ButtonReleasedState);
            return;
        }
        else if (Button.MouseState.LeftButton == ButtonState.Released)
        {
            Button.SwitchState(Button.ButtonDefaultState);
        }
    }

    public ButtonHoldState(Button pButton, ButtonEvent pOnButtonHold)
    {
        Button = pButton;
        _onButtonHold = pOnButtonHold;
    }
}