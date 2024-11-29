using UnityEngine;

public class GameObjectUtil : MonoBehaviour
{

    public void ToggleActive(GameObject target)
    {
        target.SetActive(!target.activeSelf);
    }
    
}