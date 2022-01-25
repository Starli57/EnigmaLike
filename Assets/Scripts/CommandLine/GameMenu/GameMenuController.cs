using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    public ReactValue<MenuType> CurrentMenu { get; private set; }

    public void ChangeMenu(MenuType menu)
    {
        CurrentMenu.Val = menu;
    }

    private void Awake()
    {
        CurrentMenu = new ReactValue<MenuType>();
        CurrentMenu.Val = MenuType.Main;
    }
}
