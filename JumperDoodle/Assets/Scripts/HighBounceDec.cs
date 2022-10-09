using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighBounceDec : PlatformDecorator
{
    public HighBounceDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.bounceHeight += bounceHeightMod;
        return _platform;
    }
}
