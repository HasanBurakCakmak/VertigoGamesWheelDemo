using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChildSerializationForDeathScreen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Button GiveUpButton;
    [SerializeField] private Button RetryButton;
    [SerializeField] private TextMeshProUGUI GoldToRetryValue;

    public void SetRetryValue(string Value)
    {
        GoldToRetryValue.text = Value;
    }

    public Button getGiveUpButton()
    {
        return GiveUpButton;
    }
    public Button getRetryButton()
    {
        return RetryButton;
    }
}
