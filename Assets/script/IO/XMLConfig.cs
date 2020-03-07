using UnityEngine;
using System.Collections;
using System.Xml;

public abstract class XMLConfig 
{
    public abstract void Read(XmlNode item);
    

    public string GetString(XmlNode item, string name)
    {
        return item.SelectSingleNode(name).InnerText;
    }

    public int GetInt(XmlNode item, string name)
    {
        return int.Parse(item.SelectSingleNode(name).InnerText);
    }

    public bool GetBool(XmlNode item, string name)
    {
        return bool.Parse(item.SelectSingleNode(name).InnerText);
    }

    public float GetFloat(XmlNode item, string name)
    {
        return float.Parse(item.SelectSingleNode(name).InnerText);
    }

}
