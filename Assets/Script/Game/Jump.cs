using UnityEngine;

public class Jump : MonoBehaviour
{
    void OnMouseDown()
    {
        if (!PlayerController.instance.isJump) PlayerController.instance.Jump();
        else return;

    }
}
