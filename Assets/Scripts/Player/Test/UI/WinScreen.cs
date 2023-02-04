using UnityEngine;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private GameObject _holder;
    [SerializeField] private TMPro.TextMeshProUGUI _nameText;
    [SerializeField] private GameSystem _gameSystem;

    private void Awake()
    {
        _gameSystem.WinGameEvent += OnWin;
    }

    private void OnWin(string winnerName) 
    {
        ShowScreen(winnerName);
    }

    private void ShowScreen(string winnerName) 
    {
        _nameText.text = winnerName;
        ToggleScreen(true);
    }

    private void ToggleScreen(bool enabled) 
    {
        _holder.SetActive(enabled);
    }

    public void HideScreen() 
    {
        ToggleScreen(false);
    }

    private void OnDestroy()
    {
        _gameSystem.WinGameEvent -= OnWin;
    }
}
