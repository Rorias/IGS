using UnityEngine;

public interface IPlatform
{
    public enum PlatformType { normal, highBounce, brittle, broken, spring, moving };
    public float bounceHeight { get; set; }
    public int jumpCount { get; set; }
    public Vector2 position { get; set; }
    public Color color { get; set; }
    public PlatformType platformTypes { get; set; }
    public float Touched();
}
