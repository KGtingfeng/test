using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsoleView : UIBaseView
{
    public UIInput input1;
    public UIInput input2;
    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
    }

    public void OnClick1()
    {
        string str = input1.value;
        Debug.LogError(str);
        GameTools.GetPorp(int.Parse(str));
    }

    public void OnClick2()
    {
        string str = input2.value;
        Debug.LogError(str);
        GameTools.GetEqu(int.Parse(str));
    }
}
