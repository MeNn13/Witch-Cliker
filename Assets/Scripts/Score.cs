using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Action<ProductData> OnPurchasedProduct;

    [SerializeField] private GameObject _notEnoughCountUI;
    private TextMeshProUGUI _score;
    [SerializeField] private ulong count = 0;

    [Header(" лик")]
    #region Click Variables
    [SerializeField] private TextMeshProUGUI _clickPrice;
    private ulong _summationClick = 1;
    private ulong _minSummationClick = 40;
    #endregion

    [Header("јвто-клик")]
    #region AutoClick Variable
    [SerializeField] private TextMeshProUGUI _autoClickPrice;
    private ulong _summationAutoClick = 1;
    private ulong _minSummationAutoClick = 200;
    #endregion

    private void Start()
    {
        count = Progress.Instance.GameInfo.Score;
        _summationClick = Progress.Instance.GameInfo.SummClick;
        _summationAutoClick = Progress.Instance.GameInfo.SummAutoClick;
        _minSummationClick = Progress.Instance.GameInfo.PriceClick;
        _minSummationAutoClick = Progress.Instance.GameInfo.PriceAutoClick;

        _score = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        Click.OnClickEnter += ScoreClickUp;
        AutoClick.OnAutoClick += ScoreAutoClick;
        Buy.OnBuyProduct += BuyProduct;
        Upgrade.OnUpgrade += UpgradeClicks;
    }

    private void OnDisable()
    {
        Click.OnClickEnter -= ScoreClickUp;
        AutoClick.OnAutoClick -= ScoreAutoClick;
        Buy.OnBuyProduct -= BuyProduct;
        Upgrade.OnUpgrade -= UpgradeClicks;
    }

    private void ScoreClickUp()
    {
        count += _summationClick;
        _score.text = "—чет:" + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void ScoreAutoClick()
    {
        count += _summationAutoClick;
        _score.text = "—чет:" + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void BuyProduct(ProductData product)
    {
        if (count >= product.price)
        {
            count -= product.price;
            _score.text = "—чет:" + count.ToString();

            OnPurchasedProduct?.Invoke(product);
        }
        else
            _notEnoughCountUI.SetActive(true);

    }

    private void UpgradeClicks(UpgradeItem item)
    {
        if (item == UpgradeItem.Click)
            UpgradeClick();
        else
            UpgradeAutoClick();
    }

    private void UpgradeClick()
    {
        ulong summWithProcent = _minSummationClick * 2;
        if (count >= summWithProcent)
        {
            count -= summWithProcent;
            _score.text = "—чет:" + count.ToString();
            _minSummationClick = summWithProcent;
            _clickPrice.text = "$ " + (summWithProcent * 2).ToString();
            _summationClick++;

            Progress.Instance.GameInfo.SummClick = _summationClick;
            Progress.Instance.GameInfo.PriceClick = summWithProcent;
            Progress.Instance.GameInfo.Score = count;
        }
        else
            _notEnoughCountUI.SetActive(true);
    }

    private void UpgradeAutoClick()
    {
        ulong summWithProcent = _minSummationAutoClick * 2;
        if (count >= summWithProcent)
        {
            count -= summWithProcent;
            _score.text = "—чет:" + count.ToString();
            _minSummationAutoClick = summWithProcent;
            _autoClickPrice.text = "$ " + (summWithProcent * 2).ToString();
            _summationAutoClick++;

            Progress.Instance.GameInfo.SummAutoClick = _summationAutoClick;
            Progress.Instance.GameInfo.PriceAutoClick = summWithProcent;
            Progress.Instance.GameInfo.Score = count;
        }
        else
            _notEnoughCountUI.SetActive(true);
    }
}
