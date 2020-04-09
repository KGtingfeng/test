using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public class SkillUse: MonoBehaviour
{
    public static string SKILLPATH = "GameObject/Skill/";
    List<SkillPoint> rangeList=new List<SkillPoint>();
    List<SkillPoint> damageList=new List<SkillPoint>();
    List<List<mapPoint>> mapp ;
    List<SkillPoint> open = new List<SkillPoint>();
    List<SkillPoint> close = new List<SkillPoint>();
    SkillConf skill;
    SkillLevelConf skillLevel;
    Ground ground;
    public void GetSkillArea(SkillConf skill,SkillLevelConf skillLevel)
    {
        mapp = GameManage.Instance.mapPoints;
        rangeList.Clear();
        damageList.Clear();
        this.skill = skill;
        this.skillLevel = skillLevel;
        switch (skill.skillAreaType)
        {
            case SkillAreaType.Line:
                GetArea();
                ShowRange();
                break;
            case SkillAreaType.Self:
                GetSelf();
                ShowSkill();
                break;
            case SkillAreaType.OuterCircle:
                GetArea();
                ShowRange();
                break;
            case SkillAreaType.Circle:
                GetArea();
                damageList = rangeList;
                ShowSkill();
                break;
            case SkillAreaType.Point:
                GetArea();
                ShowRange();
                break;
        }
    }

    void GetArea()
    {
        open.Clear();
        close.Clear();
        SkillPoint my = new SkillPoint(GameManage.Instance.role.x, GameManage.Instance.role.y,PointType.LEFT);
        open.Add(my);
        for (int i = 0; i < skill.range; i++)
        {
            int count = open.Count;
            for (int j=0;j< count; j++)
            {
                SkillPoint now = open[j];
                if (now.y + 1 < GameManage.col)
                    addOpen(mapp[now.x][now.y + 1], PointType.RIGHT);
                if (now.y - 1 >= 0)
                    addOpen(mapp[now.x][now.y - 1], PointType.LEFT);

                if (now.x % 2 != 0)
                {
                    if (now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y], PointType.LEFTUP);
                    if (now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y], PointType.LEFTDOWN);
                    if (now.y + 1 < GameManage.col && now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y + 1], PointType.RIGHTUP);
                    if (now.y + 1 < GameManage.col && now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y + 1], PointType.RIGHTDOWN);
                }
                else
                {
                    if (now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y], PointType.RIGHTUP);
                    if (now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y], PointType.RIGHTDOWN);
                    if (now.y - 1 >= 0 && now.x + 1 < GameManage.row)
                        addOpen(mapp[now.x + 1][now.y - 1], PointType.LEFTUP);
                    if (now.y - 1 >= 0 && now.x - 1 >= 0)
                        addOpen(mapp[now.x - 1][now.y - 1], PointType.LEFTDOWN);
                }
                close.Add(open[j]);
            }
            for(int j = 0; j < count; j++)
            {    
                open.Remove(open[0]);
            }
            
        }
        open.AddRange(close);
        open.Remove(my);
        rangeList = open;
        GameManage.Instance.IsSkill = true;
    }

    void GetSelf()
    {
        SkillPoint p = new SkillPoint(GameManage.Instance.role.x, GameManage.Instance.role.y,PointType.LEFT);
        rangeList.Add(p);
        damageList.Add(p);
        GameManage.Instance.IsSkill = true;
    }

    void addOpen(mapPoint p,PointType type)
    {
        SkillPoint point = new SkillPoint(p.x,p.y,type);
        open.Add(point);
    }

    void Attack()
    {
        CreateSkill();
        int damage = (int)GameTools.CalculateDamage(GameManage.Instance.role,skillLevel,skill);
        if (skill.skillAreaType == SkillAreaType.Circle || skill.skillAreaType == SkillAreaType.OuterCircle)
            if(damageList.Find(a => a.x == GameManage.Instance.role.x && a.y == GameManage.Instance.role.y)!=null)
            damageList.Remove(damageList.Find(a => a.x == GameManage.Instance.role.x && a.y == GameManage.Instance.role.y));
        foreach (SkillPoint point in damageList)
        {
            if(GameManage.Instance.groundList[point.x][point.y].character!=null)
            GameTools.Damage(GameManage.Instance.groundList[point.x][point.y].character,damage,skill.skillEffectType,skillLevel);
        }
        CloseRange();
        CloseSkill();
        damageList.Clear();
        GameManage.Instance.IsSkill = false;
        GameManage.Instance.role.gas -= skillLevel.gas;
    }

    public void Cancel()
    {
        CloseRange();
        CloseSkill();
        GameManage.Instance.IsSkill = false;
    }

    private Ray ray;
    private RaycastHit hit;
    public void Update()
    {
        if (GameManage.Instance.IsSkill)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (damageList.Count > 0)
                {
                    Attack();
                }
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "ground")
                {
                    Ground g = hit.transform.GetComponent<Ground>();
                    if (ground != null && ground.x == g.x && ground.y == g.y)
                        return;
                    ground = g;
                    if( rangeList.Find(a => a.x == g.x && a.y == g.y) != null)
                    {
                        switch (skill.skillAreaType)
                        {
                            case SkillAreaType.Line:
                                GetLineSkill(rangeList.Find(a => a.x == g.x && a.y == g.y));
                                break;
                            case SkillAreaType.OuterCircle:
                                GetOuterCircleSkill(rangeList.Find(a => a.x == g.x && a.y == g.y));
                                break;
                            case SkillAreaType.Point:
                                CloseSkill();
                                damageList.Clear();
                                damageList.Add(rangeList.Find(a => a.x == g.x && a.y == g.y));
                                ShowSkill();
                                break;
                        }
                    }
                    else
                    {
                        if(skill.skillAreaType!=SkillAreaType.Self&& skill.skillAreaType != SkillAreaType.Circle)
                        {
                            CloseSkill();
                            damageList.Clear();
                            ShowRange();
                        }
                        
                    }
                }
                else
                {
                    if (skill.skillAreaType != SkillAreaType.Self && skill.skillAreaType != SkillAreaType.Circle)
                    {
                        CloseSkill();
                        damageList.Clear();
                        ShowRange();
                    }
                }
            }
            
        }
    }

    void GetLineSkill(SkillPoint point)
    {
        CloseSkill();
        damageList.Clear();
        damageList.Add(point);
        switch (point.type)
        {
            case PointType.LEFT:
                GetLEFT(point);
                break;
            case PointType.RIGHT:
                GetRIGHT(point);
                break;
            case PointType.LEFTUP:
                GetLEFTUP(point);
                break;
            case PointType.LEFTDOWN:
                GetLEFTDOWN(point);
                break;
            case PointType.RIGHTUP:
                GetRIGHTUP(point);
                break;
            case PointType.RIGHTDOWN:
                GetRIGHTDOWN(point);
                break;
        }
        ShowSkill();
    }

    void GetOuterCircleSkill(SkillPoint point)
    {
        CloseSkill();
        damageList.Clear();
        damageList.Add(point);
        if (point.y + 1 < GameManage.col)
        {
            SkillPoint p = new SkillPoint(point.x, point.y + 1, PointType.RIGHT);
            damageList.Add(p);
        }
        if (point.y - 1 >= 0)
        {
            SkillPoint p = new SkillPoint(point.x, point.y - 1, PointType.RIGHT);
            damageList.Add(p);
        }
        if (point.x % 2 != 0)
        {
            if (point.x + 1 < GameManage.row)
            {
                SkillPoint p = new SkillPoint(point.x+1, point.y, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.x - 1 >= 0)
            {
                SkillPoint p = new SkillPoint(point.x - 1, point.y, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.y + 1 < GameManage.col && point.x + 1 < GameManage.row)
            {
                SkillPoint p = new SkillPoint(point.x + 1, point.y+1, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.y + 1 < GameManage.col && point.x - 1 >= 0)
            {
                SkillPoint p = new SkillPoint(point.x - 1, point.y+1, PointType.RIGHT);
                damageList.Add(p);
            }
        }
        else
        {
            if (point.x + 1 < GameManage.row)
            {
                SkillPoint p = new SkillPoint(point.x + 1, point.y, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.x - 1 >= 0)
            {
                SkillPoint p = new SkillPoint(point.x - 1, point.y, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.y - 1 >= 0 && point.x + 1 < GameManage.row)
            {
                SkillPoint p = new SkillPoint(point.x + 1, point.y-1, PointType.RIGHT);
                damageList.Add(p);
            }
            if (point.y - 1 >= 0 && point.x - 1 >= 0)
            {
                SkillPoint p = new SkillPoint(point.x - 1, point.y-1, PointType.RIGHT);
                damageList.Add(p);
            }
        }
        ShowSkill();
    }

    void ShowRange()
    {
        foreach(var p in rangeList)
        {
            GameManage.Instance.groundList[p.x][p.y].ChangeSkillRange();
        }
    }

    void CloseRange()
    {
        foreach (var p in rangeList)
        {
            GameManage.Instance.groundList[p.x][p.y].ReturnMaterial();
        }
    }

    void ShowSkill()
    {
        CloseSkill();
        ShowRange();
        foreach (var p in damageList)
        {
            GameManage.Instance.groundList[p.x][p.y].ChangeSkill();
        }
    }

    void CloseSkill()
    {
        foreach (var p in damageList)
        {
            GameManage.Instance.groundList[p.x][p.y].ReturnMaterial();
        }
    }

    void GetRIGHT(SkillPoint p)
    {
        if (p.y + 1 < GameManage.col)
        {
            SkillPoint point = new SkillPoint(p.x, p.y + 1, PointType.RIGHT);
            damageList.Add(point);
        }
    }
    void GetLEFT(SkillPoint p)
    {
        if (p.y - 1 >= 0)
        {
            SkillPoint point = new SkillPoint(p.x, p.y - 1, PointType.RIGHT);
            damageList.Add(point);
        }
    }
    void GetRIGHTUP(SkillPoint p)
    {
        if (p.x % 2 != 0)
        {
            if (p.y + 1 < GameManage.col && p.x + 1 < GameManage.row)
            {
                SkillPoint point = new SkillPoint(p.x+1, p.y + 1, PointType.RIGHTUP);
                damageList.Add(point);
            }
        }
        else
        {
            if (p.x + 1 < GameManage.row)
            {
                SkillPoint point = new SkillPoint(p.x+1, p.y, PointType.RIGHTUP);
                damageList.Add(point);
            }

        }
    }
    void GetRIGHTDOWN(SkillPoint p)
    {
        if (p.x % 2 != 0)
        {
            if (p.y + 1 < GameManage.col && p.x - 1 >= 0)
            {
                SkillPoint point = new SkillPoint(p.x - 1, p.y+1, PointType.RIGHTDOWN);
                damageList.Add(point);
            }
        }
        else
        {
            if (p.x - 1 >= 0)
            {
                SkillPoint point = new SkillPoint(p.x - 1, p.y, PointType.RIGHTDOWN);
                damageList.Add(point);
            }
        }
    }
    void GetLEFTUP(SkillPoint p)
    {
        if (p.x % 2 != 0)
        {
            if (p.x + 1 < GameManage.row)
            {
                SkillPoint point = new SkillPoint(p.x + 1, p.y, PointType.LEFTUP);
                damageList.Add(point);
            }

        }
        else
        {
            if (p.y - 1 >= 0 && p.x + 1 < GameManage.row)
            {
                SkillPoint point = new SkillPoint(p.x + 1, p.y-1, PointType.LEFTUP);
                damageList.Add(point);
            }
        }

    }
    void GetLEFTDOWN(SkillPoint p)
    {
        if (p.x % 2 != 0)
        {
            if (p.x - 1 >= 0)
            {
                SkillPoint point = new SkillPoint(p.x - 1, p.y, PointType.LEFTDOWN);
                damageList.Add(point);
            }
        }
        else
        {
            if (p.y - 1 >= 0 && p.x - 1 >= 0)
            {
                SkillPoint point = new SkillPoint(p.x - 1, p.y-1, PointType.LEFTDOWN);
                damageList.Add(point);
            }
        }

    }

    void CreateSkill()
    {
        GameObject go = Instantiate(Resources.Load(SKILLPATH + skill.id.ToString())) as GameObject;
        switch (skill.skillAreaType)
        {
            case SkillAreaType.Line:
                transform.LookAt(GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform);          
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0.3f, 0);
                go.transform.LookAt(GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform);
                break;
            case SkillAreaType.Self:
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0.2f, 0);
                break;
            case SkillAreaType.OuterCircle:
                transform.LookAt(GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform);
                go.transform.parent = GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform;
                go.transform.localPosition = new Vector3(0, 0.2f, 0);
                break;
            case SkillAreaType.Circle:
                go.transform.parent = transform;
                go.transform.localPosition = new Vector3(0, 0.2f, 0);
                break;
            case SkillAreaType.Point:
                transform.LookAt(GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform);
                go.transform.parent = GameManage.Instance.groundList[damageList[0].x][damageList[0].y].transform;
                go.transform.localPosition = new Vector3(0, 0.2f, 0);
                break;
        }
        
    }
}