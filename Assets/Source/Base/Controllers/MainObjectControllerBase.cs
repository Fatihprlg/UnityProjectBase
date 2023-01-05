using UnityEngine;
    public abstract class MainObjectControllerBase : ControllerBase
    {
        public abstract void ClearActiveObjects();
        public abstract void ResetAll();
        public abstract void SetActiveObject(int poolIndex, bool state);
    }
