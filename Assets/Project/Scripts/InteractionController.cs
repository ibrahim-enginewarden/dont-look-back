using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionController : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private float interactionDistance = 3f;
    private bool hasKey = false;
    private void Update()
    {
        if(Keyboard.current != null &&
           Keyboard.current.eKey.wasPressedThisFrame)
        {
            TryInteract();
        }
    }
    private void TryInteract()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
            );

        if(Physics.Raycast(
           ray,
           out RaycastHit hit,
           interactionDistance))
        {
            Door door = hit.collider.GetComponentInParent<Door>();

            if(door != null)
            {
                door.Interact(hasKey);
                return;
            }

            Note note = hit.collider.GetComponentInParent<Note>();

            if(note != null)
            {
                note.Read();
                return;
            }

            Key key = hit.collider.GetComponentInParent<Key>();

            if(key != null)
            {
                key.Collect();
                hasKey = true;
                return;
            }
        }
    }
}
