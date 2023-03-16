﻿using Microsoft.Xna.Framework.Graphics;

using CoffeeEngine.Physics;
namespace CoffeeEngine.SceneManagement;

public class Scene
{
    #region Public Fields

    public readonly string Name;
    public readonly int Id;
    public readonly List<GameObject> GameObjects = new();
    public readonly List<BoxCollider> Collidables = new();

    private readonly List<GameObject> _destroyObjects = new();

    #endregion


    #region Constructors

    public Scene(int sceneId, string sceneName)
    {
        Name = sceneName;
        Id = sceneId;
    }

    #endregion

    #region Public Methods

    public void Add(GameObject pObject) => GameObjects.Add(pObject);

    public void Destroy(GameObject destroyObject) => _destroyObjects.Add(destroyObject);

    public void DestroyImmediate(GameObject destroyObject)
    {
        GameObjects.Remove(destroyObject);
        Collidables.Remove(destroyObject.GetComponent<BoxCollider>());
    }

    #endregion

    #region Lifetime methods

    public void Awake() => GameObjects.ForEach(gameObject => gameObject.Awake());

    public void Start() => GameObjects.ForEach(gameObject => gameObject.Start());

    public void UpdateScene()
    {
        GameObjects.ForEach(gameObject => gameObject.Update());

        foreach (GameObject obj in _destroyObjects)
        {
            GameObjects.Remove(obj);
            Collidables.Remove(obj.GetComponent<BoxCollider>());
        }
    }

    public void DrawScene(SpriteBatch pSpriteBatch) => GameObjects.ForEach(gameObject => gameObject.Draw(pSpriteBatch));

    public void CheckCollisions()
    {
        for (int i = 0; i < Collidables.Count; i++)
        {
            for (int j = i + 1; j < Collidables.Count; j++)
            {
                if (Collidables[i].CheckCollision(Collidables[j], out CollisionInfo collisionInfo))
                {
                    collisionInfo.Other = Collidables[j];
                    Collidables[i].GameObject.SignalCollision(collisionInfo);
                    collisionInfo.Other = Collidables[i];
                    Collidables[j].GameObject.SignalCollision(collisionInfo);
                }
            }
        }
    }

    #endregion
}