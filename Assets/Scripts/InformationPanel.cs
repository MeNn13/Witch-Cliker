using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class InformationPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("������ ��������")]
    [SerializeField] private ProductData _product;

    [Header("���������� �������������� ������")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private TextMeshProUGUI _infoText;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _infoText.text = _product.description;
        _infoPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _infoPanel.SetActive(false);
    }
}
