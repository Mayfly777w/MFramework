using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : StateBase
{
    private PlayerScript player;

    public override void OnEnter(object owner, params object[] param)
    {
        player = owner as PlayerScript;
    }

    public override void OnUpdate(object owner, params object[] param)
    {

    }

    public override void OnExit(object owner, params object[] param)
    {

    }
}
