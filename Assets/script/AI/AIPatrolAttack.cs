using UnityEngine;
using System.Collections;

public class AIPatrolAttack : AIBase
{

    public override void StartRound()
    {
        if (npc == null)
        {
            npc = GetComponent<NPC>();
        }
        if (patrolPoint == null)
            GetPoint();
        Find();
        if (IsAttack)
        {
            Attack();
            //Debug.LogError(transform.name + "   Attack");
        }

        else
        {
            Patrol();
            //Debug.LogError(transform.name + "   Patrol");
        }


    }

    public override bool Find()
    {
        if (GameTools.GetDistance(npc.GetPosition()) >= 7)
        {
            IsAttack = false;
            return false;
        }
        if (GameTools.GetDistance(npc.GetPosition()) <= 4)
        {
            IsAttack = true;
            return true;
        }
        return false;
    }

    public override void Attack()
    {
        if (npc.Attack())
            return;
        npc.Goto(GameManage.Instance.role.GetPosition());
        npc.Attack();
    }

    public override void Patrol()
    {
        if(npc.GetPosition()==patrolPoint)
        {
            patrolPoint = oldPoint;
            oldPoint = npc.GetPosition();
            GetPPoint();
        }
        Debug.LogError("AIPatrolAttack    Patrol");
        npc.Goto(patrolPoint);
    }

    public override void GetPoint()
    {
        oldPoint = npc.GetPosition();
        patrolPoint = oldPoint;
        patrolPoint.x += Random.Range(-10, 10);
        if (patrolPoint.x < 0)
            patrolPoint.x = 0;
        if (patrolPoint.x >= GameManage.row)
            patrolPoint.x = GameManage.row - 1;
        patrolPoint.y += Random.Range(-10, 10);
        if (patrolPoint.y < 0)
            patrolPoint.y = 0;
        if (patrolPoint.y >= GameManage.row)
            patrolPoint.y = GameManage.row - 1;
        GetPPoint();
    }

    public void GetPPoint()
    {
        int count = 0;
        while (true)
        {
            if (GameManage.Instance.mapPoints[(int)patrolPoint.x][(int)patrolPoint.y].vaule == 1)
            {
                if (count % 2 == 0)
                    patrolPoint.x += 1;
                else
                    patrolPoint.y += 1;
                count++;
                continue;
            }
            return;
        }
    }
}
