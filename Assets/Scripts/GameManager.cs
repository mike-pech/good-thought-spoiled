using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField] private Transform mainCamera;
    [SerializeField] private Transform finish;
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Material material1;
    [SerializeField] private Material material2;
    public static GameManager instance;

    private bool GameHalted;
    public List<Ball> players = new List<Ball>();
    public int currentPlayerIndex = 0;

    private void Awake() {
        GameHalted = false;
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject); // Сохраняем объект между сценами
            InitializePlayers();
        } else {
            Destroy(gameObject); // Удаляем дубликаты
        }
    }

    private void InitializePlayers() {
        players.Add(CreatePlayer("Игрок 1", new Vector3(-22, 34, -8), material1));
        players.Add(CreatePlayer("Игрок 2", new Vector3(-22, 34, -4), material2));
        NextTurn();
        // Добавьте больше игроков по мере необходимости
    }

    private Ball CreatePlayer(string playerName, Vector3 position, Material material) {
        GameObject ballObject = Instantiate(ballPrefab, position, Quaternion.identity);
        ballObject.GetComponent<MeshRenderer>().material = material;
        Ball ball = ballObject.GetComponent<Ball>();
        ball.Name = playerName; // Assuming Ball has a Name property
        return ball;
    }
    public void NextTurn() {
        if (GameHalted) {
            return;
        }
        currentPlayerIndex++;

        if (currentPlayerIndex >= players.Count) {
            currentPlayerIndex = 0; // Вернуться к первому игроку
        }

        Debug.Log("Текущий игрок: " + players[currentPlayerIndex].Name);

        var mainCameraObject = mainCamera.GetComponent<ICamera>();
        if (mainCameraObject != null) {
            mainCameraObject.ChangeAngle(players[currentPlayerIndex].transform, finish);
        }
    }
    public void HaltResume() {
        GameHalted = !GameHalted;
    }

    public bool IsCurrentPlayer(Ball ball) {
        // Здесь вы можете реализовать логику проверки текущего игрока.
        // Например, если у вас есть несколько мячей для каждого игрока.
        return true; // Замените на вашу логику.
    }
}
