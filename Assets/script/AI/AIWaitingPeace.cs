﻿using UnityEngine;
using System.Collections;

public class AIWaitingPeace : AIBase
{
    public override void StartRound()
    {
        if (npc == null)
        {
            npc = GetComponent<NPC>();
        }
        Find();
        if (IsAttack)
            Attack();
    }

    public override bool Find()
    {
        if (GameTools.GetDistance(npc.GetPosition()) >= 7)
            IsAttack = false;        
        return false;
    }

    public override void Attack()
    {
        if (npc.Attack())
            return;
        npc.Goto(GameManage.Instance.role.GetPosition());
        npc.Attack();
    }

}
