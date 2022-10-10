public class HighBounceDec : PlatformDecorator
{
    public HighBounceDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.bounceHeight += bounceHeightMod;
        _platform.platformTypes |= IPlatform.PlatformType.highBounce;
        return _platform;
    }
}
