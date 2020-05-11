using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class EquipmentItem : MonoBehaviour
{
    
    public UILabel equName;
    public UILabel add;
    public UILabel add1;
    public UISprite icon;
    public EquipmentType type;

    Dictionary<int, string> color = new Dictionary<int, string>() {
        {0,"" },
        {1,"[00ff00]" },
        {2,"[0000ff]" },
        {3,"[ff0000]" },

    };

    Dictionary<EquipmentType, string> equType = new Dictionary<EquipmentType, string>() {
        {EquipmentType.helmet,"能量+" },
        {EquipmentType.armor,"体力+" },
        {EquipmentType.shoes,"速度+" },
    };

    Dictionary<AtrrType, string> atrrType = new Dictionary<AtrrType, string>() {
        {AtrrType.blood,"血量" },
        {AtrrType.energy,"能量+" },
        {AtrrType.moves,"移动值+" },
        {AtrrType.roundGas,"每回合加气+" },
        {AtrrType.speed,"速度+" },
        {AtrrType.strength,"体力+" },
    };

    public void Init(Equipment equipment, EquipmentType type)
    {
        this.type = type;

        if (equipment == null)
        {
            equName.gameObject.SetActive(false);
            add.gameObject.SetActive(false);
            add1.gameObject.SetActive(false);
            icon.gameObject.SetActive(false);
        }
        else
        {
            equName.text = color[equipment.color]+ equipment.Conf.equName;
            add.text = equType[equipment.Conf.equipmentType] + (equipment.Conf.equipTypeAdd * equipment.level);
            if (equipment.Conf.equipmentEffectType != AtrrType.no)
            {
                add1.text = atrrType[equipment.Conf.equipmentEffectType] + (equipment.Conf.equipEffectTypeAdd * equipment.color);
                add1.gameObject.SetActive(true);
            }
            else
                add1.gameObject.SetActive(false);
            icon.spriteName = equipment.Conf.id + "";

            equName.gameObject.SetActive(true);
            add.gameObject.SetActive(true);
            icon.gameObject.SetActive(true);
        }
            
    }

    public bool ChangeEqu(Equipment equipment)
    {
        if(type== equipment.Conf.equipmentType)
        {
            if (equipment.level <= GameManage.Instance.role.level)
            {
                switch (type)
                {
                    case EquipmentType.armor:
                        GameTools.ChangeEquipment(GameManage.Instance.userData.armor, equipment);
                        GameManage.Instance.userData.armor = equipment;                       
                        break;
                    case EquipmentType.helmet:
                        GameTools.ChangeEquipment(GameManage.Instance.userData.helmet, equipment);
                        GameManage.Instance.userData.helmet = equipment;
                        break;
                    case EquipmentType.shoes:
                        GameTools.ChangeEquipment(GameManage.Instance.userData.shoes, equipment);
                        GameManage.Instance.userData.shoes = equipment;
                        break;
                }
                GameManage.Instance.userData.equipments.Remove(equipment);
                EquView.Instance.Refrsh();
                return true;
            }
            UIManage.CreateTips("角色等级不足！");
            return false;
        }
        else
        {
            UIManage.CreateTips("装备类型不对！");
            return false;
        }
    }
}