using UnityEngine;

public class LevelManager : Singleton<LevelManager> {
    void Start() {
        PredictionManager.instance.CopyAllObstacles();
    }
}
