using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text countText;
    [SerializeField] TMP_Text autoClickText;
    int count = 0;
    //how much point 1 tap generate
    int tapValue = 1;
    //For Auto Clicker
    int autoClickerPurchase = 20;
    int autoClickerLevel = 0;
    float autoClickerLeveTimeInterval = 1f;
    public void Start()
    {
        StartCoroutine(AutoClickerLoop());
        UpdateUI();
    }
    public void ClickAction()
    {
        count += 1;
        UpdateUI();
    }
    void UpdateUI()
    {
        countText.text = count.ToString();
        autoClickText.text = $"Auto Clicker (Lv {autoClickerLevel})\nCost:{autoClickerPurchase:F0}";
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
    IEnumerator AutoClickerLoop()
    {
        while(true)
        {
            yield return new WaitForSeconds(autoClickerLeveTimeInterval);
            count += autoClickerLevel * tapValue;
            UpdateUI();
        }
    }
    
}
