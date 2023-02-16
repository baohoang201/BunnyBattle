using UnityEngine.EventSystems;
using UnityEngine;

public class MoveRight : MonoBehaviour
{
    void OnMouseDrag()
    {

        if (EventSystem.current.currentSelectedGameObject == null && !PlayerController.instance.isJump)
        {
            PlayerController.instance.MoveRight();
        }

    }
}
