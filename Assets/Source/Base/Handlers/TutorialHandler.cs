using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TutorialHandler : MonoSingleton<TutorialHandler>, IInitializable, ICrossSceneObject
{
    private List<LessonModel> lessons;

    public void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Init();
        if(destroyed) return;
        HandleDontDestroy();
        //TODO: Addressables support
        lessons = Resources.LoadAll<LessonModel>("LessonPrefabs").ToList();
    }

    public T GetLesson<T>() where T : LessonModel
    {
        T lesson = lessons.FirstOrDefault(lesson => lesson.GetType() == typeof(T)) as T;
        if (lesson is null)
        {
            Debug.LogWarning($"Tutorial type {nameof(T)} is not exists in Resources/LessonPrefabs!");
            return default;
        }
        return lesson;
    }
    
    public void HandleDontDestroy()
    {
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}