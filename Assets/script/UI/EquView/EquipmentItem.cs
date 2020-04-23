using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
public class EquipmentItem : MonoBehaviour
{
    public UILabel equName;
    public UILabel add;
    public UILabel add1;
    public UISprite icon;
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

    public void Init(Equipment equipment)
    {
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
}