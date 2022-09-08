using UnityEngine;

public class Plane : MonoBehaviour
{
    private Vector2Int _planePosition = new Vector2Int();
    private static Vector2Int _currentMousePosition = new Vector2Int();
    private static Vector2Int _initMousePosition = new Vector2Int(-1,-1);

    private Renderer _renderer;
    private bool _isObjectHovered = false;
    private static bool _isSelectionActive = false; 
    
    private Color _defaultColor = Color.white;
    private Color _hoverColor = Color.green;
    private Color _selectionColor = Color.red;
    private Color _currentColor;

    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _currentColor = _defaultColor;

        _planePosition.x = Mathf.RoundToInt(Mathf.Abs(transform.localPosition.x / 10));
        _planePosition.y = Mathf.RoundToInt(transform.localPosition.z / 10);
    }

    private void Update()
    {
        if (_isObjectHovered && Input.GetMouseButtonDown(0))
        {
            _isSelectionActive = !_isSelectionActive;
            
            if (_isSelectionActive && _initMousePosition == new Vector2Int(-1, -1))
            {
                _initMousePosition = _currentMousePosition;
            }
            else
            {
                _initMousePosition = new Vector2Int(-1, -1);
            }
        }

        if (_isSelectionActive)
        {
            //this is responsible for right & bottom selection
            if (_currentMousePosition.x >= _planePosition.x &&
                _planePosition.x >= _initMousePosition.x &&
                _currentMousePosition.y >= _planePosition.y &&
                _planePosition.y >= _initMousePosition.y)
            {
                _currentColor = _selectionColor;
                _renderer.material.color = _selectionColor;
            }//this is responsible for left & top selection
            else if (_currentMousePosition.x <= _planePosition.x &&
                    _planePosition.x <= _initMousePosition.x &&
                    _currentMousePosition.y <= _planePosition.y &&
                    _planePosition.y <= _initMousePosition.y)
            {
                _currentColor = _selectionColor;
                _renderer.material.color = _selectionColor;
            }//this is responsible for left & bottom selection
            else if (_currentMousePosition.x <= _planePosition.x &&
                    _planePosition.x <= _initMousePosition.x &&
                    _currentMousePosition.y >= _planePosition.y &&
                    _planePosition.y >= _initMousePosition.y)
            {
                _currentColor = _selectionColor;
                _renderer.material.color = _selectionColor;
            }//this is responsible for right & top selection
            else if (_currentMousePosition.x >= _planePosition.x &&
                    _planePosition.x >= _initMousePosition.x &&
                    _currentMousePosition.y <= _planePosition.y &&
                    _planePosition.y <= _initMousePosition.y)
            {
                _currentColor = _selectionColor;
                _renderer.material.color = _selectionColor;
            }
            else
            {
                _currentColor = _defaultColor;
                _renderer.material.color = _defaultColor;
            }
        }
    }

    private void OnMouseEnter()
    {
        _isObjectHovered = true;
        _currentMousePosition = _planePosition;

        if (_isSelectionActive)
            return;

        //we change the color when we hover the object
        _renderer.material.color = _hoverColor;
    }
    
    private void OnMouseExit()
    {
        _isObjectHovered = false;

        if (_isSelectionActive)
            return;

        //when we don't hover the object anymore, we switch back to the original saved color
        _renderer.material.color = _currentColor;
    }
}
