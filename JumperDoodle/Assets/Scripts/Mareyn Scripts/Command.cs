using UnityEngine;

public abstract class Command
{
    public abstract void Execute(Animator anim);
}

public class MoveLeft : Command
{
    public override void Execute(Animator anim)
    {
        anim.SetTrigger("movingLeft");
    }
}

public class MoveRight : Command
{
    public override void Execute(Animator anim)
    {
        anim.SetTrigger("movingRight");
    }
}
