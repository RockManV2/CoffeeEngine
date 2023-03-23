using CoffeeEngine.UI;
using CoffeeEngine.SceneManagement;

namespace CoffeeEngine.TempComponents;

public class GameSceneSwitcher : MonoBehaviour
{
    public override void Awake()
    {
        GameObject.Find("NextButton").GetComponent<Button>().OnButtonPressed += SceneManager.LoadNextScene;
        GameObject.Find("PreviousButton").GetComponent<Button>().OnButtonPressed += SceneManager.LoadPreviousScene;

        GameObject.Find("NextButton").GetComponent<Button>().OnButtonHover += () => { Utils.Print(Time.time); };
    }
}