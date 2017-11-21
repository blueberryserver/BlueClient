using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGround : MonoBehaviour
{
    public enum TileType
    {
        UNKNOWN = -2,
        OBSTACLE = -1,
        EMPTY = 100,
        MARKED = 200,
        PLAYER = 300,
    }

    public class Tile
    {
        public TileType _tileType = TileType.EMPTY;
        public Rect _tileSize = Rect.zero;
        public GameObject _tileObj = null;
    }

    static GameObject _groundPrefab = null;
    //Rect[] _groundArray = null;
    //GameObject[] _groundObjArray = null;
    List<Tile> _ground = new List<Tile>();
    int _groundWidthCount = 0;
    int _groundHeightCount = 0;
    float _groundUnitWidth = 0.0f;
    float _groundUnitHeight = 0.0f;

    private static GreenGround _instance = null;

    public static GreenGround Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject gameObj = new GameObject("GreenGround");
                _instance = gameObj.AddComponent<GreenGround>();

                if (_instance == null)
                {
                    _instance = new GreenGround();
                }
            }

            return _instance;
        }
    }

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int widthCount, int heightCount, float unitWidth, float unitHeight)
    {
        _groundWidthCount = widthCount;
        _groundHeightCount = heightCount;
        _groundUnitWidth = unitWidth;
        _groundUnitHeight = unitHeight;

        if (_groundPrefab == null)
        {
            _groundPrefab = (GameObject)Resources.Load("Prefab/Ground", typeof(GameObject));
        }

        int totalGroundCount = _groundWidthCount * _groundHeightCount;
        float groundWidth = _groundUnitWidth * _groundWidthCount;
        float groundHeight = _groundUnitHeight * _groundHeightCount;

        //_ground. = new Tile[totalGroundCount];
        //_ground
        //_groundArray = new Rect[totalGroundCount];
        //_groundObjArray = new GameObject[totalGroundCount];
        float initx = (-groundWidth + _groundUnitWidth) / 2.0f;
        float initz = (groundHeight - _groundUnitHeight) / 2.0f;
        for (int i = 0; i < totalGroundCount; i++)
        {
            float posx = initx + _groundUnitWidth * (i % _groundWidthCount);
            float posz = initz - _groundUnitHeight * (i / _groundHeightCount);

            Tile tile = new Tile();
            tile._tileSize = new Rect(posx, posz, _groundUnitWidth, _groundUnitHeight);
            tile._tileObj = Instantiate(_groundPrefab, new Vector3(posx, 0.0f, posz), Quaternion.identity);
            tile._tileObj.GetComponent<Renderer>().material.color = Color.black;
            _ground.Add(tile);
            //_groundArray[i] = new Rect(posx, posz, _groundUnitWidth, _groundUnitHeight);
            //_groundObjArray[i] = Instantiate(_groundPrefab, new Vector3(posx, 0.0f, posz), Quaternion.identity);
            //_groundObjArray[i].GetComponent<Renderer>().material.color = Color.black;
        }
    }

    public int GetTotalGroundCount()
    {
        return _groundWidthCount * _groundHeightCount;
    }

    public float GetTotalGroundWidth()
    {
        return _groundUnitWidth * _groundWidthCount;
    }

    public float GetTotalGroundHeight()
    {
        return _groundUnitHeight * _groundHeightCount;
    }

    public int GetGroundIndex(Vector3 pos)
    {
        float x = pos.x;
        float z = pos.z;

        for (int i = 0; i < _ground.Count; i++)
        {
            Rect rect = _ground[i]._tileSize;
            float left = rect.x - (rect.width / 2.0f);
            float right = rect.x + (rect.width / 2.0f);
            float top = rect.y + (rect.height / 2.0f);
            float bottom = rect.y - (rect.height / 2.0f);
            if (left <= x && bottom <= z &&
                x <= right && z <= top)
            {
                return i;
            }
        }

        return -1;
    }

    public void SetGroundColor(int groundIndex, Color color)
    {
        if (_ground.Count <= groundIndex)
        {
            Debug.Log("out of range");
            return;
        }

        _ground[groundIndex]._tileObj.GetComponent<Renderer>().material.color = color;
    }

    public TileType GetTileType(int groundIndex)
    {
        //int groundIndex = GetCurrentGroundIndex(position);

        if (groundIndex == -1)
        {
            //if (_ground[groundIndex]._tileType == TileType.EMPTY)
            //{
            //    return _ground[groundIndex]._tileType;
            //}

            return TileType.UNKNOWN;
        }

        return _ground[groundIndex]._tileType; 
    }

    public void ChangeTileType(int groundIndex, TileType tileType)
    {
        //int groundIndex = GetCurrentGroundIndex(position);

        if (groundIndex == -1)
        {
            return;
        }

        if (_ground[groundIndex]._tileType != tileType)
        {
            _ground[groundIndex]._tileType = tileType;
            //SetGroundColor(groundIndex, Color.white);
        }
    }

    public bool IsOutOfGround(Vector3 pos)
    {
        float totalWidth = _groundUnitWidth * (float)_groundWidthCount;
        float totalHeight = _groundUnitHeight * (float)_groundHeightCount;
        float halfWidth = totalWidth / 2.0f;
        float halfHeight = totalHeight / 2.0f;

        float x = pos.x;
        float y = pos.z;

        if (-halfWidth <= x && x <= halfWidth &&
            -halfHeight <= y && y <= halfHeight)
        {
            return false;
        }

        return true;
    }

    public List<TileType> GetTileTypeList()
    {
        List<TileType> tileTypeList = new List<TileType>();
        foreach (Tile tile in _ground)
        {
            tileTypeList.Add(tile._tileType);
        }

        return tileTypeList;
    }

    public void ResetGround()
    {
        for (int i = 0; i < _ground.Count; i++)
        {
            ChangeTileType(i, TileType.EMPTY);
            SetGroundColor(i, Color.black);
        }
    }
}
