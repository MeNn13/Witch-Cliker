using System;
using UnityEngine;

public class Buy : MonoBehaviour
{
    public static Action<ProductData> OnBuyProduct;

    public void OnBuy(ProductData productData) 
    {
        OnBuyProduct?.Invoke(productData);
    }
}

