using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    public static string SKILLPATH = "GameObject/Skill/";
    /// <summary>
    /// 速度 
    /// </summary>
    public int speed;
    /// <summary>
    /// 体力 
    /// </summary>
   public int strength;
    /// <summary>
    /// 精力 
    /// </summary>
   public int energy;
    /// <summary>
    /// 等级
    /// </summary>
    public int level;
    /// <summary>
    /// 血量
    /// </summary>
    public int blood;
    /// <summary>
    /// 总血量 
    /// </summary>
    public int totalBlood;
    /// <summary>
    /// 行动力 速度/3+装备+初始
    /// </summary>
    public int moves;
    public int roundMove;
    /// <summary>
    /// 气 释放技能的能量 每回合获得同精力
    /// </summary>
    public int gas;
    /// <summary>
    /// 每回合得气
    /// </summary>
    public int roundGas;
    /// <summary>
    /// 气最高存储数 精力+装备+初始
    /// </summary>
    public int totalGas;
    /// <summary>
    /// 经验
    /// </summary>
    public int EXP;

    public bool IsPlayer;

    public int skillNum=0;

    public List<Buff> buffList;

    public int x;
    public int y;

    public Animation animation;
    public mapPoint point;

    public virtual void StartRound()
    {
        if ((gas + roundGas) > totalGas)
        {
            gas = totalGas;
        }
        roundMove = moves;
        foreach (Buff buff in buffList)
        {
            CalculateBuff(buff);
        }
    }

    public void CalculateBuff(Buff buff)
    {
        switch (buff.skillEffectType)
        {
            case SkillEffectType.blood:
                if (blood - buff.original < 0)
                    blood = 0;
                else
                    blood -= buff.original;
                break;
            case SkillEffectType.gas:
                if (gas - buff.original < 0)
                    gas = 0;
                else
                    gas -= buff.original;
                break;
            case SkillEffectType.moves:
                if (moves - buff.original < 0)
                    moves = 0;
                else
                    moves -= buff.original;
                break;
        }
        if (buff.times <= 1)
            buffList.Remove(buff);
        else
            buff.times--;
    }

    public virtual void SetPosition(int x, int y)
    {
        GameManage.Instance.mapPoints[x][y].vaule = 1;
        GameManage.Instance.mapPoints[this.x][this.y].vaule = 0;
        this.x = x;
        this.y = y;       
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }


    public virtual void Dead() { }
}
