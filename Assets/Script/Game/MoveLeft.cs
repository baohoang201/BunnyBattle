using UnityEngine;
using UnityEngine.EventSystems;


public class MoveLeft : MonoBehaviour
{
  void OnMouseDrag()
  {
    if(EventSystem.current.currentSelectedGameObject == null && !PlayerController.instance.isJump)
    {
       PlayerController.instance.MoveLeft();
    }
   
  }
}
