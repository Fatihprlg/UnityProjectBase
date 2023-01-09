using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndViewModel : ScreenElement
{
    [SerializeField] private Button replayBtn;
    [SerializeField] private Button nextLvBtn;
    [Dependency] private LevelController lvController;
    public override void Initialize()
    {
        base.Initialize();
        this.Inject();
        replayBtn.onClick.RemoveAllListeners();
        nextLvBtn.onClick.RemoveAllListeners();
        replayBtn.onClick.AddListener(lvController.ReplayLevel);
        nextLvBtn.onClick.AddListener(lvController.NextLevel);
    }
    
}
