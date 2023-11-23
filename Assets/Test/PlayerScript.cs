using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private FSM fSM;

    private void Awake()
    {
        fSM = new FSM(this);
        fSM.ChangeState<TestState>();
    }

    private void Update()
    {
        fSM.Update();
    }

    //�����ӿ�
    public void ChangeState<T>() where T : StateBase, new()
    {
        fSM.ChangeState<T>();
    }
}
