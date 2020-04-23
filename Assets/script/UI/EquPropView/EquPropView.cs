using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquPropView : UIBaseView
{
    public GameObject grid;
    public GameObject item;

    public UILabel propName;
    public UILabel des;
    public UILabel des1;
    public UISprite icon;
    public UILabel level;
    public GameObject useItem;
    Equipment equ;

    Dictionary<EquipmentType, string> equType = new Dictionary<EquipmentType, string>() {
        {EquipmentType.helmet,"能量+" },
        {EquipmentType.armor,"体力+" },
        {EquipmentType.shoes,"速度+" },
    };

    Dictionary<AtrrType, string> atrrType = new Dictionary<AtrrType, string>() {
        {AtrrType.blood,"血量+" },
        {AtrrType.energy,"能量+" },
        {AtrrType.moves,"移动值+" },
        {AtrrType.roundGas,"每回合加气+" },
        {AtrrType.speed,"速度+" },
        {AtrrType.strength,"体力+" },
    };

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        CreateItem(GameManage.Instance.userData.equipments);
    }

    public void CreateItem(List<Equipment> props)
    {
        foreach (Equipment p in props)
        {
            GameObject go = NGUITools.AddChild(grid, item);
            go.GetComponent<EquItem>().Init(p);
            UIEventListener.Get(go).onClick = OnClickItem;
            UIEventListener.Get(go).parameter = p;
            go.SetActive(true);
        }
        grid.GetComponent<UIGrid>().Reposition();
    }

    public void OnClickItem(GameObject go)
    {
        equ = (Equipment)UIEventListener.Get(go).parameter;
        propName.text = equ.Conf.equName;
        des.text = equType[equ.Conf.equipmentType]+ (equ.Conf.equipTypeAdd*equ.level);
        if (equ.Conf.equipmentEffectType != AtrrType.no)
        {
            des1.text = atrrType[equ.Conf.equipmentEffectType] + (equ.Conf.equipEffectTypeAdd * equ.color);
            des1.gameObject.SetActive(true);
        }
        else
            des1.gameObject.SetActive(false);

        icon.spriteName = "" + equ.Conf.id;
        level.text = "Lv." + equ.level;
        useItem.SetActive(true);

    }

}

