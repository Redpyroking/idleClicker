using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TMP_Text countText;
    int count = 0;

    public void Start()
    {
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
    }
}
