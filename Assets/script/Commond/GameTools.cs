using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTools : MonoBehaviour
{
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
    

    public static void Damage(Character character,int damage,SkillEffectType skillEffect)
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

                break;
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


    public static void GetPorp()
    {

    }
}
