using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class InitDependencyInjection : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<AnimationCurvesData>().FromScriptableObject(_animationCurvesData).AsSingle();

        Container.Bind<GameMenuController>().FromNewComponentOnNewGameObject().AsSingle();
    }

    [SerializeField] private AnimationCurvesData _animationCurvesData;
}
