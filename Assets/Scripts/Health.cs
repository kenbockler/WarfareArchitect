using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int HealthPoints;

    private Animator animator;

    private WaypointFollower wpf;
    public bool IsDead = false;

    public int poison;
    private float poisonTick;

    private Transform HealthbarCanvas;
    private Image HealthBarBackground;
    private Image HealthBarForeground;
    private int MaxHealth;

    public void Damage(int value)
    {
        HealthPoints -= value;

        HealthBarBackground.color = new Color(HealthBarBackground.color.r, Mathf.Clamp((float)HealthPoints / MaxHealth, 0.2f, 1), HealthBarBackground.color.b);
        HealthBarForeground.fillAmount = 1 - (((float) HealthPoints) / MaxHealth);

        if(HealthPoints <= 0)
        {
            if (!IsDead)
            {
                IsDead = true;
                StartCoroutine(WaitForDeathAnimation());
            }
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        wpf = GetComponent<WaypointFollower>();

        HealthbarCanvas = transform.GetChild(0);
        HealthBarBackground = HealthbarCanvas.GetChild(0).GetComponent<Image>();
        HealthBarForeground = HealthbarCanvas.GetChild(0).GetChild(0).GetComponent<Image>();
        MaxHealth = HealthPoints;
    }

    private void Start()
    {
        poisonTick = Time.time;
    }

    private void Update()
    {
        HealthbarCanvas.LookAt(Camera.main.transform);
        if(Time.time > poisonTick)
        {
            Damage(poison);
            poisonTick = Time.time + 1f; // Viimane siin on konstant: m�rk toimib iga sekund.
        }
    }

    IEnumerator WaitForDeathAnimation()
    {
        // Play the death animation
        animator.SetTrigger("Death");

        // Wait for the length of the death animation
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        // Destroy the GameObject after the animation has finished
        Destroy(gameObject);


    }
}
