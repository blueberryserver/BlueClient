using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBotBrightFog : State<GreenBot>
{
    private static GreenBotBrightFog _instance = null;

    public static GreenBotBrightFog Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GreenBotBrightFog();
                }
            }

            return _instance;
        }
    }

    public override void Enter(GreenBot entity)
    {
        Debug.Log("GreenBotBrightFog.Enter");

    }

    public override void Execute(GreenBot entity)
    {
        Debug.Log("GreenBotBrightFog.Execute");

        int groundIndex = GreenGround.Instance.GetGroundIndex(entity.GetPosition());
        GreenGround.Instance.ChangeTileType(groundIndex, GreenGround.TileType.MARKED);
        GreenGround.Instance.SetGroundColor(groundIndex, Color.white);
        entity.SetReward(1.0f);

        entity.ChangeDelayedState(GreenBotThink.Instance);
    }

    public override void Exit(GreenBot entity)
    {
        Debug.Log("GreenBotBrightFog.Exit");
    }

    public override bool OnMessage(GreenBot entity, Telegram msg)
    {
        Debug.Log("GreenBotBrightFog.OnMessage");
        
        return false;
    }
}