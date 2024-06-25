using System.IO;
using System.Xml;
using UnityEngine;

public class XMLSystem : IXMLSystem
{
    private readonly XMLSourceConfig _xmlSourceConfig;
    private readonly string _savePath;

    private const string SaveNodeName = "save_file";
    private const string SaveElementName = "score";
    
    public XMLSystem(
        XMLSourceConfig xmlSourceConfig)
    {
        _xmlSourceConfig = xmlSourceConfig;
        _savePath = GenerateSavePath();
    }
    
    public void SaveToXML(string score)
    {
        var xmlDoc = new XmlDocument();
        var rootNode = xmlDoc.CreateElement(SaveNodeName);
        xmlDoc.AppendChild(rootNode);

        var elem = xmlDoc.CreateElement(SaveElementName);
        
        elem.SetAttribute("value", score);

        rootNode.AppendChild(elem);

        xmlDoc.Save(_savePath + _xmlSourceConfig.SaveName);
    }

    public string LoadFromXML()
    {
        var loadPath = _savePath + _xmlSourceConfig.SaveName;
        if (!File.Exists(loadPath))
        {
            Debug.LogWarning("Save file doesn't exist at load path, player data unchanged!");
            SaveToXML("0");
        }
            
        var xmlDoc = new XmlDocument();
        xmlDoc.Load(loadPath);

        var node = xmlDoc.SelectSingleNode($"/{SaveNodeName}/{SaveElementName}");
        if (node == null)
        {
            Debug.LogError("Save data is missing in save file, player data unchanged!");
            return null;
        }
            
        return node.Attributes[0].Value;
    }
        
    private string GenerateSavePath()
    {
        var computerPath = Application.dataPath + _xmlSourceConfig.SourcePath;
        var phonePath = Application.persistentDataPath;
        
        #if UNITY_ANDROID
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
        #elif UNITY_IPHONE
        phonePath += "/";
        if (!Directory.Exists(phonePath))
        {
            Directory.CreateDirectory(phonePath);
        }
        return phonePath;
        #elif UNITY_EDITOR
        computerPath = Application.dataPath;
        computerPath = computerPath.Remove(computerPath.IndexOf("/Assets"), 7);
        computerPath += _xmlSourceConfig.SourcePath;
        if (!Directory.Exists(computerPath))
        {
            Directory.CreateDirectory(computerPath);
        }
        return computerPath;
        #else
        if (!Directory.Exists(computerPath))
        {
            Directory.CreateDirectory(computerPath);
        }
        return computerPath;
        #endif
    }
}
