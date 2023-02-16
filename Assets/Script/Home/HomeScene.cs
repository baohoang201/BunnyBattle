using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class HomeScene : MonoBehaviour
{
    [SerializeField] private Button btnPlay;
    public static int id;
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnclickPlay()
    {
        SceneManager.LoadScene(1);
        id = 0;
    }

       public void OnclickPlayHard()
    {
        SceneManager.LoadScene(1);
        id = 1;
    }
    private void ButtonScale() => btnPlay.transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.5f).SetLoops(-1, LoopType.Yoyo).Play();


}
