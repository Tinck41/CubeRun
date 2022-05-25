using UnityEngine;

public class OpenWindowButton : MonoBehaviour
{
    [SerializeField] private GameObject _window;

    public void OnOpenWindowButtonClick()
    {
        _window?.SetActive(true);
    }
}
