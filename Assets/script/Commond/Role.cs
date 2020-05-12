using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Character
{    
    public int equMove=0;
    public int equRoundGas=0;


    List<mapPoint> walkables;
    //public SkillArea skillArea;
    public SkillUse SkillUse;
    public SkillConf skillConf;
    public SkillLevelConf skillLevel;

    public void Create( CharacterConf character)
    {
        IsNPC = false;
        this.id = character.id;
        this.level = 1;
        speed = character.speed  ;
        strength = character.strength ;
        energy = character.energy ;
        blood = character.blood + (strength * 20);
        totalBlood = blood;
        moves =  (speed / 5);
        roundGas = energy / 5;
        roundMove = moves;
        totalGas = roundGas * 2;
        gas = totalGas;
        Skill skill = new Skill(character.skill,1);
        GameManage.Instance.userData.skills.Add(skill);
    }

    public void LevelUp()
    {
        CharacterConf character = XMLData.CharacterConfs.Find(a => a.id == this.id);
        level++;
        speed +=   character.levelSpeed;
        strength +=   character.levelStrength;
        energy += character.levelEnergy;
        totalBlood =totalBlood + character.levelBlood + (character.levelStrength * 20);
        blood = totalBlood;
        CalculateArr();
    }

    public void CalculateArr()
    {
        moves = speed / 5 + equMove;
        roundGas = energy / 5 + equRoundGas;
        totalGas = roundGas * 2 ;
    }

    public void GetExp(int exp)
    {
        EXP += exp;
        EXPConf conf = XMLData.EXPConfs.Find(a=>a.minExp<=exp&&exp<a.maxExp);
        if (level == 20)
            return;
        if (conf.level > level)
        {
            while((conf.level-level)>0)
                LevelUp();
        }
    }

    public void ShowWalkable()
    {
        walkables = GetCanWalk.getWalk(GameManage.Instance.groundList[x][y], moves);
        foreach (var walk in walkables)
        {
            GameManage.Instance.groundList[x][y].ChangeMaterial();
        }
    }

    public void CreateSkillArea(Skill skill)
    {                
        skillConf = XMLData.SkillConfigs.Find(a=>a.id== skill.id);
        skillLevel = XMLData.SkillLevelConfs.Find(a=>a.id== skill.id && a.level== skill.level);
        if (gas < skillLevel.gas)
            return;
        SkillUse.GetSkillArea(skillConf, skillLevel);

    }

    public void DelectSkillArea()
    {
        SkillUse.Cancel();
    }

    #region 行走
    public void Goto(RaycastHit hit)
    {
        if (roundMove == 0)
            return;
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
                foreach(mapPoint point in road)
                    GameManage.Instance.groundList[point.x][point.y].ChangeMaterial();
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
            if (GameManage.Instance.mapPoints[point.x][point.y].vaule == 1)
                break;
            if (roundMove > 0)
            {
                SetPosition(point.x, point.y);
                this.point = new mapPoint(point);
                transform.LookAt(GameManage.Instance.groundList[point.x][point.y].transform);
                GameManage.Instance.IsWalk = true;
                roundMove--;
                MainView.Instance.Change();
                yield return new WaitForSeconds(1f);
            } 
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
            if (point != null)
                transform.position = Vector3.MoveTowards(transform.position, GameManage.Instance.groundList[point.x][point.y].transform.position, Time.deltaTime * 0.18f);
        }
    }

    #endregion

    public override void Dead()
    {
        int score = XMLData.GameDatas[0].score + GameManage.Instance.round + GameManage.Instance.score;
        XMLData.SetGameData(score, XMLData.GameDatas[0].talent);
        UIManage.CreateView(new EndController());
    }
}
