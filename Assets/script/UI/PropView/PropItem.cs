using UnityEngine;
using UnityEditor;

public class PropItem : MonoBehaviour
{
    public UILabel nameLabel;
    public UISprite sprite;
    public UILabel num;

    public void Init(Prop p)
    {
        nameLabel.text = p.conf.propsName;
        sprite.spriteName = p.conf.id + "";
        num.text = "" + p.num;
    }
}