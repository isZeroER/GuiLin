using UnityEngine;

public class ShuiYueDong : IInteract
{
    public override void Interact()
    {
        UIManager.Instance.OpenUI("introduction");
    }
}