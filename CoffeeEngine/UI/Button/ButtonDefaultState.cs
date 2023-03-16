
namespace CoffeeEngine.UI;

public class ButtonDefaultState : ButtonStateBase
{
    public override void Update()
    {
        if (Button.Hitbox.Contains(Button.MousePosition))
            Button.SwitchState(Button.ButtonHoverState);
    }

    public ButtonDefaultState(Button pButton)
    {
        Button = pButton;
    }
}