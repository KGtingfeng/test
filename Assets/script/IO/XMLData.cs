using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

public class XMLData 
{

    static List<SkillConf> skillConfigs;
    public static List<SkillConf> SkillConfigs
    {
        get
        {
            if (skillConfigs == null)
            {
                skillConfigs = GetAll<SkillConf>();
            }
            return skillConfigs;
        }
        
    }


    /// <summary>
    /// xml地址
    /// </summary>
    private static string xmlPath="XML/";
    /// <summary>
    /// 读取XML
    /// </summary>
    /// <returns></returns>
    public static  List<T> GetAll<T>() where T: XMLConfig
    {
        List<T> servers = new List<T>();
        string file = typeof(T).Name;

        string path = xmlPath + file;

        TextAsset textAsset = Resources.Load<TextAsset>(path);
        Debug.LogError("file " + file + "   is open ");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);

        XmlNode node = xml.SelectSingleNode("RECORDS");

        XmlNodeList list = node.ChildNodes;

        foreach (XmlNode item in list)
        {
            servers.Add((T)ReadObj(item,typeof(T)));
        }

        return servers;
    }

    public static object ReadObj(XmlNode item,Type c)
    {
        object o = Activator.CreateInstance(c);
        XMLConfig xMLConfig = (XMLConfig)o;
        xMLConfig.Read(item);
        return xMLConfig;

    }
    
}
