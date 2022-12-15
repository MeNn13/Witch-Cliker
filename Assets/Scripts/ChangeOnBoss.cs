using System;
using UnityEngine;

public class ChangeOnBoss : MonoBehaviour
{
    public static Action OnChangeBoss;

    [SerializeField] private Mesh _enemyMan;
    [SerializeField] private Mesh _enemyWoman;
    [SerializeField] private GameObject _bunttonBoss;

    SkinnedMeshRenderer skinnedMesh;
    private bool _isActive = false;
    private byte _died = 0;

    private void OnEnable()
    {
        EnemyHealth.OnDying += ShowBoss;
        EnemyHealth.OnDyingBoss += LevelMan;
    }

    private void OnDisable()
    {
        EnemyHealth.OnDying -= ShowBoss;
        EnemyHealth.OnDyingBoss -= LevelMan;
    }

    void Start()
    {
        skinnedMesh = GetComponent<SkinnedMeshRenderer>();
    }

    public void LevelWoman()
    {
        if (!_isActive)
        {
            skinnedMesh.sharedMesh = _enemyWoman;          
            OnChangeBoss?.Invoke();
            _isActive = true;
        }
    }

    private void LevelMan()
    {
        if (_isActive)
        {
            skinnedMesh.sharedMesh = _enemyMan;
            _bunttonBoss.SetActive(false);
            _isActive = false;
        }
    }

    private void ShowBoss()
    {
        if (_died == 5)
        {
            _bunttonBoss.SetActive(true);
            _died = 0;
        }
        else
            _died++;
    }
}
