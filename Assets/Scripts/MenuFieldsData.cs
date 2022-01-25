using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MenuFieldsData", menuName = "Data/MenuFieldsData")]
public class MenuFieldsData : ScriptableObject
{
    public Menu GetMenu(MenuType menuType)
    {
        return _menuTypes.Find(x => x.MenuType == menuType);
    }

    [SerializeField] private List<Menu> _menuTypes;
}

[System.Serializable]
public class Menu
{
    public MenuType MenuType;
    public List<MenuField> Fields;
}

[System.Serializable]
public class MenuField
{
    public MenuType Next;
    public string FieldName;
    public bool IsDev;
}

public enum MenuType { Main, Encrypt, Decrypt, Instructions, Save, Back, Exit}
