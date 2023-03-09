using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : ScreenModel
{
    [Dependency] private GameController _gameController;
    public override void Show()
    {
        this.Inject();
        screenElements[0].SetActiveGameObject(_gameController.IsPlayerWin);
        screenElements[1].SetActiveGameObject(!_gameController.IsPlayerWin);
        base.Show();
    }
}
