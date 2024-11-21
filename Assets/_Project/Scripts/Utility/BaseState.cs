using UnityEngine;
public abstract class BaseState : MonoBehaviour , IState
{
    public virtual void StateEnter()
    {
        //noop
    }

    public virtual void StateUpdate()
    {
        //noop
    }

    public virtual void StateExit()
    {
        //noop
    }
}
