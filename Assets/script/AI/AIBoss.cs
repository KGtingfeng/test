using UnityEngine;
using UnityEditor;

public class AIBoss : AIBase
{
    public override void StartRound()
    {
        if (npc == null)
        {
            npc = GetComponent<NPC>();
        }
        if (IsAttack)
        {
            Attack();
        }       
    }

    public override void Attack()
    {
        if (npc.Attack())
            return;
        npc.Goto(GameManage.Instance.role.GetPosition());
        npc.Attack();
    }
}