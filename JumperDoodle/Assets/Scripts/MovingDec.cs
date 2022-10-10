public class MovingDec : PlatformDecorator
{
    public MovingDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        _platform.platformTypes |= IPlatform.PlatformType.moving;
        return _platform;
    }
}
