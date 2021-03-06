﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class MainView : UIBaseView
{
    public static MainView Instance;
    public UILabel round;
    public GameObject guai;
    public GameObject guaiBlood;
    public GameObject guaiGas;
    public UISprite guaiIcon;
    public GameObject blood;
    public GameObject gas;
    public UILabel moves;
    public GameObject waitPanel;

    public override void InitView(BaseController controller)
    {
        base.InitView(controller);
        Instance = this;
    }

    public void HideNpc()
    {
        guai.SetActive(false);
    }

    public void GetNpc(Transform go)
    {
        NPC npc = go.GetComponent<NPC>();
        float blood = (float)npc.blood / npc.totalBlood;
        float gas = (float)npc.gas / npc.totalGas;
        guaiBlood.transform.Find("Sprite").GetComponent<UIProgressBar>().value = blood;
        guaiBlood.transform.Find("Label").GetComponent<UILabel>().text = npc.blood + "/" + npc.totalBlood;
        guaiGas.transform.Find("Sprite").GetComponent<UIProgressBar>().value = gas;
        guaiGas.transform.Find("Label").GetComponent<UILabel>().text = npc.gas + "/" + npc.totalGas;
        guai.SetActive(true);
    }

    public void Change()
    {
        ChangeBlood();
        ChangeGas();
        ChangeMoves();
        round.text = GameManage.Instance.round + "";
    }

    public void ChangeBlood()
    {
        float value=(float)GameManage.Instance.role.blood/ GameManage.Instance.role.totalBlood;
        //Debug.LogError(value);
        blood.transform.Find("Sprite").GetComponent<UIProgressBar>().value= value;
        blood.transform.Find("Label").GetComponent<UILabel>().text = GameManage.Instance.role.blood + "/" + GameManage.Instance.role.totalBlood;
    }

    public void ChangeGas()
    {
        float value;
        if (GameManage.Instance.role.gas == 0)
        {
            value = 0;
        }
        else
        {
            value = (float)GameManage.Instance.role.gas / GameManage.Instance.role.totalGas;
        }
        gas.transform.Find("Sprite").GetComponent<UIProgressBar>().value = value;
        gas.transform.Find("Label").GetComponent<UILabel>().text = GameManage.Instance.role.gas + "/" + GameManage.Instance.role.totalGas;
    }

    public void ChangeMoves()
    {
        moves.text = "" + GameManage.Instance.role.roundMove;
    }

    public void ShowWait()
    {
        waitPanel.SetActive(true);
    }

    public void HideWait()
    {
        waitPanel.SetActive(false);
    }
}
