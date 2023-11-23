using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState2 : StateBase
{
    private PlayerScript player;

    public override void OnEnter(object owner, params object[] param)
    {
        Debug.Log("OnEnter_2");
        player = owner as PlayerScript;
    }

    public override void OnUpdate(object owner, params object[] param)
    {
        Debug.Log("OnUpdate_2");

        if (Input.GetKeyDown(KeyCode.C))
        {
            player.ChangeState<TestState>();
        }
    }

    public override void OnExit(object owner, params object[] param)
    {
        Debug.Log("OnExit_2");
    }
}
