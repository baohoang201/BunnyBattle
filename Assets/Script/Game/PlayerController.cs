using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Image heathBar;
    [SerializeField] private Button jumpBtn;
    [SerializeField] private CanvasGroup hurt;
    public Transform circle, target;
    public bool isJump;
    [SerializeField] private BoxCollider2D damage;
    private int count;
    private Rigidbody2D rb;
    void Awake()
    {
        instance = this;
        isJump = false;
        rb = GetComponent<Rigidbody2D>();
        count = 0;
        heathBar.fillAmount = 1f;
    }
    void Update()
    {
        if (isJump) damage.enabled = true;
        else damage.enabled = false;

    }

    public void MoveLeft() => transform.RotateAround(circle.position, Vector3.forward, -100 * Time.deltaTime);
    public void MoveRight() => transform.RotateAround(circle.position, Vector3.forward, 100 * Time.deltaTime);
    public void Jump()
    {
        jumpBtn.interactable = false;
        DOVirtual.DelayedCall(0.8f, () =>
        {
            jumpBtn.interactable = true;
        });
        var direction = target.position - transform.position;
        rb.AddForce(direction * 5, ForceMode2D.Impulse);
        DOVirtual.DelayedCall(0.55f, () =>
        {
            if (count % 2 == 0) transform.localScale = new Vector3(1, -1, 1);
            else transform.localScale = new Vector3(1, 1, 1);
            count++;
        });
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("hole"))
        {
            rb.velocity = Vector2.zero;
            isJump = false;
        }

        if (other.gameObject.CompareTag("flower"))
        {
            Hurt();
            heathBar.fillAmount -= 0.1f;
            if (heathBar.fillAmount <= 0f) GameManager.instance.GameOver();

        }

        if (other.gameObject.CompareTag("carot"))
        {
            Hurt();
            heathBar.fillAmount -= 0.1f;
            if (heathBar.fillAmount <= 0f) GameManager.instance.GameOver();
            Destroy(other.gameObject);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        isJump = false;
    }


    void OnTriggerExit2D(Collider2D other)
    {
        isJump = true;
    }

    public void ResetHeathPlayer() => heathBar.fillAmount = 1;
    private void Hurt()
    {
        hurt.DOFade(1f, 0.2f).Play().OnComplete(() =>
        {
            hurt.DOFade(0f, 0.2f).Play();
        });
    }

}
