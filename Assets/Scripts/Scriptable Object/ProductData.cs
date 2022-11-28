using UnityEngine;

[CreateAssetMenu(fileName = "ProductData", menuName = "My Game/Product Data")]
public class ProductData : ScriptableObject
{
    public string productName;
    public string description;
    public ulong price;
}
