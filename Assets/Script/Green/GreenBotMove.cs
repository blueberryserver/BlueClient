using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.IO;
using System.Net.Sockets;
using System;

public class GreenBotMove : State<GreenBot>
{
    private static GreenBotMove _instance = null;

    public static GreenBotMove Instance
    {
        get
        {
            if (_instance == null)
            {
                if (_instance == null)
                {
                    _instance = new GreenBotMove();
                }
            }

            return _instance;
        }
    }

    public override void Enter(GreenBot entity)
    {
        Debug.Log("GreenBotMove.Enter");

    }

    public override void Execute(GreenBot entity)
    {
        Debug.Log("GreenBotMove.Execute");

        entity.Move();

        int groundIndex = GreenGround.Instance.GetGroundIndex(entity.GetPosition());

        if (GreenGround.Instance.GetTileType(groundIndex) == GreenGround.TileType.EMPTY)
        {
            entity.ChangeDelayedState(GreenBotBrightFog.Instance);
            return;
        }

        entity.ChangeDelayedState(GreenBotThink.Instance);
    }

    public override void Exit(GreenBot entity)
    {
        Debug.Log("GreenBotMove.Exit");
    }

    public override bool OnMessage(GreenBot entity, Telegram msg)
    {
        Debug.Log("GreenBotMove.OnMessage");
        
        return false;
    }
}