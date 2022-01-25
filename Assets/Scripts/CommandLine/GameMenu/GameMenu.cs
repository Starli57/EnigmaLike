using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private MenuFieldsData _menuData;

    [Space]
    [SerializeField] private TextMeshProUGUI _menuText;

    [Space]
    [SerializeField] private string _defaultColor;
    [SerializeField] private string _devMenuColor;
    [SerializeField] private string _selectedColor;

    private GameMenuController _menuController { get; set; }

    private int _selectedMenuIndex;

    private MenuType MenuType { get { return _menuController.CurrentMenu.Val; } }

    [Inject]
    private void Construct(GameMenuController menuController)
    {
        _menuController = menuController;
    }

    private void OnEnable()
    {
        _menuController.CurrentMenu.OnChanged += OnCurrentMenuChanged;
        RenderMenu(_menuData.GetMenu(MenuType), _selectedMenuIndex);
    }

    private void OnDisable()
    {
        _menuController.CurrentMenu.OnChanged -= OnCurrentMenuChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            var menu = _menuData.GetMenu(MenuType);
            _selectedMenuIndex = Mathf.Clamp(_selectedMenuIndex - 1, 0, menu.Fields.Count - 1);
            RenderMenu(_menuData.GetMenu(MenuType), _selectedMenuIndex);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            var menu = _menuData.GetMenu(MenuType);
            _selectedMenuIndex = Mathf.Clamp(_selectedMenuIndex + 1, 0, menu.Fields.Count - 1);
            RenderMenu(_menuData.GetMenu(MenuType), _selectedMenuIndex);
        }
        else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            var menu = _menuData.GetMenu(MenuType);
            var newField = menu.Fields[_selectedMenuIndex].Next;

            menu = _menuData.GetMenu(newField);
            if (menu.Fields.Count > 0)
            {
                _menuController.ChangeMenu(menu.MenuType);
            }
        }
    }

    private void RenderMenu(Menu menu, int selected)
    {
        List<string> formattedFields = new List<string>();

        for(int i = 0; i < menu.Fields.Count; i++)
        {
            string fieldColor = i == selected ? _selectedColor : menu.Fields[i].IsDev ? _devMenuColor : _defaultColor;
            string prefix = i == selected ? "> " : "";
            formattedFields.Add("<color=#" + fieldColor + ">" + prefix + menu.Fields[i].FieldName + "</color>");
        }

        string formattedMenu = "";
        foreach (var field in formattedFields)
            formattedMenu += field + "\n";

        _menuText.text = formattedMenu;
    }

    private void OnCurrentMenuChanged(MenuType menuType)
    {
        _selectedMenuIndex = 0;
        RenderMenu(_menuData.GetMenu(MenuType), _selectedMenuIndex);
    }
}
