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
    public bool Active { get; set; } = true;
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
        T newCompenent = new() { GameObject = this };
        _components.Add(newCompenent);
        if (newCompenent is IUpdateable updateableComponent)
            _updateableComponents.Add(updateableComponent);

        if (newCompenent is IDrawable drawableComponent)
            _drawableComponents.Add(drawableComponent);

        if (newCompenent is BoxCollider boxCollider)
            SceneManager.ActiveScene.Collidables.Add(boxCollider);

        return newCompenent;
    }

    public T GetComponent<T>() where T : Component
    {
        foreach (var component in _components)
            if (component is T typedComponent)
                return typedComponent;

        return null;
    }


    public void Destroy() => SceneManager.ActiveScene.Destroy(this);

    public void DestroyImmediate() => SceneManager.ActiveScene.DestroyImmediate(this);

    #endregion

    #region Lifetime methods
    
    public void Awake() => _components.ForEach(component => component.Awake());

    public void Start() => _components.ForEach(component => component.Start());

    public void Update() => _updateableComponents.ForEach(updateableComponent => updateableComponent.Update());

    public void Draw(SpriteBatch spriteBatch) => _drawableComponents.ForEach(drawableComponent => drawableComponent.Draw(spriteBatch));

    public void SignalCollision(CollisionInfo collisionInfo) => _components.ForEach(component => component.OnCollisionStay(collisionInfo));

    #endregion
}
