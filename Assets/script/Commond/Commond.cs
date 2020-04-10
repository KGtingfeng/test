public enum PropType
{
    //技能
    SKILL,
    //物品
    GOODS,
}

public enum EquipmentEffectType
{
    no,
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

public enum ColorType
{
    Green,
    Blue,
    Purple,
    Red,
    Orange,
}
/// <summary>
/// 技能释放类型
/// </summary>
public enum SkillAreaType
{
    // 周围一圈
    OuterCircle,   
    // 直线
    Line,       
    //自己
    Self,
    //圆
    Circle,
    //点
    Point,
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
/// 天赋影响属性类型
/// </summary>
public enum TalentEffectType
{
    moves,
    blood,
    gas,
    strength,
    energy,
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

public enum PointType
{
    RIGHT,
    LEFT,
    RIGHTUP,
    RIGHTDOWN,
    LEFTUP,
    LEFTDOWN,
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

public class SkillPoint
{
    public int x, y;
    public PointType type;
    public SkillPoint(int x,int y,PointType type)
    {
        this.x = x;
        this.y = y;
        this.type = type;
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