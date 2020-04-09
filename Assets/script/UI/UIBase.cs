using UnityEngine;
using UnityEditor;

public abstract class UIBase : MonoBehaviour
{
    public abstract void InitView();
    public virtual void Destory() { }
}