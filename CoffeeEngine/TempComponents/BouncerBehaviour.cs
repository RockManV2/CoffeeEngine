using Microsoft.Xna.Framework;

namespace CoffeeEngine.TempComponents;

public class BouncerBehaviour : MonoBehaviour, IUpdateable
{
    public float Amplitude = 1;
    public float Frequency = 1;

    private Vector2 _startPosition;

    public override void Start()
    {
        _startPosition = transform.Position;
    }
    public void Update()
    {
        Vector2 diffrence = new Vector2(
            x: 0,
            y: (Amplitude * 100) * MathF.Sin(MathHelper.TwoPi * Time.time * Frequency)
        );
        
        transform.Position = _startPosition + diffrence;
    }
}