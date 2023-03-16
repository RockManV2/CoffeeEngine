
namespace CoffeeEngine.UI;

public class ButtonPressedState : ButtonStateBase
{
    public event ButtonEvent OnButtonPressed;

    public override void EnterState()
    {
        if(OnButtonPressed == null)
            Utils.Print("Buttonevent is null");
        OnButtonPressed?.Invoke();
        Button.SwitchState(Button.ButtonHoldState);
    }

    public ButtonPressedState(Button pButton, ButtonEvent pOnButtonPressed)
    {
        Button = pButton;
        OnButtonPressed = pOnButtonPressed;
    }
}