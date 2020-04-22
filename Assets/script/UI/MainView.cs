using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainView : UIBaseView
{
    public List<UISprite> Skill;
    public GameObject guai;
    public GameObject blood;
    public GameObject gas;
    public UILabel moves;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);

    }


    public void ChangeBlood()
    {
        float value=GameManage.Instance.role.totalBlood/ GameManage.Instance.role.blood;
        blood.transform.Find("Sprite").GetComponent<UIProgressBar>().value= value;
        blood.transform.Find("Label").GetComponent<UILabel>().text = GameManage.Instance.role.blood + "/" + GameManage.Instance.role.totalBlood;
    }

    public void ChangeGas()
    {
        float value = GameManage.Instance.role.totalGas / GameManage.Instance.role.gas;
        gas.transform.Find("Sprite").GetComponent<UIProgressBar>().value = value;
        gas.transform.Find("Label").GetComponent<UILabel>().text = GameManage.Instance.role.gas + "/" + GameManage.Instance.role.totalGas;
    }

    public void ChangeMoves()
    {
        moves.text = "" + GameManage.Instance.role.moves;
    }
}
