using UnityEngine;
using System.Collections;
using System.Xml;

public class SkillConf:XMLConfig
{
    public SkillAreaType skillAreaType;
    public SkillEffectType skillEffectType;
    public SkillAdditionType skillAdditionType;
    public int id;
    public string skillName;
    /// <summary>
    /// 原始伤害
    /// </summary>
    public int original;
    /// <summary>
    /// 加成系数
    /// </summary>
    public float multiple;
    /// <summary>
    /// 范围
    /// </summary>
    public float range;
    public string buffList;
    public override void Read(XmlNode item)
    {
        skillAreaType = (SkillAreaType)GetInt(item, "skillAreaType");
        skillEffectType = (SkillEffectType)GetInt(item, "skillEffectType");
        skillAdditionType = (SkillAdditionType)GetInt(item, "skillAdditionType");
        id = GetInt(item, "id");
        skillName = GetString(item, "skillName");
        original = GetInt(item, "original");
        multiple = GetFloat(item, "multiple");
        range = GetFloat(item, "range");
        buffList = GetString(item, "buffList");
    }
}

public class Buff : XMLConfig
{
    public SkillEffectType skillEffectType;
    public SkillAdditionType skillAdditionType;
    public int id;
    public string buffName;
    public int times;
    public  int original;
    public float multiple;

    public override void Read(XmlNode item)
    {
        skillEffectType = (SkillEffectType)GetInt(item, "skillEffectType");
        skillAdditionType = (SkillAdditionType)GetInt(item, "skillAdditionType");
        id = GetInt(item, "id");
        buffName = GetString(item, "skillName");
        times = GetInt(item, "times");
        original = GetInt(item, "original");
        multiple = GetFloat(item, "multiple");
    }
}
