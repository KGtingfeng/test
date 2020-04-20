using UnityEngine;
using System.Collections;
using System;

public class PrefabPath : Attribute
{
    public PrefabPath(string path)
    {
        this.Path = path;
    }
    public string Path { get; set; }
}
