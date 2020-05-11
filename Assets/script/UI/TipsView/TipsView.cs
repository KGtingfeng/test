using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipsView : UIBaseView
{
    public UILabel tips;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
    }

    public void Tips(string tip)
    {
        tips.text = tip;
    }
}
