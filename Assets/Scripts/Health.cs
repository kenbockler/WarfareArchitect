using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    public int HealthPoints;

    private Animator animator;

    private WaypointFollower wpf;
    public bool IsDead = false;

    public int poison;
    private float poisonTick;

    public void Damage(int value)
    {
        HealthPoints -= value;
        if(HealthPoints <= 0)
        {
            if (!IsDead)
            {
                IsDead = true;
                StartCoroutine(WaitForDeathAnimation());
            }
        }
    }

    private void Start()
    {
        poisonTick = Time.time;
    }

    private void Update()
    {
        if(Time.time > poisonTick)
        {
            Damage(poison);
            poisonTick = Time.time + 1f; // Viimane siin on konstant: m�rk toimib iga sekund.
        }
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        wpf = GetComponent<WaypointFollower>();
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
