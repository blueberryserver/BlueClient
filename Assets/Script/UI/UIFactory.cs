using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UIFactory : MonoBehaviour
{
    private static UIFactory _instance = null;

    public static UIFactory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(UIFactory)) as UIFactory;
                if (_instance == null)
                {
                    //GameObject prefab = Resources.Load("UIManager") as GameObject;
                    //GameObject obj = Instantiate(prefab) as GameObject;

                    _instance = new UIFactory();

                    //Destroy( prefab );
                    //prefab = null;
                }
            }

            return _instance;
        }
    }

    public GameObject CreateText(Transform parent, string name, string contents)
    {
        GameObject textObject = new GameObject(name);
        textObject.AddComponent<CanvasRenderer>();
        Text text = textObject.AddComponent<Text>();
        text.text = contents;
        text.alignment = TextAnchor.MiddleCenter;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");// UnityEditor.AssetDatabase.GetBuiltinExtraResource<Font>("UI/Skin/Arial.psd");
        text.color = Color.black;
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.anchoredPosition = Vector2.one * 0.5f;
        rectTransform.localPosition = Vector2.zero;
        rectTransform.sizeDelta = Vector2.zero;
        textObject.transform.SetParent(parent, false);
        return textObject;
    }

    public GameObject CreateText(Transform parent, string name, string contents, Rect rect, UnityAction<Text> setEvent)
    {
        GameObject textObject = new GameObject(name);
        textObject.AddComponent<CanvasRenderer>();
        Text text = textObject.AddComponent<Text>();
        text.text = contents;
        text.alignment = TextAnchor.MiddleCenter;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");// UnityEditor.AssetDatabase.GetBuiltinExtraResource<Font>("UI/Skin/Arial.psd");
        text.color = Color.black;
        RectTransform rectTransform = textObject.GetComponent<RectTransform>();
        //rectTransform.anchorMin = Vector2.zero;
        //rectTransform.anchorMax = Vector2.one;
        //rectTransform.anchoredPosition = Vector2.one * 0.5f;
        rectTransform.localPosition = rect.position;
        rectTransform.sizeDelta = rect.size;
        textObject.transform.SetParent(parent, false);
        setEvent(text);
        return textObject;
    }

    public GameObject CreateCanvas(Transform parent, string name, Rect rect)
    {
        GameObject canvasObject = new GameObject(name);
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();
        //Image image = canvasObject.AddComponent<Image>();
        //image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        //image.type = Image.Type.Sliced;
        //image.color = Color.white;
        RectTransform rectTransform = canvasObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(rect.x, rect.y);
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
        canvasObject.transform.SetParent(parent, false);
        return canvasObject;
    }

    public GameObject CreatePanel(Transform parent, string name, Rect rect)
    {
        GameObject panelObject = new GameObject(name);
        panelObject.AddComponent<CanvasRenderer>();
        Image image = panelObject.AddComponent<Image>();
        image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        image.type = Image.Type.Sliced;
        image.color = Color.white;
        RectTransform rectTransform = panelObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(rect.x, rect.y);
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
        panelObject.transform.SetParent(parent, false);
        return panelObject;
    }

    public GameObject CreateButton(Transform parent, string name, Rect rect, UnityAction clickedEvent)
    {
        GameObject buttonObject = new GameObject(name);
        buttonObject.transform.SetParent(parent, false);
        buttonObject.AddComponent<CanvasRenderer>();
        Image image = buttonObject.AddComponent<Image>();
        image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        image.type = Image.Type.Sliced;
        //image.color = Color.white;
        Button button = buttonObject.AddComponent<Button>();
        button.onClick.AddListener(clickedEvent);
        RectTransform rectTransform = buttonObject.GetComponent<RectTransform>();
        rectTransform.localPosition = rect.position;
        rectTransform.sizeDelta = rect.size;
        CreateText(buttonObject.transform, "Text", name);

        return buttonObject;
    }

    public GameObject CreateInputField(Transform parent, string name, string contents, Rect rect, UnityAction<InputField> setEvent)
    {
        GameObject inputFieldObject = new GameObject(name);
        inputFieldObject.AddComponent<CanvasRenderer>();
        Image image = inputFieldObject.AddComponent<Image>();
        image.sprite = UnityEditor.AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
        image.type = Image.Type.Sliced;
        RectTransform rectTransform = inputFieldObject.GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector2(rect.x, rect.y);
        rectTransform.sizeDelta = new Vector2(rect.width, rect.height);
        InputField inputField = inputFieldObject.AddComponent<InputField>();
        inputField.text = contents;
        GameObject placeHolderObject = CreateText(inputFieldObject.transform, "Placeholder", name);
        GameObject textObject = CreateText(inputFieldObject.transform, "Text", "");
        Text placeHolder = placeHolderObject.GetComponent<Text>();
        //placeHolder.alignment = TextAnchor.MiddleLeft;
        placeHolder.font = Resources.GetBuiltinResource<Font>("Arial.ttf");// UnityEditor.AssetDatabase.GetBuiltinExtraResource<Font>("UI/Skin/Arial.psd");
        placeHolder.color = Color.gray;
        Text text = textObject.GetComponent<Text>();
        //text.alignment = TextAnchor.MiddleLeft;
        text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");// UnityEditor.AssetDatabase.GetBuiltinExtraResource<Font>("UI/Skin/Arial.psd");
        text.color = Color.black;
        inputField.placeholder = placeHolder;
        inputField.textComponent = text;
        inputFieldObject.transform.SetParent(parent, false);
        setEvent(inputField);
  
        return inputFieldObject;
    }

    public Rect GetRectGrid(Rect panelRect, int rowCount, int columnCount, int rowIndex, int columnIndex)
    {
        Rect rowRect = GetRectVertical(panelRect, rowCount, rowIndex);
        Rect columnRect = GetRectHorizontal(rowRect, columnCount, columnIndex);
        Rect retRect = columnRect;
        retRect.y = rowRect.y;
        return retRect;
    }

    public Rect GetRectVertical(Rect panelRect, Rect unitRect, int index)
    {
        int max = (int)(panelRect.height / unitRect.height);
        float pos = (panelRect.height - unitRect.height) / 2.0f - unitRect.height * Mathf.Min(index, max);

        return new Rect(0.0f, pos, unitRect.width, unitRect.height);
    }

    public Rect GetRectVertical(Rect panelRect, int sliceCount, int index)
    {
        int unitHeight = (int)(panelRect.height / sliceCount);
        float pos = (panelRect.height - unitHeight) / 2.0f - unitHeight * Mathf.Min(index, unitHeight-1);

        return new Rect(0.0f, pos, panelRect.width, unitHeight);
    }

    public Rect GetRectVerticalSum(Rect upperRect, Rect lowerRect)
    {
        Vector2 pos = (upperRect.position + lowerRect.position) / 2.0f;
        Vector2 size = new Vector2(upperRect.width, upperRect.height + lowerRect.height);

        return new Rect(pos, size);
    }

    public Rect GetRectHorizontal(Rect panelRect, Rect unitRect, int index)
    {
        int max = (int)(panelRect.width / unitRect.width);
        float pos = (unitRect.width - panelRect.width) / 2.0f + unitRect.width * Mathf.Min(index, max);

        return new Rect(pos, 0.0f, unitRect.width, unitRect.height);
    }

    public Rect GetRectHorizontal(Rect panelRect, int sliceCount, int index)
    {
        int unitWidth = (int)(panelRect.width / sliceCount);
        float pos = (unitWidth - panelRect.width) / 2.0f + unitWidth * Mathf.Min(index, sliceCount-1);

        return new Rect(pos, 0.0f, unitWidth, panelRect.height);
    }

    public Rect GetRectHorizontalSum(Rect leftRect, Rect rightRect)
    {
        Vector2 pos = (leftRect.position + rightRect.position) / 2.0f;
        Vector2 size = new Vector2(leftRect.width + rightRect.width, leftRect.height);

        return new Rect(pos, size);
    }
}

