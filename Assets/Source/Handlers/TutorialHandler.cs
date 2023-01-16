using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialHandler : MonoBehaviour, IInitializable
{
    public static bool isTutorialEnd;
    [SerializeField] bool switchTutorialsWithTouchInput;
    [SerializeField] List<LessonModel> tutorials;
    [SerializeField] GameObject lessonsBg;
    private int currentTutorialIndex;
    private LessonModel currentTutorial;
    private bool lessonActive;

    public void Initialize()
    {
        currentTutorialIndex = TutorialDataModel.Data.TutorialIndex;

        if (currentTutorialIndex < tutorials.Count)
        {
            isTutorialEnd = false;
            currentTutorial = tutorials[currentTutorialIndex];
            if (!currentTutorial.playByTrigger)
                PlayCurrentTutorial();
        }
        else isTutorialEnd = true;
    }

    public void SwitchToNextTutorial()
    {
        lessonActive = false;
        if (currentTutorial && !currentTutorial.selfManaged)
        {
            currentTutorial.StopLesson();
            lessonsBg.SetActive(false);
        }

        currentTutorialIndex = ++TutorialDataModel.Data.TutorialIndex;
        if (currentTutorialIndex >= tutorials.Count)
        {
            currentTutorial = null;
            isTutorialEnd = true;
        }
        else
        {
            currentTutorial = tutorials[currentTutorialIndex];
            if (!currentTutorial.playByTrigger)
                PlayCurrentTutorial();
        }
    }

    public void PlayCurrentTutorial()
    {
        if (currentTutorial && !lessonActive)
        {
            Invoke(nameof(PlayCurrentLesson), currentTutorial.playDelay);
        }
    }

    private void PlayCurrentLesson()
    {
        DOVirtual.DelayedCall(.3f, () => lessonActive = true);
        if (currentTutorial.selfManaged)
        {
            currentTutorial.OnLessonStop.AddListener(SwitchToNextTutorial);
            currentTutorial.PlayLesson();
        }
        else
        {
            currentTutorial.OnLessonPlay.AddListener(switchTutorialsWithTouchInput ? null : ActivateBG);
            currentTutorial.PlayLesson();
        }
    }

    private void ActivateBG() => DOVirtual.DelayedCall(.3f, () => lessonsBg.SetActive(true));

    private void Update()
    {
        if(currentTutorialIndex >= tutorials.Count) return;
        if (!switchTutorialsWithTouchInput || !lessonActive || currentTutorial.selfManaged) return;
        if (Input.GetMouseButtonDown(0))
        {
            SwitchToNextTutorial();
        }
    }
}