using Microsoft.Xna.Framework.Graphics;
using CoffeeEngine.SceneManagement;
using CoffeeEngine.Physics;

namespace CoffeeEngine;

public class GameObject
{
    #region Object Fields

    public Transform transform;

    private readonly List<Component> _components = new();
    private readonly List<IUpdateable> _updateableComponents = new();
    private readonly List<IDrawable> _drawableComponents = new();

    #endregion

    #region Object Properties

    public string Name { get; set; } = "New GameObject";

    private bool _active = true;

    public bool Active
    {
        get => _active;
        set
        {
            if (_active && !value)
                _components.ForEach(component => component.OnDisable());
            else if (!_active && value)
                _components.ForEach(component => component.OnEnable());

            _active = value;
        }
    }

    public string Tag { get; set; } = "";

    #endregion

    #region Constructors

    public GameObject()
    {
    }

    #endregion


    #region Static Methods

    public static GameObject Find(string name)
    {
        foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            if (gameObject.Name == name)
                return gameObject;
        return null;
    }

    public static GameObject Instantiate()
    {
        GameObject newObject = new();
        newObject.transform = new();

        SceneManager.ActiveScene.Add(newObject);

        return newObject;
    }

    public static List<GameObject> FindObjectsWithTag(string pTag)
    {
        List<GameObject> foundObjects = new();
        foreach (GameObject gameObject in SceneManager.ActiveScene.GameObjects)
            if (gameObject.Tag == pTag)
                foundObjects.Add(gameObject);

        if (foundObjects.Count > 0)
            return foundObjects;
        else
            return null;
    }

    #endregion

    #region Public Methods

    public T AddComponent<T>() where T : Component, new()
    {
        T newComponent = new() { GameObject = this };
        _components.Add(newComponent);
        if (newComponent is IUpdateable updateableComponent)
            _updateableComponents.Add(updateableComponent);

        if (newComponent is IDrawable drawableComponent)
            _drawableComponents.Add(drawableComponent);

        if (newComponent is BoxCollider boxCollider)
            SceneManager.ActiveScene.Collidables.Add(boxCollider);

        return newComponent;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var component in _components)
            if (component is T typedComponent)
                return typedComponent;

        return null;
    }
    
    public List<T> GetComponents<T>() where T : Component
    {
        List<T> result = new();
        foreach (var component in _components)
            if (component is T typedComponent)
                result.Add(typedComponent);

        return result.Count > 0 ? result : null;
    }


    public void Destroy() => SceneManager.ActiveScene.Destroy(this);

    public void DestroyImmediate() => SceneManager.ActiveScene.DestroyImmediate(this);

    #endregion

    #region Lifetime methods

    public void Awake()
    {
        if (!_active) return;
        _components.ForEach(component => component.Awake());
    }

    public void Start()
    {
        if (!_active) return;
        _components.ForEach(component => component.Start());
    }

    public void Update()
    {
        if (!_active) return;
        _updateableComponents.ForEach(updateableComponent => updateableComponent.Update());
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        if(!_active) return;
        _drawableComponents.ForEach(drawableComponent => drawableComponent.Draw(spriteBatch));
    }

    public void SignalCollision(CollisionInfo collisionInfo)
    {
        foreach (var component in _components)
        {
            if (component is MonoBehaviour monoBehaviour)
                monoBehaviour.OnCollisionStay(collisionInfo);
        }
    }

    #endregion
}