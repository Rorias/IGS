using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlatformDecorator
{
    public float bounceHeightMod { get; set; }
    public int jumpCountMod { get; set; }

    public PlatformDecorator(float _bHeight, int _jumpCount)
    {
        bounceHeightMod = _bHeight;
        jumpCountMod = _jumpCount;
    }

    public abstract IPlatform Decorate(IPlatform _platform);
}
