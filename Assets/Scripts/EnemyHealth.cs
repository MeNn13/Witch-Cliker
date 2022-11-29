using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject _die;

    [SerializeField]
    private int _health = 200;

    private void OnEnable()
    {
        Click.OnClickEnter += CheckHealth;
        AutoClick.OnAutoClick += CheckHealth;
    }

    private void OnDisable()
    {
        Click.OnClickEnter -= CheckHealth;
        AutoClick.OnAutoClick -= CheckHealth;
    }

    private void CheckHealth()
    {
        if (_health == 0)
        {
            //Умер            
            _die.SetActive(true);
            _health = 200;
            Progress.Instance.Save();
        }
        else
        {
            _health--;
        }
    }
}
