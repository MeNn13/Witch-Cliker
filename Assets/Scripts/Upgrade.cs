using System;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public static Action<UpgradeItem> OnUpgrade;

    [SerializeField] private UpgradeItem item = 0;

    public void OnClickUp()
    {
        OnUpgrade?.Invoke(item);
    }
}

public enum UpgradeItem
{
    Click = 1,
    AutoClick = 2,
}
