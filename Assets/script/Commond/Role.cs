using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Role : Character
{
    List<mapPoint> walkables;
    SkillArea skillArea;
    SkillConf skillConf;

    static string SKILLPATH = "GameObject/Skill/";
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
        go.GetComponent<SkillColl>().skillNum = GameManage.Instance.skillNum++;
        GameManage.Instance.IsSkill = false;
    }


    public void CreateSkillArea()
    {
        List<SkillConf> skillConfigs = XMLData.SkillConfigs;
        skillConf = skillConfigs[0];
        skillArea = gameObject.AddComponent<SkillArea>();
        skillArea.skillArea(skillConfigs[0],gameObject);
    }

    public void DelectSkillArea()
    {
        skillArea.Delect();
        Destroy(gameObject.GetComponent<SkillArea>());
    }
}
