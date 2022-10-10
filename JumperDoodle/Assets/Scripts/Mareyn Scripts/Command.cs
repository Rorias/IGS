using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator _anim);
}

public class MoveLeft : Command
{
    public override void Execute(Animator _anim)
    {
        _anim.SetTrigger("movingLeft");
    }
}

public class MoveRight : Command
{
    public override void Execute(Animator _anim)
    {
        _anim.SetTrigger("movingRight");
    }
}
