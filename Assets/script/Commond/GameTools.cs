﻿using System.Collections;
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
        Debug.LogError("Damage   " + damage+"name  "+character.name);
        switch (skillEffect)
        {
            case SkillEffectType.blood:
                if (character.blood - damage <= 0)
                    character.Dead();
                else if (character.blood - damage > character.totalBlood)
                    character.blood = character.totalBlood;
                else
                    character.blood -= damage;
                break;
            case SkillEffectType.gas:
                if (character.gas - damage < 0)
                    character.gas = 0;
                else if (character.gas - damage > character.totalGas)
                    character.gas = character.totalGas;
                else
                    character.gas -= damage;
                break;
        }
        if (skillLevel.buffList != null)
        {
            List<Buff> buffs = GetBuff(skillLevel.buffList);
            if(buffs!=null)
                character.buffList.AddRange(buffs);
        }
        if (character.IsNPC)
            character.GetComponent<AIBase>().IsAttack = true;
    }

    public static List<Buff> GetBuff(string buffList)
    {
        Debug.LogError(buffList);
        string[] bList = buffList.Split('；');
        if (bList.Length == 1)
        {
            return null;

        }
        List<Buff> buffs=new List<Buff>();
        for(int i=0;i< bList.Length-1;i++)
        {
            Debug.LogError(bList[i]);
            string[] list = bList[i].Split('，');
            Buff buff = new Buff();
            if(list.Length>4)
            {
                Debug.LogError("Buff格式错误");
                return null;
            }
            //Debug.LogError(list[0]);
            buff.skillEffectType = (SkillEffectType)int.Parse(list[0]);
            buff.buffName = list[1];
            buff.times = int.Parse(list[2]);
            buff.original = int.Parse(list[3]);
            buffs.Add(buff);
        }
        return buffs;
    }

    /// <summary>
    /// 更换装备
    /// </summary>
    /// <returns></returns>
    public static void ChangeEquipment(Equipment old, Equipment now)
    {
        if (old != null)
        {
            switch (old.Conf.equipmentType)
            {
                case EquipmentType.helmet:
                    GameManage.Instance.role.energy -= old.Conf.equipTypeAdd * old.level;
                    break;
                case EquipmentType.armor:
                    GameManage.Instance.role.strength -= old.Conf.equipTypeAdd * old.level;
                    GameManage.Instance.role.totalBlood -= (old.Conf.equipTypeAdd * old.level)*20;
                    break;
                case EquipmentType.shoes:
                    GameManage.Instance.role.speed -= old.Conf.equipTypeAdd * old.level;
                    break;
            }
        }
        if (now != null)
        {
            switch (now.Conf.equipmentType)
            {
                case EquipmentType.helmet:
                    GameManage.Instance.role.energy += now.Conf.equipTypeAdd * now.level;
                    break;
                case EquipmentType.armor:
                    GameManage.Instance.role.strength += now.Conf.equipTypeAdd * now.level;
                    GameManage.Instance.role.totalBlood += (now.Conf.equipTypeAdd * now.level) * 20;
                    break;
                case EquipmentType.shoes:
                    GameManage.Instance.role.speed += now.Conf.equipTypeAdd * now.level;
                    break;
            }
        }
        if (old != null)
        {
            switch (old.Conf.equipmentEffectType)
            {
                case AtrrType.no: break;
                case AtrrType.blood:
                    GameManage.Instance.role.totalBlood -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
                case AtrrType.roundGas:
                    GameManage.Instance.role.roundGas -= old.Conf.equipEffectTypeAdd * old.color;
                    GameManage.Instance.role.equRoundGas -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
                case AtrrType.energy:
                    GameManage.Instance.role.energy -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
                case AtrrType.speed:
                    GameManage.Instance.role.speed -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
                case AtrrType.strength:
                    GameManage.Instance.role.strength -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
                case AtrrType.moves:
                    GameManage.Instance.role.moves -= old.Conf.equipEffectTypeAdd * old.color;
                    GameManage.Instance.role.equMove -= old.Conf.equipEffectTypeAdd * old.color;
                    break;
            }
        }
        if (now != null)
        {
            switch (now.Conf.equipmentEffectType)
            {
                case AtrrType.no: break;
                case AtrrType.blood:
                    GameManage.Instance.role.totalBlood += now.Conf.equipEffectTypeAdd * now.color;
                    break;
                case AtrrType.roundGas:
                    GameManage.Instance.role.roundGas += now.Conf.equipEffectTypeAdd * now.color;
                    GameManage.Instance.role.equRoundGas += now.Conf.equipEffectTypeAdd * now.color;
                    break;
                case AtrrType.energy:
                    GameManage.Instance.role.energy += now.Conf.equipEffectTypeAdd * now.color;
                    break;
                case AtrrType.speed:
                    GameManage.Instance.role.speed += now.Conf.equipEffectTypeAdd * now.color;
                    break;
                case AtrrType.strength:
                    GameManage.Instance.role.strength += now.Conf.equipEffectTypeAdd * now.color;
                    break;
                case AtrrType.moves:
                    GameManage.Instance.role.moves += now.Conf.equipEffectTypeAdd * now.color;
                    GameManage.Instance.role.equMove += now.Conf.equipEffectTypeAdd * now.color;
                    break;
            }
        }
        if(GameManage.Instance.role.blood> GameManage.Instance.role.totalBlood)
        {
            GameManage.Instance.role.blood = GameManage.Instance.role.totalBlood;
        }
        GameManage.Instance.role.CalculateArr();
    }

    /// <summary>
    /// 物品掉落
    /// </summary>
    /// <param name="level"></param>
    public static void ItemDrop(int level)
    {
        System.Random random = new System.Random(DateTime.Now.Millisecond);
        int r = random.Next(99);
        if (r >= 0 && r < 20)
        {
            GetEqu(level);
        }
        else if (r >= 20 && r < 70)
        {
            List<PropsConf> confs = XMLData.PropsConfs.FindAll(a => a.id < 4500);
            r = random.Next(confs.Count - 1);
            GetPorp(confs[r].id);
        }
        else
        {
            List<PropsConf> confs = XMLData.PropsConfs.FindAll(a => a.id > 4500);
            r = random.Next(confs.Count - 1);
            GetPorp(confs[r].id);
        }
    }

    public static void GetEqu(int level)
    {
        Debug.LogError("GetEqu  " + level);
        System.Random random = new System.Random(DateTime.Now.Millisecond);
        Equipment equipment = new Equipment();
        int r = random.Next(-1, 2);
        if ((level + r) <= 0)
        {
            equipment.level = 1;
        }else if((level + r) > 20)
        {
            equipment.level = 20;
        }
        else
        {
            equipment.level = level + r;
        }
        r = random.Next(XMLData.EquipmentConfs.Count-1);
        equipment.Conf = XMLData.EquipmentConfs[r];
        r = random.Next(4);
        equipment.color = r;
        GameManage.Instance.userData.equipments.Add(equipment);
    }

    public static void GetPorp(int id,int num=1)
    {
        if(XMLData.PropsConfs.Find(a => a.id == id) == null)
        {
            UIManage.CreateTips("不存在该物品!");
        }
        if (GameManage.Instance.userData.props.Find(a => a.conf.id == id) != null)
        {
            GameManage.Instance.userData.props.Find(a => a.conf.id == id).num+=num;
        }
        else
        {
            Prop prop = new Prop();
            prop.conf = XMLData.PropsConfs.Find(a => a.id == id);
            prop.num = num;
            GameManage.Instance.userData.props.Add(prop);
        }
    }

    public static bool UseProp(PropsConf propsConf)
    {
        switch (propsConf.type)
        {
            case PropType.GOODS:
                Buff buff = GetBuff(propsConf.buff)[0];
                GameManage.Instance.role.CalculateBuff(buff);
                GameManage.Instance.role.buffList.Add(buff);
                UIManage.CreateTips("使用物品成功！");
                MainView.Instance.Change();
                return true;
            case PropType.SKILL:
                if(GameManage.Instance.userData.skills.Find(a=>a.id== int.Parse(propsConf.buff))==null)
                {
                    Skill skill = new Skill(int.Parse(propsConf.buff),1);
                    GameManage.Instance.userData.skills.Add(skill);
                    UIManage.CreateTips("学习技能成功！");
                    return true;
                }
                else
                {
                    UIManage.CreateTips("该技能已学习！");
                    return false;
                }
        }
        return false;
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
        Vector2 point=new Vector2();
        System.Random random = new System.Random();
        int x = random.Next(-10, 10);
        int y = random.Next(-10, 10);
        if (old.x + x < 0)
        {
            point.x = 0;
        }else if(old.x + x > GameManage.col)
        {
            point.x = GameManage.col-1;
        }
        if (old.y + y < 0)
        {
            point.y = 0;
        }
        else if (old.y + y > GameManage.col)
        {
            point.y = GameManage.col - 1;
        }
        return GetPPoint(point);
    }

    public static Vector2 GetPPoint(Vector2 point)
    {
        //Debug.LogError(GameManage.Instance.mapPoints[(int)point.x][(int)point.y].vaule);
        int count = 0;
        while (true)
        {
            if (GameManage.Instance.mapPoints[(int)point.x][(int)point.y].vaule == 1)
            {
                if (count % 2 == 0)
                    point.x += 1;
                else
                    point.y += 1;
                count++;
                continue;
            }
            return point;
        }
    }

    /// <summary>
    /// 创建npc
    /// </summary>
    public static void CreateNPC(List<CharacterConf> charaConf,int x,int y,Transform parent,int level)
    {
        System.Random random = new System.Random(x+y*DateTime.Now.Millisecond);
        int r = random.Next(charaConf.Count - 1);
        GameObject go = Instantiate(Resources.Load(ROLEPATH + charaConf[r].id.ToString())) as GameObject;
        go.GetComponent<NPC>().Create(level, charaConf[r]);
        go.GetComponent<NPC>().x = x;
        go.GetComponent<NPC>().y = y;
        go.transform.parent = parent;
        go.transform.localPosition = Vector3.zero;
        GameManage.Instance.npcList.Add(go.GetComponent<NPC>());
        parent.GetComponent<Ground>().character = go.GetComponent<NPC>();
    }

    public static void DeleteAllChild(Transform grid)
    {
        if (grid.transform.childCount > 0)
        {
            for (int i = 0; i < grid.transform.childCount; i++)
            {
                grid.GetComponent<UIGrid>().RemoveChild(grid.transform.GetChild(i));
                Destroy(grid.transform.GetChild(i).gameObject);
            }
            grid.transform.DetachChildren();
        }
        grid.GetComponent<UIGrid>().repositionNow = true;
    }
}
