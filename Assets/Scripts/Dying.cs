using UnityEngine;

public class Dying : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _collider;

    private Animator _enemyAnim;

    private void OnEnable()
    {
        EnemyHealth.OnDying += DyingOfEnemy;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDying -= DyingOfEnemy;
    }

    private void Start()
    {
        _enemyAnim = GetComponent<Animator>();
    }

    private void DyingOfEnemy()
    {
        _enemyAnim.SetBool("isDie", true);
        _collider.enabled = false;       
    }
}
