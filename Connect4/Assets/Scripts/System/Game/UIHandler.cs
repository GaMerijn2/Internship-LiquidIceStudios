using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TMP_InputField amountForWinInputField;
    private RowCheck rowCheck;

    public Button confirmButton;

    private void Start()
    {
        rowCheck = FindObjectOfType<RowCheck>();

        amountForWinInputField.text = rowCheck.amountForWin.ToString();

        if (confirmButton != null)
        {
            confirmButton.onClick.AddListener(OnConfirmButtonClicked);
        }
    }

    private void OnConfirmButtonClicked()
    {
        if (int.TryParse(amountForWinInputField.text, out int newAmountForWin))
        {
            rowCheck.SetAmountForWin(newAmountForWin);
        }
        else
        {
            Debug.LogWarning("Invalid input. Please enter a valid number.");
        }
    }
}