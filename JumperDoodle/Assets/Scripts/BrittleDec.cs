public class BrittleDec : PlatformDecorator
{
    public BrittleDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.jumpCount = jumpCountMod;
        _platform.platformTypes |= IPlatform.PlatformType.brittle;
        return _platform;
    }
}
