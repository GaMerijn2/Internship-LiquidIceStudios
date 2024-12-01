using UnityEngine;
using UnityEngine.UI;

public class SpawnButtonInitializer
{
    private readonly System.Action<int> _spawnCoinAction;

    public SpawnButtonInitializer(System.Action<int> spawnCoinAction)
    {
        _spawnCoinAction = spawnCoinAction;
    }

    public void InitializeButtons()
    {
        foreach (GameObject buttonObject in GameObject.FindGameObjectsWithTag("Button"))
        {
            if (buttonObject.TryGetComponent(out Button button))
            {
                // Clear any existing listeners to prevent multiple calls
                button.onClick.RemoveAllListeners();

                if (int.TryParse(buttonObject.name.Replace("SpawnButton ", ""), out int columnIndex))
                {
                    button.onClick.AddListener(() => _spawnCoinAction(columnIndex));
                }
            }
        }
    }
}