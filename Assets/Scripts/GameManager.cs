using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text autoClickText;
    [SerializeField] TMP_Text tapMultiplierText;

    [SerializeField] GameObject clickButton;
    int count = 0;
    //how much point 1 tap generate
    int tapValue = 1;
    //For Auto Clicker
    int autoClickerPurchase = 20;
    int autoClickerLevel = 0;
    float autoClickerLeveTimeInterval = 1f;

    //For Tap Multiplier
    int tapMultiplierCost = 20;
    int tapMultiplierLevel = 1;
    
    public float scale = 1.5f;
    public float duration = 0.5f;
    public Ease easeType = Ease.InOutCubic;
    public bool isAnimating = false;

    void Start()
    {
        LoadGame();
        StartCoroutine(AutoClickerLoop());
        UpdateUI();
    }

    void OnApplicationQuit()
    {
        SaveGame();
    }

    public void SaveGame()
    {
        PlayerPrefs.SetString("count",count.ToString());
        PlayerPrefs.SetInt("tapValue",tapValue);
        PlayerPrefs.SetInt("autoClickerPurchase",autoClickerPurchase);
        PlayerPrefs.SetInt("autoClickerLevel",autoClickerLevel);
        PlayerPrefs.SetInt("tapMultiplierCost",tapMultiplierCost);
        PlayerPrefs.SetInt("tapMultiplierLevel",tapMultiplierLevel);
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("count"))
        {
            int.TryParse(PlayerPrefs.GetString("count"),out count);
            tapMultiplierLevel = PlayerPrefs.GetInt("tapMultiplierLevel");
            tapMultiplierCost = PlayerPrefs.GetInt("tapMultiplierCost");
            autoClickerLevel = PlayerPrefs.GetInt("autoClickerLevel");
            autoClickerPurchase = PlayerPrefs.GetInt("autoClickerPurchase");
            tapValue = PlayerPrefs.GetInt("tapValue");

        }
    }
    public void ClickAction()
    {
        count += tapValue;
        UpdateUI();
        if (isAnimating)
        return;
        Vector2 original = transform.localScale;
        Vector2 target = original * scale;
        Sequence seq = DOTween.Sequence();
        seq.Append(clickButton.transform.DOScale(target,duration).SetEase(easeType));
        seq.Append(clickButton.transform.DOScale(original,duration).SetEase(easeType));
        seq.OnComplete(()=>isAnimating = false);

    }
    void UpdateUI()
    {
        countText.text = count.ToString();
        autoClickText.text = $"Auto Clicker (Lv {autoClickerLevel})\nCost:{autoClickerPurchase:F0}";
        tapMultiplierText.text = $"Tap Multiplier (Lv {tapMultiplierLevel})\nCost:{tapMultiplierCost:F0}";
    }

    public void ResetGame()
    {
        PlayerPrefs.SetString("count","0");
        PlayerPrefs.SetInt("tapValue",0);
        PlayerPrefs.SetInt("autoClickerPurchase",0);
        PlayerPrefs.SetInt("autoClickerLevel",0);
        PlayerPrefs.SetInt("tapMultiplierCost",0);
        PlayerPrefs.SetInt("tapMultiplierLevel",0);
        count = 0;
        tapValue = 1;
        autoClickerPurchase = 20;
        autoClickerLevel = 0;
        tapMultiplierCost = 20;
        tapMultiplierLevel = 1;
    }

    public void ClickOnAutoClick()
    {
        //purchase autoclick
        if (count < autoClickerPurchase)
        return;
        count -= autoClickerPurchase;
        autoClickerLevel += 1;
        autoClickerPurchase *= 2;
        UpdateUI();
    }

    public void ClickOnTapMultiplier()
    {
        if (count < tapMultiplierCost)
        return;
        count -= tapMultiplierCost;
        tapMultiplierLevel += 1;
        tapValue += 2;
        tapMultiplierCost *= 2;
        UpdateUI();
    }
    IEnumerator AutoClickerLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(autoClickerLeveTimeInterval);
            count += autoClickerLevel;
            UpdateUI();
        }
    }
    
}
