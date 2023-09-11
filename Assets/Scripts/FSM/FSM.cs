using System.Collections.Generic;

/// <summary>
/// 有限状态机类
/// </summary>
public class FSM
{
    private object owner;
    private Dictionary<string, IState> stateDic;
    private IState currentState;

    public FSM(object owner)
    {
        this.owner = owner;
        stateDic = new Dictionary<string, IState>();
    }

    public void AddState(IState state)
    {
        string name = state.GetType().Name;
        if (stateDic.ContainsKey(name)) return;

        stateDic.Add(name, state);
    }

    public void RemoveState(string name)
    {
        if (!stateDic.ContainsKey(name)) return;

        stateDic.Remove(name);
    }

    public void RemoveState(IState state)
    {
        string name = state.GetType().Name;
        if (!stateDic.ContainsKey(name)) return;

        stateDic.Remove(name);
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.OnUpdate(owner);
        }
    }

    public void ChangeState<T>() where T : StateBase, new()
    {
        if (currentState != null)
        {
            currentState.OnExit(owner);
        }

        string name = typeof(T).Name;

        if (stateDic.ContainsKey(name))
        {
            currentState = stateDic[name];
        }
        else
        {
            IState state = new T();
            this.AddState(state);
            currentState = state;
        }

        currentState.OnEnter(owner);
    }
}
