using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameTools : MonoBehaviour
{
    static string ROLEPATH = "GameObject/Role/";

    public static float CalculateDamage(Character character,SkillLevelConf skill,SkillConf skillConf)
    {
        int skillAddition = 0;
        switch (skillConf.skillAdditionType)
        {
            case SkillAdditionType.energy:
                skillAddition = character.energy;
                break;
            case SkillAdditionType.strength:
                skillAddition = character.strength;
                break;
            case SkillAdditionType.speed:
                skillAddition = character.speed;
                break;
            case SkillAdditionType.level:
                skillAddition = character.level;
                break;
        }
        return skill.original + skill.multiple * skillAddition;
    }
    
    public static void Damage(Character character,int damage,SkillEffectType skillEffect,SkillLevelConf skillLevel)
    {
        switch (skillEffect)
        {
            case SkillEffectType.blood:
                if (character.blood - damage < 0)
                    character.blood = 0;
                else
                    character.blood -= damage;
                break;
            case SkillEffectType.gas:
                if (character.gas - damage < 0)
                    character.gas = 0;
                else
                    character.gas -= damage;
                break;
            case SkillEffectType.moves:
                if (character.moves - damage < 0)
                    character.moves = 0;
                else
                    character.moves -= damage;
                break;
        }
        if (skillLevel.buffList != null)
        {
            List<Buff> buffs = GetBuff(skillLevel.buffList);
            character.buffList.AddRange(buffs);
        }
    }

    public static List<Buff> GetBuff(string buffList)
    {
        string[] bList = buffList.Split(';');
        if (bList.Length == 0)
        {
            return null;

        }
        List<Buff> buffs=new List<Buff>();
        foreach(string b in bList)
        {
            string[] list = b.Split(',');
            Buff buff = new Buff();
            if(list.Length<7)
            {
                Debug.LogError("Buff格式错误");
                return null;
            }
            buff.skillEffectType= (SkillEffectType)int.Parse(list[0]);
            buff.buffName = list[1];
            buff.times = int.Parse(list[2]);
            buff.original = int.Parse(list[3]);
        }
        return buffs;
    }

    /// <summary>
    /// 更换装备
    /// </summary>
    /// <returns></returns>
    public static void ChangeEquipment(Equipment old , Equipment now)
    {
        EquipmentConf oldEquip = XMLData.EquipmentConfs.Find(a => a.equipmentType == old.equipmentType&& a.equipmentEffectType == old.equipmentEffectType);
        EquipmentConf nowEquip= XMLData.EquipmentConfs.Find(a => a.equipmentType == now.equipmentType && a.equipmentEffectType == now.equipmentEffectType);
        switch (old.equipmentType)
        {
            case EquipmentType.helmet:
                GameManage.Instance.role.gas -= oldEquip.equipTypeAdd * old.level;
                GameManage.Instance.role.gas += nowEquip.equipTypeAdd * now.level;
                break;
            case EquipmentType.armor:
                GameManage.Instance.role.blood -= oldEquip.equipTypeAdd * old.level;
                GameManage.Instance.role.blood += nowEquip.equipTypeAdd * now.level;
                break;
            case EquipmentType.shoes:
                GameManage.Instance.role.moves -= oldEquip.equipTypeAdd * old.level;
                GameManage.Instance.role.moves += nowEquip.equipTypeAdd * now.level;
                break;
        }
        switch (old.equipmentEffectType)
        {
            case EquipmentEffectType.blood:
                GameManage.Instance.role.blood -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.roundGas:
                GameManage.Instance.role.roundGas -= oldEquip.equipEffectTypeAdd * old.color;
                GameManage.Instance.role.equRoundGas -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.totalGas:
                GameManage.Instance.role.totalGas -= oldEquip.equipEffectTypeAdd * old.color;
                GameManage.Instance.role.equTotalGas -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.energy:
                GameManage.Instance.role.energy -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.speed:
                GameManage.Instance.role.speed -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.strength:
                GameManage.Instance.role.strength -= oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.moves:
                GameManage.Instance.role.moves -= oldEquip.equipEffectTypeAdd * old.color;
                GameManage.Instance.role.equMove -= oldEquip.equipEffectTypeAdd * old.color;
                break;
        }
        switch (now.equipmentEffectType)
        {
            case EquipmentEffectType.blood:
                GameManage.Instance.role.blood += nowEquip.equipEffectTypeAdd * now.color;
                break;
            case EquipmentEffectType.roundGas:
                GameManage.Instance.role.roundGas += oldEquip.equipEffectTypeAdd * old.color;
                GameManage.Instance.role.equRoundGas += oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.totalGas:
                GameManage.Instance.role.totalGas += oldEquip.equipEffectTypeAdd * old.color;
                GameManage.Instance.role.equTotalGas += oldEquip.equipEffectTypeAdd * old.color;
                break;
            case EquipmentEffectType.energy:
                GameManage.Instance.role.energy += nowEquip.equipEffectTypeAdd * now.color;
                break;
            case EquipmentEffectType.speed:
                GameManage.Instance.role.speed += nowEquip.equipEffectTypeAdd * now.color;
                break;
            case EquipmentEffectType.strength:
                GameManage.Instance.role.strength += nowEquip.equipEffectTypeAdd * now.color;
                break;
            case EquipmentEffectType.moves:
                GameManage.Instance.role.moves += nowEquip.equipEffectTypeAdd * now.color;
                GameManage.Instance.role.equMove += nowEquip.equipEffectTypeAdd * now.color;
                break;
        }
    }

    public static void GetPorp(int id)
    {
        if (GameManage.Instance.userData.props.Find(a => a.conf.id == id) != null)
        {
            GameManage.Instance.userData.props.Find(a => a.conf.id == id).num++;
        }
        else
        {
            Prop prop = new Prop();
            prop.conf = XMLData.PropsConfs.Find(a => a.id == id);
            prop.num = 1;
            GameManage.Instance.userData.props.Add(prop);

        }
    }

    public static void UseProp(PropsConf propsConf)
    {
        switch (propsConf.type)
        {
            case PropType.GOODS:
                Buff buff = GetBuff(propsConf.buff)[0];
                GameManage.Instance.role.CalculateBuff(buff);
                GameManage.Instance.role.buffList.Add(buff);
                break;
            case PropType.EQUIP: break;
            case PropType.SKILL: break;
        }
    }

    public static int GetDistance(Vector2 pos)
    {
        Vector2 role = GameManage.Instance.role.GetPosition();
        return GetH((int)pos.x, (int)pos.y, (int)role.x, (int)role.y);
    }

    public static int GetH(int posx, int posy, int endposx, int endposy)
    {
        int x = posx;
        int y = posy;
        int endx = endposx;
        int endy = endposy;
        int gapx = Mathf.Abs(endx - x);
        if (gapx % 2 == 0)
        {
            if (endy >= y - gapx / 2 && endy <= y + gapx / 2)
            {
                return gapx;
            }
            else if (endy < y - gapx / 2)
            {
                return gapx + Mathf.Abs(endy - y + gapx / 2);
            }
            else if (endy > y + gapx / 2)
            {
                return gapx + Mathf.Abs(endy - y - gapx / 2);
            }
        }
        else
        {
            if (Mathf.Abs(x) % 2 == 0)
            {
                if (endy >= y - gapx / 2 + 0.5 && endy <= y + gapx / 2 + 0.5)
                {
                    return gapx;
                }
                else if (endy < y - gapx / 2 + 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y + gapx / 2 - 0.5f);
                }
                else if (endy > y + gapx / 2 + 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y - gapx / 2 - 0.5f);
                }
            }
            else
            {
                if (endy >= y - gapx / 2 - 0.5 && endy <= y + gapx / 2 - 0.5)
                {
                    return gapx;
                }
                else if (endy < y - gapx / 2 - 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y + gapx / 2 + 0.5f);
                }
                else if (endy > y + gapx / 2 - 0.5)
                {
                    return gapx + (int)Mathf.Abs(endy - y - gapx / 2 + 0.5f);
                }
            }

        }
        return 0;
    }

    public static Vector2 GetPoint(Vector2 old)
    {
        Vector2 point = old;
        point.x += UnityEngine.Random.Range(-10, 10);
        if (point.x < 0)
            point.x = 0;
        if (point.x >= GameManage.row)
            point.x = GameManage.row - 1;
        point.y += UnityEngine.Random.Range(-10, 10);
        if (point.y < 0)
            point.y = 0;
        if (point.y >= GameManage.row)
            point.y = GameManage.row - 1;
        GetPPoint(point);
        return point;
    }

    public static void GetPPoint(Vector2 point)
    {
        while (true)
        {
            if (GameManage.Instance.mapPoints[(int)point.x][(int)point.y].vaule == 1)
            {
                point.x += UnityEngine.Random.Range(-1, 1);
                if (point.x < 0)
                    point.x = 0;
                if (point.x >= GameManage.row)
                    point.x = GameManage.row - 1;
                point.y += UnityEngine.Random.Range(-1, 1);
                if (point.y < 0)
                    point.y = 0;
                if (point.y >= GameManage.row)
                    point.y = GameManage.row - 1;
                continue;
            }
            return;
        }
    }

    /// <summary>
    /// 创建npc
    /// </summary>
    public static void CreateNPC(List<CharacterConf> charaConf,int x,int y)
    {
        System.Random random = new System.Random(x+y*DateTime.Now.Millisecond);
        int r = random.Next(charaConf.Count - 1);
        GameObject go = Instantiate(Resources.Load(ROLEPATH + charaConf[r].id.ToString())) as GameObject;
        go.GetComponent<NPC>().Create(10, charaConf[r]);
        go.GetComponent<NPC>().SetPosition(x, y);
        go.transform.parent = GameManage.Instance.groundList[x][y].transform;
        go.transform.localPosition = Vector3.zero;
        GameManage.Instance.npcList.Add(go.GetComponent<NPC>());
    }

}
