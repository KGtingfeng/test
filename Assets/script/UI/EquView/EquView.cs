using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquView : UIBaseView
{
    public EquipmentItem helmet;
    public EquipmentItem armor;
    public EquipmentItem shoes;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        Refrsh();
    }

    public void Refrsh()
    {
        helmet.Init(GameManage.Instance.userData.helmet);
        armor.Init(GameManage.Instance.userData.armor);
        shoes.Init(GameManage.Instance.userData.shoes);
    }
}
