
using Microsoft.Xna.Framework;
namespace CoffeeEngine;

public class ColorShifter : MonoBehaviour, IUpdateable
{

    private SpriteRenderer _spriteRenderer;
    public float Speed = 1;
    private float _hue;


    #region Framework Methods

    public override void Start()
    {
        _spriteRenderer = GameObject.GetComponent<SpriteRenderer>();
    }

    public void Update()
    {
        _hue += Time.deltaTime * Speed;
        _hue %= 1.0f;
        _spriteRenderer.Color = HslToRgb(_hue, 1.0f, 0.5f);
    }

    #endregion

    #region Private Methods

    private Color HslToRgb(float hue, float saturation, float lightness)
    {
        float red, green, blue;

        if (saturation == 0f)
        {
            red = green = blue = lightness;
        }
        else
        {
            float q = lightness < 0.5f ? lightness * (1f + saturation) : lightness + saturation - lightness * saturation;
            float p = 2f * lightness - q;
            red = HueToRgb(p, q, hue + 1f / 3f);
            green = HueToRgb(p, q, hue);
            blue = HueToRgb(p, q, hue - 1f / 3f);
        }

        return new Color(To255(red), To255(green), To255(blue));
    }
	
    private float HueToRgb(float p, float q, float t)
    {
        if (t < 0f) t += 1f;
        if (t > 1f) t -= 1f;
        if (t < 1f / 6f) return p + (q - p) * 6f * t;
        if (t < 1f / 2f) return q;
        if (t < 2f / 3f) return p + (q - p) * (2f / 3f - t) * 6f;
        return p;
    }
	
    private int To255(float pValue)
    {
        return (int) Math.Min(255, 256 * pValue);
    }

    #endregion
}