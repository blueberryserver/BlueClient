using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenSimulator : MonoBehaviour
{
    void Start()
    {
        // Ground
        GreenGround.Instance.Init(4, 4, 10, 10);

        // GreenBot
        int botCount = 1;
        int id = 0;

        for (int i = 0; i < botCount; i++)
        {
            GreenBot bot = EntityManager.Instance.AddEntity<GreenBot>(id);
            bot.ChangeDelayedState(GreenBotConnect.Instance);
            id++;
        }
    }
}

