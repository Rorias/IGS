using UnityEngine;

public class Platform : IPlatform
{
    public IPlatform.PlatformType platformTypes { get; set; }
    public Vector2 position { get; set; }
    public Color color { get; set; }
    public float bounceHeight { get; set; }
    public int jumpCount { get; set; }

    public Platform(float _bHeight, int _jumpCount, Vector2 _pos, Color _color)
    {
        bounceHeight = _bHeight;
        jumpCount = _jumpCount;
        position = _pos;
        color = _color;
    }

    public float Touched()
    {
        jumpCount--;

        if (jumpCount <= 0)
        {
            return 0;
        }

        return bounceHeight;
    }
}
