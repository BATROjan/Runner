using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IXMLSystem
{
    void SaveToXML(string score);
    string LoadFromXML();
}
