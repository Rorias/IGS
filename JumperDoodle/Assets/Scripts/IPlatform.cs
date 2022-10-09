using UnityEngine;

public interface IPlatform
{
    public float bounceHeight { get; set; }
    public int jumpCount { get; set; }
    public Vector2 position { get; set; }
    public Color color { get; set; }
    public float Touched();
}
