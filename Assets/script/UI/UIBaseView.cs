using UnityEngine;
using UnityEditor;

public class UIBaseView : MonoBehaviour 
{
    public BaseController controller;
    public virtual void InitView(BaseController controller)
    {
        this.controller = controller;
    }
    public virtual void CloseView() {
        Destroy(gameObject);
    }
}