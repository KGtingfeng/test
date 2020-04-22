using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleView : UIBaseView
{
    public UILabel roleName;
    public UILabel level;
    public UILabel blood;
    public UILabel gas;
    public UILabel speed;
    public UILabel strength;
    public UILabel energy;
    public UILabel roundMove;
    public UILabel roundGas;
    public UILabel exp;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        roleName.text =XMLData.CharacterConfs.Find(a=>a.id== GameManage.Instance.role.id).name;
        level.text ="Lv."+ GameManage.Instance.role.level;
        blood.text =GameManage.Instance.role.blood+"/"+ GameManage.Instance.role.totalBlood;
        gas.text = GameManage.Instance.role.gas + "/" + GameManage.Instance.role.totalGas;
        speed.text = GameManage.Instance.role.speed + "";
        strength.text = GameManage.Instance.role.strength + "";
        energy.text = GameManage.Instance.role.energy + "";
        roundMove.text = GameManage.Instance.role.roundMove + "";
        roundGas.text = GameManage.Instance.role.roundGas + "";
        exp.text= GameManage.Instance.role.EXP + "";
    }
}
