using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase : MonoBehaviour
{
    public NPC npc;
    public bool IsAttack;
    public Vector2 patrolPoint;
    public Vector2 oldPoint;
    public abstract void StartRound();
    public abstract void Attack();
    public virtual bool Find() { return false; }
    public virtual void Patrol() { }
    public virtual void GetPoint() { }
}
