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
    
    public override void Read(XmlNode item)
    {
        skillAreaType = (SkillAreaType)GetInt(item, "skillAreaType");
        skillEffectType = (SkillEffectType)GetInt(item, "skillEffectType");
        skillAdditionType = (SkillAdditionType)GetInt(item, "skillAdditionType");
        id = GetInt(item, "id");
        skillName = GetString(item, "skillName");
        
    }
}

public class SkillLevelConf : XMLConfig
{
    public int id;

    public int level;
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
    public int range;
    public string buffList;
    public override void Read(XmlNode item)
    {        
        id = GetInt(item, "id");
        level = GetInt(item, "level");
        original = GetInt(item, "original");
        multiple = GetFloat(item, "multiple");
        range = GetInt(item, "range");
        buffList = GetString(item, "buffList");
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
    }
}

public class EquipmentConf: XMLConfig
{
    public EquipmentType equipmentType;
    public EquipmentEffectType equipmentEffectType;
    public int equipTypeAdd;
    public int equipEffectTypeAdd;

    public override void Read(XmlNode item)
    {
        equipmentType = (EquipmentType)GetInt(item, "equipmentType");
        equipmentEffectType = (EquipmentEffectType)GetInt(item, "equipmentEffectType");
        equipTypeAdd = GetInt(item, "equipTypeAdd");
        equipEffectTypeAdd = GetInt(item, "equipEffectTypeAdd");
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