using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public static Action<ProductData> OnPurchasedProduct;

    private TextMeshProUGUI _score;
    [SerializeField] private ulong count = 0;

    [Header("UI")]
    [SerializeField] private Button[] _buttons;
    [SerializeField] private GameObject _levelPanel;
    [SerializeField] private TextMeshProUGUI _forcePunch;
    [SerializeField] private GameObject _notEnoughCountUI;

    [Header("Клик")]
    #region Click Variables
    [SerializeField] private TextMeshProUGUI _clickPrice;
    private byte _summationClick = 1;
    private int _minSummationClick = 40;
    #endregion

    [Header("Авто-клик")]
    #region AutoClick Variable
    [SerializeField] private TextMeshProUGUI _autoClickPrice;
    private byte _summationAutoClick = 1;
    private int _minSummationAutoClick = 200;
    #endregion

    private byte _multiplier = 1;
    private string _countText;

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
        _countText = _score.text;

        CheckLevel();
    }

    private void CheckLevel()
    {
        if (_summationClick == 10 && _summationAutoClick == 10 && Progress.Instance.GameInfo.Level != 10)
            _levelPanel.SetActive(true);
    }

    public void Zero()
    {
        Progress.Instance.GameInfo.Score = 0;
        Progress.Instance.GameInfo.SummClick = 0;
        Progress.Instance.GameInfo.PriceClick = 0;
        Progress.Instance.GameInfo.SummAutoClick = 0;
        Progress.Instance.GameInfo.PriceAutoClick = 0;
        Progress.Instance.GameInfo.Level = 0;

        Progress.Instance.GameInfo.ProductName.Clear();

        Progress.Instance.Save();
    }

    private void OnEnable()
    {
        Click.OnClickEnter += ScoreClickUp;
        AutoClick.OnAutoClick += ScoreAutoClick;
        Buy.OnBuyProduct += BuyProduct;
        Upgrade.OnUpgrade += UpgradeClicks;
        Level.OnLevelUp += SummClicksUpdate;
        EnemyHealth.OnDying += ScorePlus;
        EnemyHealth.OnDyingBoss += KillBoss;

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
        Level.OnLevelUp -= SummClicksUpdate;
        EnemyHealth.OnDying -= ScorePlus;
        EnemyHealth.OnDyingBoss -= KillBoss;

        #region ADS Events
        ADS.OnAdsMultiplier -= MultiplierClicks;
        ADS.UnAdsMultiplier -= UnMultiplierClicks;
        #endregion
    }

    private void ScoreClickUp()
    {
        count += (ulong)_summationClick * _multiplier;
        _score.text = _countText + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void ScoreAutoClick()
    {
        count += (ulong)_summationAutoClick * _multiplier;
        _score.text = _countText + count.ToString();
        Progress.Instance.GameInfo.Score = count;
    }

    private void BuyProduct(ProductData product)
    {
        if (count >= product.price)
        {
            count -= product.price;
            _score.text = _countText + count.ToString();

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
        {
            CheckLevel();
            UpgradeClick();
        }
        else
        {
            CheckLevel();
            UpgradeAutoClick();
        }
    }

    private void UpgradeClick()
    {
        int summWithProcent = _minSummationClick * 2;

        if (_summationClick != 10)
            if (count >= (ulong)summWithProcent)
            {
                count -= (ulong)summWithProcent;
                _score.text = _countText + count.ToString();
                _minSummationClick = summWithProcent;
                _clickPrice.text = "$ " + (summWithProcent * 2).ToString();
                _summationClick++;

                Progress.Instance.GameInfo.SummClick = _summationClick;
                Progress.Instance.GameInfo.PriceClick = summWithProcent;
                Progress.Instance.GameInfo.Score = count;

                Progress.Instance.Save();
                _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";
            }
            else
                _notEnoughCountUI.SetActive(true);
        else
            _buttons[0].interactable = false;
    }

    private void UpgradeAutoClick()
    {
        int summWithProcent = _minSummationAutoClick * 2;

        if (_summationAutoClick != 10)
            if (count >= (ulong)summWithProcent)
            {
                count -= (ulong)summWithProcent;
                _score.text = _countText + count.ToString();
                _minSummationAutoClick = summWithProcent;
                _autoClickPrice.text = "$ " + (summWithProcent * 2).ToString();
                _summationAutoClick++;

                Progress.Instance.GameInfo.SummAutoClick = _summationAutoClick;
                Progress.Instance.GameInfo.PriceAutoClick = summWithProcent;
                Progress.Instance.GameInfo.Score = count;

                Progress.Instance.Save();

                _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";
            }
            else
                _notEnoughCountUI.SetActive(true);
        else
            _buttons[1].interactable = false;
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

    private void SummClicksUpdate()
    {
        _summationClick = 1;
        _summationAutoClick = 1;
        Progress.Instance.GameInfo.SummClick = _summationClick;
        Progress.Instance.GameInfo.SummAutoClick = _summationAutoClick;

        _buttons[0].interactable = true;
        _buttons[1].interactable = true;

        _minSummationClick = 40;
        _minSummationAutoClick = 200;

        Progress.Instance.GameInfo.PriceClick = _minSummationClick;
        Progress.Instance.GameInfo.PriceAutoClick = _minSummationAutoClick;

        _clickPrice.text = "$ " + (_minSummationClick * 2).ToString();
        _autoClickPrice.text = "$ " + (_minSummationAutoClick * 2).ToString();
        _forcePunch.text = Progress.Instance.GameInfo.SummClick + "\n" + Progress.Instance.GameInfo.SummAutoClick + "\n";

        Progress.Instance.Save();
    }

    private void ScorePlus()
    {
        count += 100;
        Progress.Instance.GameInfo.Score = count;
    }

    private void KillBoss()
    {
        count += Convert.ToUInt16(500 + (2 * Progress.Instance.GameInfo.Level));
        Progress.Instance.GameInfo.Score = count;
        Progress.Instance.Save();
    }
}
