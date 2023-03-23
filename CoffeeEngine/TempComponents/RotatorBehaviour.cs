namespace CoffeeEngine.TempComponents;

public class RotatorBehaviour : MonoBehaviour, IUpdateable
{
    public float Speed = 1;

    public void Update()
    {
        transform.Rotation += (Speed * 100) * Time.deltaTime;
    }
}