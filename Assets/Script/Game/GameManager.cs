using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject ObstaclePrefabs;
    public int enemyHeath;

    [SerializeField] private Transform obsParent, diamondParent;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private GameOverPopup gameOverPopup;
    public static GameManager instance;
    public static float time;
    public int countLevel;
    [SerializeField] private Text txtHeath;


    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        InstiateObs();

        if (HomeScene.id == 0) enemyHeath = 3;
        else enemyHeath = 6;


        countLevel = 1;
        UpdateHeathEnemy();
    }

    public void InstiateObs()
    {
        var obsIns = Instantiate(ObstaclePrefabs);
        obsIns.transform.position = spawnPoint.transform.position;
        obsIns.transform.SetParent(obsParent);
    }


    public void GameOver()
    {
        UIManager.EnableGameOverPopUp(true);
        gameOverPopup.LoadText();
        UIManager.EnableMainGame(false);
        UIManager.EnableEnviroment(false);
    }

    public void SubHeath()
    {
        enemyHeath--;
        UpdateHeathEnemy();
    }

    public void UpdateHeathEnemy() => txtHeath.text = "x" + enemyHeath.ToString();
    public void OnclickPlay() =>  SceneManager.LoadScene(1);
       







}
