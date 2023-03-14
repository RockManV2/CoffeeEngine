namespace CoffeeEngine.UI;

public class ButtonReleasedState : ButtonStateBase
{
    public event ButtonEvent OnButtonReleased;

    public override void EnterState()
    {
        OnButtonReleased?.Invoke();

        Button.SwitchState(Button.ButtonDefaultState);
    }

    public ButtonReleasedState(Button pButton, ButtonEvent pOnButtonReleased)
    {
        Button = pButton;
        OnButtonReleased = pOnButtonReleased;
    }
}