using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CommandLine : MonoBehaviour
{
    [SerializeField] private List<CommandLineWindow> _allWindows;
    [SerializeField] private List<MenuWindows> _windowsByMenu;

    private GameMenuController _menuController { get; set; }

    [Inject]
    private void Construct(GameMenuController menuController)
    {
        _menuController = menuController;
    }

    private void OnEnable()
    {
        _menuController.CurrentMenu.OnChanged += OnMenuChanged;
    }

    private void OnDisable()
    {
        _menuController.CurrentMenu.OnChanged -= OnMenuChanged;
    }

    private void OnMenuChanged(MenuType menuType)
    {
        var relevantWindows = _windowsByMenu.Find(x => x.MenuType == menuType);
        foreach(var window in _allWindows)
        {
            window.WindowData.GameObject.SetActive(relevantWindows.Windows.Find(x => x.WindowData.Caption == window.WindowData.Caption));//todo: compare by id not caption
        }
    }
}
