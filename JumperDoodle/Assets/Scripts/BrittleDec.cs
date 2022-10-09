using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrittleDec : PlatformDecorator
{
    public BrittleDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.jumpCount = jumpCountMod;
        return _platform;
    }
}
