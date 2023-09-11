public class StateBase : IState
{
    public virtual void OnEnter(object owner, params object[] param)
    {

    }

    public virtual void OnUpdate(object owner, params object[] param)
    {

    }

    public virtual void OnExit(object owner, params object[] param)
    {

    }
}

public interface IState
{
    public void OnEnter(object owner, params object[] param);
    public void OnUpdate(object owner, params object[] param);
    public void OnExit(object owner, params object[] param);
}