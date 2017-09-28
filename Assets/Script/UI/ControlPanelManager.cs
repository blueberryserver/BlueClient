using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelManager : MonoBehaviour
{
    Dictionary<int, ControlPanel> _controlPanelDict = new Dictionary<int, ControlPanel>();

    private static ControlPanelManager _instance = null;

    public static ControlPanelManager Instance
    {
        get
        {
            if (_instance == null)
            {
                //GameObject gameObj = new GameObject("ControlPanelManager");
                Rect rect = new Rect(Screen.width / 2.0f, Screen.height / 2.0f, Screen.width, Screen.height);
                GameObject gameObj = UIFactory.Instance.CreateCanvas(null, "ControlPanelManager", rect);
                _instance = gameObj.AddComponent<ControlPanelManager>();

                if (_instance == null)
                {
                    _instance = new ControlPanelManager();
                }
            }

            return _instance;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public ControlPanel CreateControlPanel(Transform parent, string name, Rect rect)
    {
        GameObject panel = UIFactory.Instance.CreatePanel(parent, name, rect);
        ControlPanel controlPanel = panel.AddComponent<ControlPanel>();

        return controlPanel;
    }

    public ControlPanel AddControlPanel(int id, Transform parent, string name, Rect rect)
    {
        GameObject panel = UIFactory.Instance.CreatePanel(parent, name, rect);
        ControlPanel controlPanel = panel.AddComponent<ControlPanel>();

        _controlPanelDict.Add(id, controlPanel);

        return controlPanel;
    }

    public ControlPanel FindControlPanel(int id)
    {
        if (_controlPanelDict.ContainsKey(id) == false)
        {
            return null;
        }

        return _controlPanelDict[id];
    }

    public int FindKey(ControlPanel value)
    {
        foreach (KeyValuePair<int, ControlPanel> pair in _controlPanelDict)
        {
            if (pair.Value == value)
            {
                return pair.Key;
            }
        }

        return -1;
    }
}
