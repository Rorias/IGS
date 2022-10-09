public class MovingDec : PlatformDecorator
{
    public MovingDec(float _bHeight, int _jumpCount) : base(_bHeight, _jumpCount) { }

    public override IPlatform Decorate(IPlatform _platform)
    {
        throw new System.NotImplementedException();
    }
}
