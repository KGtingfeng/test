using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Character
{    
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
        roundMove = 0;
        gas = 0;
        totalGas = roundGas * 2;
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


    public void CreateSkillArea(int id, int level)
    {        
        skillConf = XMLData.SkillConfigs.Find(a=>a.id==id);
        skillLevel = XMLData.SkillLevelConfs.Find(a=>a.id==id&&a.level==level);
        if (skillConf.skillAreaType == SkillAreaType.Now)
        {
            GameObject go = Instantiate(Resources.Load(SKILLPATH + skillConf.id.ToString())) as GameObject;
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;
            float damage = GameTools.CalculateDamage(GetComponent<Character>(), skillLevel, skillConf);
            GameTools.Damage(GetComponent<Character>(), (int)damage, skillConf.skillEffectType, skillLevel);
        }
        else
        {
            skillArea = gameObject.AddComponent<SkillArea>();           
            skillArea.skillArea(skillLevel, skillConf, gameObject);
        }
        
    }

    public void DelectSkillArea()
    {
        skillArea.Delect();
        Destroy(gameObject.GetComponent<SkillArea>());
    }

    public void RoundStart()
    {

    }

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
                if (GameManage.Instance.mapPoints[point.x][point.y].vaule == 2)
                {
                    GameTools.GetPorp(GameManage.Instance.mapPoints[point.x][point.y].id);
                    GameManage.Instance.mapPoints[point.x][point.y].vaule = 0;
                }
                this.point = new mapPoint(point);
                transform.LookAt(GameManage.Instance.groundList[point.x][point.y].transform);
                GameManage.Instance.IsWalk = true;
                roundMove--;
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
}