public class DragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private Vector2 _pointerOffset;
    private RectTransform _canvasRectTransform;
    private RectTransform _panelRectTransform;

    void Awake()
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        if (canvas != null)
        {
            _canvasRectTransform = canvas.transform as RectTransform;
            _panelRectTransform = transform as RectTransform;
        }
    }

    public void OnPointerDown(PointerEventData data)
    {
        _panelRectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_panelRectTransform, data.position, data.pressEventCamera, out _pointerOffset);
    }

    public void OnDrag(PointerEventData data)
    {
        if (_panelRectTransform == null)
            return;

        Vector2 pointerPostion = ClampToWindow(data);

        Vector2 localPointerPosition;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            _canvasRectTransform, pointerPostion, data.pressEventCamera, out localPointerPosition
        ))
        {
            _panelRectTransform.localPosition = localPointerPosition - _pointerOffset;
        }
    }

    Vector2 ClampToWindow(PointerEventData data)
    {
        Vector2 rawPointerPosition = data.position;

        Vector3[] canvasCorners = new Vector3[4];
        _canvasRectTransform.GetWorldCorners(canvasCorners);

        float clampedX = Mathf.Clamp(rawPointerPosition.x, canvasCorners[0].x, canvasCorners[2].x);
        float clampedY = Mathf.Clamp(rawPointerPosition.y, canvasCorners[0].y, canvasCorners[2].y);

        Vector2 newPointerPosition = new Vector2(clampedX, clampedY);
        return newPointerPosition;
    }
}

public class ResizePanel : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Vector2 minSize;
    public Vector2 maxSize;

    private RectTransform rectTransform;
    private Vector2 currentPointerPosition;
    private Vector2 previousPointerPosition;

    void Awake()
    {
        rectTransform = transform.parent.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData data)
    {
        rectTransform.SetAsLastSibling();
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out previousPointerPosition);
    }

    public void OnDrag(PointerEventData data)
    {
        if (rectTransform == null)
            return;

        Vector2 sizeDelta = rectTransform.sizeDelta;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, data.position, data.pressEventCamera, out currentPointerPosition);
        Vector2 resizeValue = currentPointerPosition - previousPointerPosition;

        sizeDelta += new Vector2(resizeValue.x, -resizeValue.y);
        sizeDelta = new Vector2(
            Mathf.Clamp(sizeDelta.x, minSize.x, maxSize.x),
            Mathf.Clamp(sizeDelta.y, minSize.y, maxSize.y)
            );

        rectTransform.sizeDelta = sizeDelta;

        previousPointerPosition = currentPointerPosition;
    }
}