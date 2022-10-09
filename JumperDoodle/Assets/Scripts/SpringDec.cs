using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringDec : PlatformDecorator
{
    public SpringDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.bounceHeight += bounceHeightMod;
        return _platform;
    }
}
