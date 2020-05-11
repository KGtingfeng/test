using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquView : UIBaseView
{
    public static EquView Instance;
    public EquipmentItem helmet;
    public EquipmentItem armor;
    public EquipmentItem shoes;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        Instance = this;
        Refrsh();
    }

    public void Refrsh()
    {
        helmet.Init(GameManage.Instance.userData.helmet,EquipmentType.helmet);
        armor.Init(GameManage.Instance.userData.armor, EquipmentType.armor);
        shoes.Init(GameManage.Instance.userData.shoes, EquipmentType.shoes);
    }
}
