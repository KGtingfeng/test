using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
public class SkillConf:XMLConfig
{
    public SkillAreaType skillAreaType;
    public SkillEffectType skillEffectType;
    public SkillAdditionType skillAdditionType;
    public int id;
    public string skillName;
    /// <summary>
    /// 范围
    /// </summary>
    public int range;
    public override void Read(XmlNode item)
    {
        skillAreaType = (SkillAreaType)GetInt(item, "skillAreaType");
        skillEffectType = (SkillEffectType)GetInt(item, "skillEffectType");
        skillAdditionType = (SkillAdditionType)GetInt(item, "skillAdditionType");
        id = GetInt(item, "id");
        skillName = GetString(item, "skillName");
        range = GetInt(item, "range");
    }
}

public class SkillLevelConf : XMLConfig
{
    public int id;
    public int gas;
    public int level;
    /// <summary>
    /// 原始伤害
    /// </summary>
    public int original;
    /// <summary>
    /// 加成系数
    /// </summary>
    public float multiple;

    public string buffList;
    public override void Read(XmlNode item)
    {        
        id = GetInt(item, "id");
        level = GetInt(item, "level");
        original = GetInt(item, "original");
        multiple = GetFloat(item, "multiple");     
        buffList = GetString(item, "buffList");
        gas = GetInt(item, "gas");
    }
}

public class CharacterConf : XMLConfig
{
    public int id;
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
    /// 血量
    /// </summary>
    public int blood;
    
    /// <summary>
    /// 每级加速度 
    /// </summary>
    public int levelSpeed;
    /// <summary>
    /// 每级加体力 
    /// </summary>
    public int levelStrength;
    /// <summary>
    /// 每级加精力 
    /// </summary>
    public int levelEnergy;
    /// <summary>
    /// 每级加血量
    /// </summary>
    public int levelBlood;
    public int skill;
    public string name;

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        speed = GetInt(item,"speed");
        strength = GetInt(item, "strength");
        energy = GetInt(item, "energy");
        blood = GetInt(item, "blood");
        levelSpeed = GetInt(item, "levelSpeed");
        levelStrength = GetInt(item, "levelStrength");
        levelEnergy = GetInt(item, "levelEnergy");
        levelBlood = GetInt(item, "levelBlood");
        skill = GetInt(item, "skill");
        name= GetString(item, "name");
    }
}

public class EquipmentConf: XMLConfig
{
    public int id;
    public EquipmentType equipmentType;
    public AtrrType equipmentEffectType;
    public int equipTypeAdd;
    public int equipEffectTypeAdd;
    public string equName;

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        equipmentType = (EquipmentType)GetInt(item, "equipmentType");
        equipmentEffectType = (AtrrType)GetInt(item, "equipmentEffectType");
        equipTypeAdd = GetInt(item, "equipTypeAdd");
        equipEffectTypeAdd = GetInt(item, "equipEffectTypeAdd");
        equName = GetString(item, "equName");
    }
}

public class TerrainConf : XMLConfig
{
    public int id;
    public bool canDestroy;
    public bool canWlak;
    public string terrainName;
    public string introduction;

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        canDestroy = GetBool(item, "canDestroy");
        canWlak = GetBool(item, "canWlak");
        terrainName = GetString(item, "terrainName");
        introduction = GetString(item, "introduction");
    }
}

public class PropsConf : XMLConfig
{
    public int id;
    public PropType type;
    public string buff;
    public string introduction;
    public string propsName;

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        type = (PropType)GetInt(item, "type");
        buff = GetString(item, "buff");
        introduction = GetString(item, "introduction");
        propsName = GetString(item, "propsName");
    }
}

public class MapConf : XMLConfig
{
    public int id;
    public int mapType;
    public string map;
    

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        mapType = GetInt(item, "mapType");
        map = GetString(item, "map");
    }
}

public class TalentConf : XMLConfig
{
    public int id;
    public string buff;
    public string introduction;
    public string talentName;
    public int num;
    public TalentType type;

    public override void Read(XmlNode item)
    {
        id = GetInt(item, "id");
        buff = GetString(item, "buff");
        introduction = GetString(item, "introduction");
        talentName = GetString(item, "talentName");
        num = GetInt(item, "num");
        type =(TalentType) GetInt(item, "type");
    }
}

public class GameData:XMLConfig
{
    public int score;
    public string talent;
    public List<TalentConf> talents= new List<TalentConf>();

    public override void Read(XmlNode item)
    {
        score = GetInt(item, "score");
        talent = GetString(item, "talent");
    }
}

public class EXPConf : XMLConfig
{
    public int level;
    public int minExp;
    public int maxExp;

    public override void Read(XmlNode item)
    {
        level = GetInt(item, "level");
        minExp = GetInt(item, "minExp");
        maxExp = GetInt(item, "maxExp");
    }
}