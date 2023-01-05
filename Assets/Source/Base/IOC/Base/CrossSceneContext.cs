using UnityEngine;

public class CrossSceneContext : MonoSingleton<CrossSceneContext>,IContext,ICrossSceneObject
{
    [SerializeField] protected CrossSceneBuilder builder;
    protected Container container;
    public override void Initialize()
    {
        destroyGameObjectOnDuplicate = true;
        base.Initialize();
        if(destroyed) return;
        container = new();
        if (builder == null) CreateBuilder();
        builder.Build(container);
        container.Initialize();
    }

    public void CreateBuilder()
    {
        builder = FindObjectOfType<CrossSceneBuilder>();
        if(builder != null) return;
        Debug.LogError("There is no builder in scene. Creating temporary instance.");
        var gObj = new GameObject("TemporaryBuilder", typeof(Builder));
        builder = gObj.GetComponent<CrossSceneBuilder>();
    }

    public void HandleDontDestroy()
    {
        this.transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }
}