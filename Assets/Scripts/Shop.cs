using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] _products;
    [SerializeField] private Button[] _productsUI;

    private void OnEnable()
    {
        Score.OnPurchasedProduct += Purchased;
    }

    private void OnDisable()
    {
        Score.OnPurchasedProduct -= Purchased;
    }

    private void Purchased(ProductData product)
    {
        switch (product.productName)
        {
            case "�����":
                _products[0].SetActive(true);
                _productsUI[0].interactable = false;
                break;

            case "�������":
                _products[1].SetActive(true);
                _productsUI[1].interactable = false;
                break;

            case "�����":
                _products[2].SetActive(true);
                _productsUI[2].interactable = false;
                break;

            case "����� ����������":
                _products[3].SetActive(true);
                _productsUI[3].interactable = false;
                break;

            case "�����":
                _products[4].SetActive(true);
                _productsUI[4].interactable = false;
                break;

        }
    }
}
