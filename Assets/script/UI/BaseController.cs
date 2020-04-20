using UnityEngine;
using UnityEditor;

public class BaseController
{

    public string GetPath()
    {
        PrefabPath path= (PrefabPath)PrefabPath.GetCustomAttribute(this.GetType(), typeof(PrefabPath));
        return path.Path;
    }
}