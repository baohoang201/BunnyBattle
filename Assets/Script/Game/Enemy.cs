using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform flower;
    [SerializeField] private GameObject carotPrefab;
    [SerializeField] private Sprite[] enemySprite;
     private GameObject icon;
    private SpriteRenderer enemyRenderer;
    private Animator animator;

    private float distance, distanceFire;
    private float randomNumber;
    private float fireRate, carotRate;
    private float nextFire, nextCarot;
    public static Enemy instance;
    void Awake()
    {
        instance = this;
        animator = GetComponent<Animator>();
        enemyRenderer = GetComponent<SpriteRenderer>();
        icon = GameObject.Find("icon");
    }

    void Start()
    {
        fireRate = 2f;
        nextFire = nextCarot = Time.time;

        carotRate = 4f;
        if (HomeScene.id == 1) enemyRenderer.flipX = true;
        enemyRenderer.sprite = enemySprite[HomeScene.id];
        icon.GetComponent<Image>().sprite = enemySprite[HomeScene.id];


    }
    void Update()
    {

        distance = Vector2.Distance(transform.position, PlayerController.instance.transform.position);
        distanceFire = Vector2.Distance(flower.position, PlayerController.instance.transform.position);
        if (distance > 1.5f)
        {
            RandomMove();
            if (HomeScene.id == 1)
            {
                if (Time.time > nextCarot)
                {
                    var direction = PlayerController.instance.transform.position - transform.position;
                    var carot = Instantiate(carotPrefab, transform.position, Quaternion.identity);
                    carot.GetComponent<Rigidbody2D>().AddForce(direction * 3f, ForceMode2D.Impulse);
                    var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
                    carot.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    nextCarot = Time.time + carotRate;
                }
            }
            animator.SetBool("status", false);

            if (randomNumber == 0) transform.RotateAround(PlayerController.instance.circle.position, Vector3.forward, -50f * Time.deltaTime);
            else transform.RotateAround(PlayerController.instance.circle.position, Vector3.forward, 50f * Time.deltaTime);
        }
        else animator.SetBool("status", true);


    }

    private void RandomMove()
    {
        if (Time.time > nextFire)
        {
            randomNumber = UnityEngine.Random.Range(0, 2);
            nextFire = Time.time + fireRate;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("damge"))
        {
            GameManager.instance.SubHeath();
            Camera.main.transform.DOShakePosition(0.05f, 1, 1).Play().OnComplete(() =>
            {
                Camera.main.transform.position = new Vector3(0, 0, -10);
            });
            if (GameManager.instance.enemyHeath <= 0)
            {
                ScoreManager.instance.score += 100;
                ScoreManager.instance.UpdateScore();
                GameManager.instance.countLevel++;
                Destroy(gameObject);
                GameManager.instance.InstiateObs();

                if (GameManager.instance.enemyHeath < 20) GameManager.instance.enemyHeath = 3 * GameManager.instance.countLevel;
                GameManager.instance.UpdateHeathEnemy();
                PlayerController.instance.ResetHeathPlayer();
            }


        }



    }
}
