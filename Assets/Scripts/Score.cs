using System;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Action<ProductData> OnPurchasedProduct;

    [SerializeField] private GameObject _notEnoughCountUI;
    private TextMeshProUGUI _score;
    [SerializeField] private ulong count = 0;
    [SerializeField] private TextMeshProUGUI _forcePunch;

    [Header("����")]
    #region Click Variables
    [SerializeField] private TextMeshProUGUI _clickPrice;
    private ulong _summationClick = 1;
    private ulong _minSummationClick = 40;
    #endregion

    [Header("����-����")]
    #region AutoClick Variable
    [SerializeField] private TextMeshProUGUI _autoClickPrice;
    private ulong _summationAutoClick = 1;
    private ulong _minSummationAutoClick = 200;
    #endregion

    private byte _multiplier = 1;

    private void Start()
    {
        #region Initialization Variables
        count = Progress.Instance.GameInfo.Score;
        _summationClick = Progress.Instance.GameInfo.SummClick;
        _summationAutoClick = Progress.Instance.GameInfo.SummAutoClick;
        _minSummationClick = Progress.Instance.GameInfo.PriceClick;
        _minSummationAutoClick = Progress.Instance.GameInfo.PriceAutoClick;
        #endregion

        _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";

        _clickPrice.text = "$ " + (_minSummationClick * 2).ToString();
        _autoClickPrice.text = "$ " + (_minSummationAutoClick * 2).ToString();

        _score = GetComponent<TextMeshProUGUI>();
    }

    public void Zero()
    {
        Progress.Instance.GameInfo.Score = 0;
        Progress.Instance.GameInfo.SummClick = 1;
        Progress.Instance.GameInfo.PriceClick = 40;
        Progress.Instance.GameInfo.SummAutoClick = 1;
        Progress.Instance.GameInfo.PriceAutoClick = 200;

        Progress.Instance.GameInfo.ProductName.Clear();

        Progress.Instance.Save();
    }

    private void OnEnable()
    {
        Click.OnClickEnter += ScoreClickUp;
        AutoClick.OnAutoClick += ScoreAutoClick;
        Buy.OnBuyProduct += BuyProduct;
        Upgrade.OnUpgrade += UpgradeClicks;

        #region ADS Events
        ADS.OnAdsMultiplier += MultiplierClicks;
        ADS.UnAdsMultiplier += UnMultiplierClicks;
        #endregion
    }

    private void OnDisable()
    {
        Click.OnClickEnter -= ScoreClickUp;
        AutoClick.OnAutoClick -= ScoreAutoClick;
        Buy.OnBuyProduct -= BuyProduct;
        Upgrade.OnUpgrade -= UpgradeClicks;

        #region ADS Events
        ADS.OnAdsMultiplier -= MultiplierClicks;
        ADS.UnAdsMultiplier -= UnMultiplierClicks;
        #endregion
    }

    private void ScoreClickUp()
    {
        count += _summationClick * _multiplier;
        _score.text = "����:" + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void ScoreAutoClick()
    {
        count += _summationAutoClick * _multiplier;
        _score.text = "����:" + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void BuyProduct(ProductData product)
    {
        if (count >= product.price)
        {
            count -= product.price;
            _score.text = "����:" + count.ToString();

            OnPurchasedProduct?.Invoke(product);

            Progress.Instance.GameInfo.ProductName.Add(product.productName);
            Progress.Instance.GameInfo.Score = count;
            Progress.Instance.Save();
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
            _score.text = "����:" + count.ToString();
            _minSummationClick = summWithProcent;
            _clickPrice.text = "$ " + (summWithProcent * 2).ToString();
            _summationClick++;

            Progress.Instance.GameInfo.SummClick = _summationClick;
            Progress.Instance.GameInfo.PriceClick = summWithProcent;
            Progress.Instance.GameInfo.Score = count;

            _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";
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
            _score.text = "����:" + count.ToString();
            _minSummationAutoClick = summWithProcent;
            _autoClickPrice.text = "$ " + (summWithProcent * 2).ToString();
            _summationAutoClick++;

            Progress.Instance.GameInfo.SummAutoClick = _summationAutoClick;
            Progress.Instance.GameInfo.PriceAutoClick = summWithProcent;
            Progress.Instance.GameInfo.Score = count;

            _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";
        }
        else
            _notEnoughCountUI.SetActive(true);
    }

    private void MultiplierClicks()
    {
        _forcePunch.color = Color.red;
        _multiplier = 2;
    }

    private void UnMultiplierClicks()
    {
        _forcePunch.color = _score.color;
        _multiplier = 1;
    }
}
