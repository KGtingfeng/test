using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : Character
{
    public AIBase AI;
    public int id;
    public SkillConf skillConf;
    public SkillLevelConf skillLevel;

    public void Create(int level, CharacterConf character)
    {
        this.id = character.id;
        this.level = level;
        speed = character.speed+((level-1)* character.levelSpeed);
        strength = character.strength + ((level - 1) * character.levelStrength);
        energy = character.energy + ((level - 1) * character.levelEnergy);
        blood = character.blood + ((level - 1) * character.levelBlood)+(strength*20);
        totalBlood = blood;
        moves = speed/5;
        roundGas = energy / 5;
        gas = roundGas;
        totalGas = roundGas * 2;
        skillConf = XMLData.SkillConfigs.Find(a => a.id == character.skill);
        skillLevel= XMLData.SkillLevelConfs.Find(a => a.id == character.skill);
        AI.GetPoint();
    }

    public override void StartRound()
    {
        base.StartRound();
        AI.StartRound();
    }

    public bool Attack()
    {
        if (GameTools.GetDistance(GetPosition()) > skillLevel.range)
            return false;
        transform.LookAt(GameManage.Instance.role.GetPosition());
        GameObject go = Instantiate(Resources.Load(SKILLPATH + skillConf.id.ToString())) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.LookAt(GameManage.Instance.role.GetPosition());
        go.GetComponent<SkillColl>().character = this;
        go.GetComponent<SkillColl>().skillNum = GameManage.Instance.skillNum++;
        go.GetComponent<SkillColl>().skill = skillConf;
        go.GetComponent<SkillColl>().skillLevelConf = skillLevel;
        return true;
    }

    public override void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
        transform.parent = GameManage.Instance.groundList[x][y].transform;
    }

    #region 行走
    public void Goto(Vector2 hit)
    {
        if (roundMove == 0)
            return;
        Vector2 pos = GetPosition();
        Ground finish = GameManage.Instance.groundList[(int)hit.x][(int)hit.y];
        List<mapPoint> road = AStar.find(GameManage.Instance.groundList[(int)pos.x][(int)pos.y], finish);
        if (road != null)
        {
            road.Reverse();
            GoToFinish(road);
        }
        else
        {
            Debug.LogError("不可走");
        }
    }

    public void GoToFinish(List<mapPoint> road)
    {
        foreach (mapPoint point in road)
        {
            if (roundMove > 0)
            {
                SetPosition(point.x, point.y);
                this.point = new mapPoint(point);
                transform.LookAt(GameManage.Instance.groundList[point.x][point.y].transform);
                transform.localPosition = Vector3.zero;
                if (!AI.IsAttack)
                    if (AI.Find())
                        AI.Attack();
            }            
        }
    }

    #endregion

    public override void Dead()
    {
        base.Dead();
    }
}
