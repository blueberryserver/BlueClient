using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class ControlPanel : MonoBehaviour
{       
    Text _textDisplay = null;
    string _textDisplayUpdate = "";
    Image _panelImage = null;
    Color _panelImageColor = Color.red;
    List<InputField> _inputFieldList = new List<InputField>();
    
    void Start()
    {

    }

    void Update()
    {
        if (_textDisplay)
        {
            _textDisplay.text = _textDisplayUpdate;
        }

        if (_panelImage == null)
        {
            _panelImage = GetComponent<Image>();
        }
        else
        {
            _panelImage.color = _panelImageColor;
        }
    }
    
    public void SetTextDisplay(Text textDisplay)
    {
        _textDisplay = textDisplay;
    }

    public void SetTextDisplayUpdate(string text)
    {
        _textDisplayUpdate = text;
    }

    public void SetPanelImageColor(Color color)
    {
        _panelImageColor = color;
    }

    public void AddInputField(InputField inputField)
    {
        _inputFieldList.Add(inputField);
    }

    public string GetInputFieldText(string name)
    {
        foreach (InputField inputFIend in _inputFieldList)
        {
            if (inputFIend.name == name)
            {
                return inputFIend.text;
            }
        }

        return null;
    }
}