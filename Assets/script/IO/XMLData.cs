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

    static List<SkillLevelConf> skillLevelConfs;
    public static List<SkillLevelConf> SkillLevelConfs
    {
        get
        {
            if (skillLevelConfs == null)
            {
                skillLevelConfs = GetAll<SkillLevelConf>();
            }
            return skillLevelConfs;
        }

    }

    static List<EquipmentConf> equipmentConfs;
    public static List<EquipmentConf> EquipmentConfs
    {
        get
        {
            if (equipmentConfs == null)
            {
                equipmentConfs = GetAll<EquipmentConf>();
            }
            return equipmentConfs;
        }

    }


    static List<CharacterConf> characterConfs;
    public static List<CharacterConf> CharacterConfs
    {
        get
        {
            if (characterConfs == null)
            {
                characterConfs = GetAll<CharacterConf>();
            }
            return characterConfs;
        }

    }

    static List<MapConf> mapConfs;
    public static List<MapConf> MapConfs
    {
        get
        {
            if (mapConfs == null)
            {
                mapConfs = GetAll<MapConf>();
            }
            return mapConfs;
        }

    }

    static List<PropsConf> propsConfs;
    public static List<PropsConf> PropsConfs
    {
        get
        {
            if (propsConfs == null)
            {
                propsConfs = GetAll<PropsConf>();
            }
            return propsConfs;
        }

    }

    static List<TalentConf> talentConfs;
    public static List<TalentConf> TalentConfs
    {
        get
        {
            if (talentConfs == null)
            {
                talentConfs = GetAll<TalentConf>();
            }
            return talentConfs;
        }
    }

    static List<GameData> gameData;
    public static List<GameData> GameDatas
    {
        get
        {
            if (gameData == null)
            {
                gameData = GetGameData<GameData>();
            }
            return gameData;
        }
    }

    static List<EXPConf> eXPConfs;
    public static List<EXPConf> EXPConfs
    {
        get
        {
            if (eXPConfs == null)
            {
                eXPConfs = GetAll<EXPConf>();
            }
            return eXPConfs;
        }
    }

    #region xml读取
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

    public static List<T> GetGameData<T>() where T : XMLConfig
    {
        List<T> servers = new List<T>();
        string file = typeof(T).Name;
        string path = xmlPath + file;
        if(!File.Exists(path))
        {
            File.Create("Assets/Resources/" + path + ".xml").Dispose();
            XmlDocument newxml = new XmlDocument();
            XmlDeclaration xmldecl = newxml.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            newxml.AppendChild(xmldecl);
            XmlElement root = newxml.CreateElement("RECORDS");//创建根节点
            XmlElement info = newxml.CreateElement("RECORD");//创建子节点
            XmlElement score = newxml.CreateElement("score");
            score.InnerText = "0";
            XmlElement talent = newxml.CreateElement("talent");
            info.AppendChild(score);
            info.AppendChild(talent);
            root.AppendChild(info);
            newxml.AppendChild(root);
            newxml.Save("Assets/Resources/"+path + ".xml");
            GameData gameData = new GameData();
            gameData.score = 0;
            object o = gameData;
            servers.Add((T)o);
            return servers;
        }
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        Debug.LogError("file " + file + "   is open ");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlNode node = xml.SelectSingleNode("RECORDS");
        XmlNodeList list = node.ChildNodes;

        foreach (XmlNode item in list)
        {
            servers.Add((T)ReadObj(item, typeof(T)));
        }
        return servers;
    }

    public static void SetGameData(int score,string talent)
    {
        string file = "gamedata";
        string path = xmlPath + file;
        TextAsset textAsset = Resources.Load<TextAsset>(path);
        //Debug.LogError("file " + file + "   is open ");
        XmlDocument xml = new XmlDocument();
        xml.LoadXml(textAsset.text);
        XmlNode node = xml.SelectSingleNode("RECORDS");
        XmlNodeList list = node.ChildNodes;
        GameDatas[0].score = score;
        GameDatas[0].talent = talent;
        foreach (XmlNode item in list)
        {
            item.SelectSingleNode("score").InnerText = score + "";
            item.SelectSingleNode("talent").InnerText = talent;
        }
    }

    public static object ReadObj(XmlNode item,Type c)
    {
        object o = Activator.CreateInstance(c);
        XMLConfig xMLConfig = (XMLConfig)o;
        xMLConfig.Read(item);
        return xMLConfig;
    }
    #endregion
}
