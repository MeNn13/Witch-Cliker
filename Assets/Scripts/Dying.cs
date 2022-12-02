using UnityEngine;

public class Dying : MonoBehaviour
{
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private GameObject _enemyResurrection;
    [SerializeField] private AudioSource _audioResurrection;

    private void OnEnable()
    {
        EnemyHealth.OnDying += DyingOfEnemy;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDying -= DyingOfEnemy;
    }

    private void DyingOfEnemy()
    {
        Instantiate(_enemyResurrection);
        _audioResurrection.Play();
    }
}
