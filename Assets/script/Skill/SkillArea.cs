using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
/// <summary>
/// 技能释放类型
/// </summary>
public enum SkillAreaType
{
    OuterCircle,    // 外圆
    Cube,           // 矩形 
    Now,
}

/// <summary>
/// 技能影响属性类型
/// </summary>
public enum SkillEffectType
{
    moves,
    blood,
    gas,
}

/// <summary>
/// 技能加成类型
/// </summary>
public enum SkillAdditionType
{
    speed,
    strength,
    energy,
    level,
}

public class SkillArea : MonoBehaviour
{

    public SkillAreaType areaType;      // 设置指示器类型

    Vector3 deltaVec;

    float outerRadius = 2f;      
    float innerRadius = 1f;     
    float cubeWidth = 0.5f;       

    string path = "SkillArea/Prefabs/Hero_skillarea/";  // 路径
    string circle = "quan_hero";    // 圆形
    string cube = "chang_hero";     // 矩形
    string sector60 = "shan_hero_60";    // 扇形60度
    string sector120 = "shan_hero_120";    // 扇形120度

    Dictionary<SkillAreaType, string> allElementPath;
    Dictionary<SkillAreaType, Transform> allElementTrans;

    public GameObject role;
    bool isCreate;

    public void skillArea(SkillLevelConf levelConf,SkillConf skillConf, GameObject role)
    {
        this.role = role;
        this.areaType = skillConf.skillAreaType;
        this.outerRadius = levelConf.range;
        InitSkillAreaType();
    } 


    void InitSkillAreaType()
    {
        allElementPath = new Dictionary<SkillAreaType, string>();
        allElementPath.Add(SkillAreaType.OuterCircle, circle);
        allElementPath.Add(SkillAreaType.Cube, cube);

        allElementTrans = new Dictionary<SkillAreaType, Transform>();
        allElementTrans.Add(SkillAreaType.OuterCircle, null);
        allElementTrans.Add(SkillAreaType.Cube, null);

        areaType = SkillAreaType.Cube;
        CreateSkillArea();
        isCreate = true;
    }



    void LateUpdate()
    {
        if(isCreate)
            Turning();
    }

    /// <summary>
    /// 创建技能区域展示
    /// </summary>
    void CreateSkillArea()
    {
        switch (areaType)
        {
            case SkillAreaType.OuterCircle:
                CreateElement(areaType);
                break;                              
            default:
                CreateElement(SkillAreaType.OuterCircle);
                CreateElement(areaType);
                break;
        }
    }

    /// <summary>
    /// 创建技能区域展示元素
    /// </summary>
    /// <param name="element"></param>
    void CreateElement(SkillAreaType element)
    {
        Transform elementTrans = GetElement(element);
        if (elementTrans == null) return;
        allElementTrans[element] = elementTrans;
        switch (element)
        {
            case SkillAreaType.OuterCircle:
                elementTrans.localScale = new Vector3(outerRadius * 2, 1, outerRadius * 2) * 0.2f;
                elementTrans.gameObject.SetActive(true);
                break;
            case SkillAreaType.Cube:
                elementTrans.localScale = new Vector3(cubeWidth, 1, outerRadius) * 0.2f;
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 获取元素物体
    /// </summary>
    Transform GetElement(SkillAreaType element)
    {
        string name = element.ToString();

        GameObject elementGo = Instantiate(Resources.Load(path + allElementPath[element])) as GameObject;
        elementGo.transform.parent = role.transform;
        elementGo.name = name;
        Transform elementTrans = elementGo.transform;
        elementTrans.localEulerAngles = Vector3.zero;
        elementTrans.localPosition = Vector3.zero;
        elementTrans.localScale = Vector3.one;
        return elementTrans;
    }

    LayerMask whatIsGround = ~(1 << 0);

    void Turning()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(camRay, out hitInfo, 200, whatIsGround))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            allElementTrans[areaType].LookAt(target);
        }
    }

    public void Delect()
    {
        foreach(var i in allElementTrans)
        {
            if (i.Value != null)
                Destroy(i.Value.gameObject);
        }
    }
}