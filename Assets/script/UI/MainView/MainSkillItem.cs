using UnityEngine;
using UnityEditor;

public class MainSkillItem : MonoBehaviour
{
    public UISprite icon;
    public int num;
    public void Change(Skill skill)
    {
        icon.gameObject.SetActive(true);
        icon.spriteName = skill.id + "";
        
        GameManage.Instance.userData.skill[num] = skill;
    }
}