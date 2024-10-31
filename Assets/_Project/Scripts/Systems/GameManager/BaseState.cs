public abstract class BaseState : IState
{
    public virtual void OnEnter()
    {
        //noop
    }

    public virtual void Update()
    {
        //noop
    }

    public virtual void OnExit()
    {
        //noop
    }
}
