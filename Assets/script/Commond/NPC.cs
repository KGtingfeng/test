using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : Character
{
    public AIBase AI;
    public SkillConf skillConf;
    public SkillLevelConf skillLevel;

    public void Create(int level, CharacterConf character)
    {
        IsNPC = true;
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
    }

    public override void StartRound()
    {
        base.StartRound();
        AI.StartRound();
    }

    public override void SetPosition(int x, int y)
    {
        base.SetPosition(x, y);
        transform.parent = GameManage.Instance.groundList[x][y].transform;
        transform.localPosition = Vector3.zero;
        //Debug.LogError(x + "    " + y + "    " + GameManage.Instance.groundList[x][y].transform.position+"   "+ transform.position);
    }

    #region 放技能
    public bool Attack()
    {
        if (GameTools.GetDistance(GetPosition()) > skillConf.range)
            return false;
        if (gas < skillLevel.gas)
            return false;
        CreateSkill();
        int damage = (int)GameTools.CalculateDamage(GameManage.Instance.role, skillLevel, skillConf);
        GameTools.Damage(GameManage.Instance.role, damage, skillConf.skillEffectType, skillLevel);       
        gas -= skillLevel.gas;
        return true;
    }

    void CreateSkill()
    {
        GameObject go = Instantiate(Resources.Load(SKILLPATH + skillConf.id.ToString())) as GameObject;
        transform.LookAt(GameManage.Instance.groundList[GameManage.Instance.role.x][GameManage.Instance.role.y].transform);
        go.transform.parent = GameManage.Instance.groundList[GameManage.Instance.role.x][GameManage.Instance.role.y].transform;
        go.transform.localPosition = new Vector3(0, 0.2f, 0);

    }
    #endregion

    #region 行走
    public void Goto(Vector2 hit)
    {
        //Debug.LogError(transform.name+"   roundMove  " + roundMove);
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
            Debug.LogError(hit.x+"   "+hit.y+"  不可走");
        }
    }

    public void GoToFinish(List<mapPoint> road)
    {
        foreach (mapPoint point in road)
        {
            if (roundMove > 0)
            {
                if (GameManage.Instance.mapPoints[point.x][point.y].vaule == 1)
                    return;
                SetPosition(point.x, point.y);
                this.point = new mapPoint(point);
                transform.LookAt(GameManage.Instance.groundList[point.x][point.y].transform);
                transform.localPosition = Vector3.zero;
                if (!AI.IsAttack)
                    if (AI.Find())
                        AI.Attack();
                roundMove--;
            }            
        }
    }

    #endregion

    public override void Dead()
    {
        GameObject go = Instantiate(Resources.Load(SKILLPATH + "dead")) as GameObject;
        go.transform.parent = GameManage.Instance.groundList[x][y].transform;
        go.transform.localPosition = new Vector3(0, 0.2f, 0);

        GameManage.Instance.role.GetExp(level*100);
        GameManage.Instance.npcList.Remove(GameManage.Instance.npcList.Find(a => a.x == x && a.y == y));
        GameManage.Instance.groundList[x][y].character = null;
        GameManage.Instance.mapPoints[x][y].vaule = 0;
        GameManage.Instance.score += level;
        GameTools.ItemDrop(level);
        Destroy(gameObject);
    }
}
