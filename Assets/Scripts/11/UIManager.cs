using UnityEngine;

public class UIManager:UnitySingleton<UIManager>
{
    [Header("界面")]
    public GameObject introduction;
    public GameObject micPhone;
    
    public void OpenUI(string uiName)
    {
        switch (uiName)
        {
            case "micPhone":
                micPhone.SetActive(true);
                break;
            case "introduction":
                introduction.SetActive(true);
                break;
            default:
                break;
        }
    }        
}