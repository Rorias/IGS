using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenDec : PlatformDecorator
{
    public BrokenDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.jumpCount = jumpCountMod;
        return _platform;
    }
}
