
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;
public class BuildingCreator : Singleton<BuildingCreator>
{
    [SerializeField] Tilemap previewMap, defaultMap;
    PlayerEditorInput playerInput;

    TileBase tileBase;
    BuildingObjectBase selectedObj;
    Camera _camera;

    Vector2 mousePos;
    Vector3Int currentGridPosition;
    Vector3Int lastGridPosition;

    bool holdActive;
    Vector3Int holdStartPosition;

    BoundsInt bounds;

    private void Awake()
    {
        base.Awake();
        playerInput = new PlayerEditorInput();
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        playerInput.Enable();

        playerInput.Gameplay.MousePosition.performed += OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started += OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled += OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed += OnRightClick;
    }

    private void OnDisable()
    {
        playerInput.Disable();

        playerInput.Gameplay.MousePosition.performed -= OnMouseMove;

        playerInput.Gameplay.MouseLeftClick.performed -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.started -= OnLeftClick;
        playerInput.Gameplay.MouseLeftClick.canceled -= OnLeftClick;

        playerInput.Gameplay.MouseRightClick.performed -= OnRightClick;
    }

    private BuildingObjectBase SelectedObj
    {
        set
        {
            selectedObj = value;
            tileBase = selectedObj != null ? selectedObj.TileBase : null;
            UpdatePreview();
        }
    }

    private void Update()
    {
        if (selectedObj != null)
        {
            Vector3 pos = _camera.ScreenToWorldPoint(mousePos);
            Vector3Int gridPos = previewMap.WorldToCell(pos);

            if (gridPos != currentGridPosition)
            {
                lastGridPosition = currentGridPosition;
                currentGridPosition = gridPos;
                UpdatePreview();
                if (holdActive)
                {
                    HandleDrawing();
                }
            }
        }
    }

    private void OnMouseMove(InputAction.CallbackContext context)
    {
        mousePos = context.ReadValue<Vector2>();
    }

    private void OnLeftClick(InputAction.CallbackContext context) 
    {
        if (selectedObj != null && !EventSystem.current.IsPointerOverGameObject())
        {
            if (context.phase == InputActionPhase.Started)
            {
                holdActive = true;
                if (context.interaction is TapInteraction)
                {
                    
                    holdStartPosition = currentGridPosition;
                }
                
                HandleDrawing();
            }
            else
            {
                if (context.interaction is SlowTapInteraction || context.interaction is TapInteraction && context.phase == InputActionPhase.Performed)
                {
                    holdActive = false;
                    HandleDrawRelease();
                }
            }
            HandleDrawing();
        }
    }
    private void OnRightClick(InputAction.CallbackContext context) 
    {
        SelectedObj = null;
    }

    public void ObjectSelected(BuildingObjectBase obj)
    {
        SelectedObj = obj;
    }

    private void UpdatePreview()
    {
        previewMap.SetTile(lastGridPosition, null);
        previewMap.SetTile(currentGridPosition, tileBase);
    }

    private void HandleDrawing()
    {
        if (selectedObj != null)
        {
            switch(selectedObj.PlaceType)
            {
                case PlaceType.Single:
                default: 
                    DrawItem(); 
                    break;
                case PlaceType.Line:
                    LineRenderer();
                    break;
                case PlaceType.Rectangle:
                    RectangleRenderer();
                    break;
            }
        }
        DrawItem();
    }

    private void HandleDrawRelease()
    {
        if (selectedObj != null)
        {
            switch (selectedObj.PlaceType)
            {
                case PlaceType.Line:
                case PlaceType.Rectangle:
                    DrawBounds(defaultMap);
                    previewMap.ClearAllTiles();
                    break;
            }
        }
    }

    private void RectangleRenderer()
    {
        previewMap.ClearAllTiles();

        bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
        bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;

        DrawBounds(previewMap);
    }

    private void LineRenderer()
    {
        previewMap.ClearAllTiles();
        float diffX = Mathf.Abs(currentGridPosition.x - holdStartPosition.x);
        float diffY = Mathf.Abs(currentGridPosition.y - holdStartPosition.y);

        bool lineIsHorizontal = diffX >= diffY;

        if (lineIsHorizontal)
        {
            bounds.xMin = currentGridPosition.x < holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.xMax = currentGridPosition.x > holdStartPosition.x ? currentGridPosition.x : holdStartPosition.x;
            bounds.yMin = holdStartPosition.y;
            bounds.yMax = holdStartPosition.y;
        }
        else
        {
            bounds.xMin = holdStartPosition.x;
            bounds.xMax = holdStartPosition.x;
            bounds.yMin = currentGridPosition.y < holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
            bounds.yMax = currentGridPosition.y > holdStartPosition.y ? currentGridPosition.y : holdStartPosition.y;
        }

        DrawBounds(previewMap);
    }

    private void DrawBounds (Tilemap map)
    {
        for (int x = bounds.xMin; x <= bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y <= bounds.yMax; y++)
            {
                map.SetTile(new Vector3Int(x, y, 0), tileBase);
            }
        }
    }


    private void DrawItem()
    {
        defaultMap.SetTile(currentGridPosition, tileBase);
    }
}
