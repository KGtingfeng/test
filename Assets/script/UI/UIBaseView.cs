using UnityEngine;
using UnityEditor;

public class UIBaseView : MonoBehaviour 
{
    BaseController controller;
    public virtual void InitView(BaseController controller)
    {
        this.controller = controller;
    }
    public virtual void Destory() { }
}