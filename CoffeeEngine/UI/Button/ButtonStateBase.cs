
namespace CoffeeEngine.UI;

    public abstract class ButtonStateBase
    {
        protected Button Button;
        public delegate void ButtonEvent();

        public virtual void Update(){ }
        public virtual void EnterState(){ }
        public virtual void ExitState(){ }
    }

