
using System.ComponentModel;
using System.Reflection;
using System.Xml;
namespace CoffeeEngine.SceneManagement;

public static class SceneManager
{
    #region Object fields
    
    public static Scene ActiveScene;
    // Change to read te content of the scene folder
    private static readonly Dictionary<int, string> Scenes = new() { };

    #endregion
    

    #region Public Methods

    public static void LoadNextScene()
    {
        if(Scenes.ContainsKey(ActiveScene.Id + 1))
            LoadSceneContent(Scenes[ActiveScene.Id + 1]);
    }

    public static void LoadSceneContent(string sceneName)
    {
        XmlDocument doc = GetSceneXml(sceneName);

        ActiveScene = new Scene(Scenes.Count, sceneName);


        foreach (XmlNode xmlNode in doc.ChildNodes)
        {
            if (xmlNode.Name == "GameObjects")
                CreateContent(xmlNode);
        }
        AfterSceneCreation();

        ActiveScene.Awake();
        ActiveScene.Start();
    }

    #endregion

    #region Private Methods

    private static XmlDocument GetSceneXml(string sceneName)
    {
        string scenePath = Path.Combine(Utils.ContentManager.RootDirectory, "Scenes" ,sceneName + ".xml");

        XmlDocument document = new XmlDocument();
        document.Load(scenePath);

        return document;
    }

    private static void CreateContent(XmlNode pNode)
    {
        foreach (XmlNode gameObject in pNode.ChildNodes)
        {
            GameObject newObject = new();
            newObject.Name = gameObject.Name;

            foreach (XmlNode component in gameObject.ChildNodes)
            {
                Type componentType = Assembly.GetExecutingAssembly().DefinedTypes
                    .FirstOrDefault(x => x.Name == component.Name);
                MethodInfo info =
                    typeof(GameObject).GetMethod("AddComponent", BindingFlags.Instance | BindingFlags.Public);
                MethodInfo genericInfo = info.MakeGenericMethod(componentType);
                Component newComponent = (Component)genericInfo.Invoke(newObject, null);

                foreach (XmlNode xmlValue in component.ChildNodes)
                {
                    FieldInfo field = componentType.GetField(xmlValue.Name);
                    PropertyInfo property = componentType.GetProperty(xmlValue.Name);
                    if (field is null && property is null)
                        continue;

                    Type fieldType = field is null ? property.PropertyType : field.FieldType;

                    Type instanceType = componentType.GetField(xmlValue.Name).FieldType;

                    dynamic typeInstance = Activator.CreateInstance(instanceType);

                    TypedReference reference = new();

                    if (instanceType.IsValueType)
                        reference = __makeref(typeInstance);


                    foreach (XmlAttribute attribute in xmlValue.Attributes)
                    {
                        if (componentType.GetProperty(attribute.Name) != null)
                        {
                            dynamic d = Convert(componentType.GetProperty(attribute.Name).PropertyType,
                                attribute.Value);
                            componentType.GetProperty(attribute.Name).SetValue(newComponent,
                                d);
                        }
                        else if (componentType.GetField(attribute.Name) != null)
                        { 
                            dynamic d = Convert(componentType.GetField(attribute.Name).FieldType, attribute.Value);
                            componentType.GetField(attribute.Name).SetValue(newComponent,
                                d);
                        }
                        else if (componentType.GetField(xmlValue.Name) != null)
                        {
                            FieldInfo instanceField = instanceType.GetField(attribute.Name);

                            object value = Convert(instanceField.FieldType,
                                attribute.Value);

                            if (instanceType.IsClass)
                                instanceField.SetValue(typeInstance, value);
                            else
                                instanceField.SetValueDirect(reference, value);
                            
                            newComponent.GetType().GetField(xmlValue.Name).SetValue(newComponent, typeInstance);
                        }
                    }
                }

                newComponent.transform = newObject.GetComponent<Transform>();
            }

            newObject.GetComponent<SpriteRenderer>()
                .LoadContent(gameObject.ChildNodes[1].ChildNodes[0].Attributes[0].Value);
            ActiveScene.Add(newObject);
        }
    }

    private static void AfterSceneCreation()
    {
        foreach (GameObject go in ActiveScene.GameObjects)
        {
            go.transform = go.GetComponent<Transform>();
        }
    }

    private static dynamic Convert(Type to, dynamic from)
    {
        var converter = TypeDescriptor.GetConverter(to);
        var result = converter.ConvertFrom(null, System.Globalization.CultureInfo.InvariantCulture, from);
        return result;
    }

    #endregion
}