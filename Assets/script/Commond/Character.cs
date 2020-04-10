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
    public int id;

    public List<Buff> buffList=new List<Buff>();

    public int x;
    public int y;

    public Animation animation;
    public mapPoint point;

    public virtual void StartRound()
    {
        gas += roundGas;        
        roundMove = moves;
        foreach (Buff buff in buffList)
        {
            CalculateBuff(buff);
        }
        if (blood <= 0)
            Dead();
        else if (blood > totalBlood)
            blood = totalBlood;
        if (gas < 0)
            gas = 0;
        else if (gas > totalGas)
            gas = totalGas;
        if (roundMove < 0)
            roundMove = 0;
    }

    public void CalculateBuff(Buff buff)
    {
        Debug.LogError("buff   " + buff.original);
        switch (buff.skillEffectType)
        {
            case SkillEffectType.blood:
                    blood -= buff.original;
                break;
            case SkillEffectType.gas:                
                    gas -= buff.original;
                break;
            case SkillEffectType.moves:
                roundMove -= buff.original;
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
        GameManage.Instance.groundList[x][y].character = this;
        GameManage.Instance.groundList[this.x][this.y].character = null;
        this.x = x;
        this.y = y;
        
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }


    public virtual void Dead() { }
}
