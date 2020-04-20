using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleItem : MonoBehaviour
{
    public UILabel nameLabel;
    public UISprite sprite;
    public UILabel num;
    public UILabel num1;
    public UILabel num2;
    public UILabel num3;

    public void Init(CharacterConf conf)
    {
        nameLabel.text = conf.name;
        sprite.spriteName = conf.id + "";
        num.text = conf.strength + "";
        num1.text = conf.speed + "";
        num2.text = conf.energy + "";
        num3.text = conf.blood + "";
    }
}
