using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;

public class MainMenuCanvas : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputField;
    [SerializeField] private Button _hostButton;
    [SerializeField] private Button _clientButton;
    [SerializeField] private GameObject _holder;


    private bool _canContinue = false;
    private StringBuilder _nameSB = new StringBuilder();

    void Start()
    {
        _inputField.onEndEdit.AddListener(OnInputEndEdit);
        _hostButton.onClick.AddListener(OnHostClick);
        _clientButton.onClick.AddListener(OnClientClick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnHostClick() 
    {
        if (!_canContinue) return;

        CustomNetworkManager.singleton.StartHost();
    }

    private void OnClientClick() 
    {

        if (!_canContinue) return;

        CustomNetworkManager.singleton.StartClient();
    }

    private void OnInputEndEdit(string s) 
    {
        if (String.IsNullOrEmpty(s)) 
        {
            _canContinue = false;
            return;
        }
        
        _nameSB = new StringBuilder(s);
        
        _canContinue = true;
    }

    private void OnDestroy()
    {
        _inputField.onEndEdit.RemoveListener(OnInputEndEdit);
        _hostButton.onClick.RemoveListener(OnHostClick);
        _clientButton.onClick.RemoveListener(OnClientClick);
    }
}
