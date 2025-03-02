﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PredictionManager : Singleton<PredictionManager> {
    [SerializeField] private GameObject obstacles;
    [SerializeField] private int maxIterations;

    Scene currentScene;
    Scene predictionScene;

    PhysicsScene currentPhysicsScene;
    PhysicsScene predictionPhysicsScene;

    List<GameObject> dummyObstacles = new List<GameObject>();

    LineRenderer lineRenderer;
    GameObject dummy;

    void Start() {
        Physics.simulationMode = SimulationMode.Script;

        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene();

        lineRenderer = GetComponent<LineRenderer>();
    }

    void FixedUpdate() {
        if (currentPhysicsScene.IsValid()) {
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void CopyAllObstacles() {
        foreach (Transform t in obstacles.transform) {
            if (t.gameObject.GetComponent<Collider>() != null) {
                GameObject fakeT = Instantiate(t.gameObject);
                fakeT.transform.position = t.position;
                fakeT.transform.rotation = t.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if (fakeR) {
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                dummyObstacles.Add(fakeT);
            }
        }
    }

    void KillAllObstacles() {
        foreach (var o in dummyObstacles) {
            Destroy(o);
        }
        dummyObstacles.Clear();
    }

    public void Predict(GameObject subject, Vector3 currentPosition, Vector3 force) {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid()) {
            if (dummy == null) {
                dummy = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }
            Debug.DrawLine(currentPosition, dummy.transform.position, Color.cyan);

            dummy.transform.position = currentPosition;
            dummy.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);
            // lineRenderer.positionCount = 0;
            lineRenderer.positionCount = maxIterations;


            for (int i = 0; i < maxIterations; i++) {
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                lineRenderer.SetPosition(i, dummy.transform.position);
            }

            Destroy(dummy);
        }
    }

    void OnDestroy() {
        KillAllObstacles();
    }
}
