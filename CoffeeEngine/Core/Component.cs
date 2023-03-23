
namespace CoffeeEngine;

public abstract class Component
{
    private bool _enabled = true;
    public bool Enabled
    {
        get => _enabled;
        set
        {
            if (_enabled && !value)
                OnDisable();
            else if (!_enabled && value)
                OnEnable();

            _enabled = value;
        }
    }
    
    public GameObject GameObject;
    public Transform transform;
    public virtual void Awake() { }
    public virtual void Start() { }

    public virtual void OnEnable() { }
    public virtual void OnDisable() { }
}