using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

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
    /// 总血量 体力*100+装备
    /// </summary>
    public int totalBlood;
    /// <summary>
    /// 行动力 速度/3+装备
    /// </summary>
    public int moves;
    /// <summary>
    /// 气 释放技能的能量 每回合获得同精力
    /// </summary>
    public int gas;
    /// <summary>
    /// 气最高存储数 精力*2+装备
    /// </summary>
    public int totalGas;
    /// <summary>
    /// 经验
    /// </summary>
    public int EXP;

    public bool IsPlayer;

    public int skillNum=0;

    List<Buff> buffList;
    List<SkillConf> skillList;
    public int x;
    public int y;

    public  Animation animation;
    mapPoint point;

    public void StartRound()
    {
        CalculateBuff();
    }

    void CalculateBuff()
    {
        foreach(Buff buff in buffList)
        {
            switch (buff.skillEffectType)
            {
                case SkillEffectType.blood:
                    if (blood + (int)GameTools.CalculateDamage(this,buff) < 0)
                        blood = 0;
                    else
                        blood += (int)GameTools.CalculateDamage(this, buff);
                    break;
                case SkillEffectType.gas:
                    if (gas + (int)GameTools.CalculateDamage(this, buff) < 0)
                        gas = 0;
                    else
                        gas += (int)GameTools.CalculateDamage(this, buff);
                    break;
                case SkillEffectType.moves:
                    if (moves + (int)GameTools.CalculateDamage(this, buff) < 0)
                        moves = 0;
                    else
                        moves += (int)GameTools.CalculateDamage(this, buff);
                    break;
            }
            if (buff.times <= 1)
                buffList.Remove(buff);
            else
                buff.times--;
        }
    }

    public void SetPosition(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(x, y);
    }

    public void Goto(RaycastHit hit)
    {
        StartCoroutine(GetFinish(hit));
    }

    IEnumerator GetFinish(RaycastHit hit)
    {
        yield return 0;
        if (hit.transform.GetComponent<Ground>())
        {
            Vector2 pos = GetPosition();
            Ground finish = hit.transform.GetComponent<Ground>();
            List<mapPoint> road = AStar.find(GameManage.Instance.groundList[(int)pos.x][(int)pos.y], finish);
            if (road != null)
            {
                road.Reverse();
                StartCoroutine(GoToFinish(road));
            }
            else
            {
                Debug.LogError("不可走");
            }

        }
    }

    IEnumerator GoToFinish(List<mapPoint> road)
    {
        animation.Play("K,M,P walk");
        foreach (mapPoint point in road)
        {
            SetPosition(point.x, point.y);
            this.point = new mapPoint(point);
            transform.LookAt(GameManage.Instance.groundList[point.x][point.y].transform);
            GameManage.Instance.IsWalk = true;
            
            yield return new WaitForSeconds(1f);
            GameManage.Instance.groundList[point.x][point.y].ReturnMaterial();
        }
        GameManage.Instance.IsWalk = false;
        animation.Play("K,P idle 2");
        //foreach (var walk in walkables)
        //{
        //    GroundManage.Instance.groundList[role.x][role.y].ReturnMaterial();
        //}
        //walkables = GetCanWalk.getWalk(GroundManage.Instance.groundList[role.x][role.y], role.moves);
        //foreach (var walk in walkables)
        //{
        //    GroundManage.Instance.groundList[role.x][role.y].ChangeMaterial();
        //}
    }

    private void Update()
    {
        if (GameManage.Instance.IsWalk)
        {
            if(point!=null)
            transform.position = Vector3.MoveTowards(transform.position, GameManage.Instance.groundList[point.x][point.y].transform.position, Time.deltaTime * 0.18f);
        }
    }
}

