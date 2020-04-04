public enum PropType
{
    //技能
    SKILL,
    //装备
    EQUIP,
    //物品
    GOODS,
}

public enum EquipmentEffectType
{
    speed,
    strength,
    energy,
    blood,
    moves,
    roundGas,
    totalGas,
}

public enum EquipmentType
{
    /// <summary>
    /// 头盔
    /// </summary>
    helmet,
    /// <summary>
    /// 铠甲
    /// </summary>
    armor,
    /// <summary>
    /// 鞋子
    /// </summary>
    shoes,
}

public class mapPoint
{
    public int x;
    public int y;
    public int id;
    public int vaule = 0;
    public int f = 0;
    public int g;
    public mapPoint parent = null;

    public mapPoint(mapPoint mapPoint)
    {
        this.x = mapPoint.x;
        this.y = mapPoint.y;
        this.vaule = mapPoint.vaule;

    }
    public mapPoint(int x, int y, int vaule)
    {
        this.x = x;
        this.y = y;
        this.vaule = vaule;
    }
}

public class Buff 
{
    public SkillEffectType skillEffectType;
    public string buffName;
    public int times;
    public int original;
}

public class Equipment
{
    public EquipmentType equipmentType;
    public EquipmentEffectType equipmentEffectType;
    public int level;
    public int color;
    public string name;
}

public class Skill
{
    public int id;
    public int level;

    public Skill(int id,int level)
    {
        this.id = id;
        this.level = level;
    }
}

public class Prop
{
    public PropsConf conf;
    public int num;
}