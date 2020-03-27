using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Character
{
    public UserData userData;
    public int equMove=0;
    public int equRoundGas=0;
    public int equTotalGas=0;


    List<mapPoint> walkables;
    public SkillArea skillArea;
    public SkillConf skillConf;
    public SkillLevelConf skillLevel;

    public int id;

    static string SKILLPATH = "GameObject/Skill/";

    public void Create( CharacterConf character)
    {
        this.id = character.id;
        this.level = 1;
        speed = character.speed + ((level - 1) * character.levelSpeed);
        strength = character.strength + ((level - 1) * character.levelStrength);
        energy = character.energy + ((level - 1) * character.levelEnergy);
        blood = character.blood + ((level - 1) * character.levelBlood) + (strength * 20);
        totalBlood = blood;
        moves =  (speed / 5);
        roundGas = energy / 5;
        gas = roundGas;
        totalGas = roundGas * 2;
        Skill skill = new Skill();
        skill.id = character.skill;
        skill.level = 1;
        userData.skills.Add(skill);
    }

    public void LevelUp()
    {
        CharacterConf character = XMLData.CharacterConfs.Find(a => a.id == this.id);
        level++;
        speed +=   character.levelSpeed;
        strength +=   character.levelStrength;
        energy += character.levelEnergy;
        totalBlood = character.levelBlood + (character.levelStrength * 20);
        blood = totalBlood;
        moves = speed / 5+ equMove;
        roundGas = energy / 5+ equRoundGas;
        totalGas = roundGas * 2+ equTotalGas;
    }


    public void ShowWalkable()
    {
        walkables = GetCanWalk.getWalk(GameManage.Instance.groundList[x][y], moves);
        foreach (var walk in walkables)
        {
            GameManage.Instance.groundList[x][y].ChangeMaterial();
        }
    }

    public void CreateSkill(Vector3 target)
    {
        transform.LookAt(target);
        DelectSkillArea();
        GameObject go = Instantiate(Resources.Load(SKILLPATH + skillConf.id.ToString())) as GameObject;
        go.transform.parent = transform;
        go.transform.localPosition = Vector3.zero;

        go.transform.LookAt(target);
        go.GetComponent<SkillColl>().character = this;
        go.GetComponent<SkillColl>().skillNum = GameManage.Instance.skillNum++;
        go.GetComponent<SkillColl>().skill = skillConf;
        go.GetComponent<SkillColl>().skillLevelConf = skillLevel;
        GameManage.Instance.IsSkill = false;
    }


    public void CreateSkillArea()
    {
        List<SkillConf> skillConfigs = XMLData.SkillConfigs;
        List<SkillLevelConf> skillLevelConfs = XMLData.SkillLevelConfs;
        skillConf = skillConfigs[0];
        skillArea = gameObject.AddComponent<SkillArea>();
        skillLevel = skillLevelConfs[0];
        skillArea.skillArea(skillLevelConfs[0], skillConfigs[0],gameObject);
    }

    public void DelectSkillArea()
    {
        skillArea.Delect();
        Destroy(gameObject.GetComponent<SkillArea>());
    }

    public void RoundStart()
    {

    }
        
}
