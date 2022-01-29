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

        Container.Bind<Printer>().FromInstance(_printer);
        Container.Bind<RotorsBox>().FromInstance(_rotorsBox);
        Container.Bind<PapersList>().FromInstance(_papersList);

        Container.Bind<AudioManager>().FromInstance(_audioManager);
    }

    [SerializeField] private AnimationCurvesData _animationCurvesData;

    [Space]
    [SerializeField] private Printer _printer;
    [SerializeField] private RotorsBox _rotorsBox;
    [SerializeField] private PapersList _papersList;

    [Space]
    [SerializeField] private AudioManager _audioManager;
}
