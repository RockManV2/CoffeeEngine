using Microsoft.Xna.Framework;

namespace CoffeeEngine.TempComponents;

public class ScalerBehaviour : MonoBehaviour, IUpdateable
{
    public float Amplitude = 1;
    public float Frequency = 1;

    public void Update()
    {
        float sine = MathF.Sin(Time.time * MathHelper.TwoPi * Frequency);
        transform.Scale = Vector2.One * (sine + 1) / 2 * Amplitude;
    }
}