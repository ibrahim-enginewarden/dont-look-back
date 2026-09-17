using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door Settings")]
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 3f;

    [Header("Lock")]
    [SerializeField] private bool requiresKey = false;
    private bool isOpen;
    private Quaternion closedRotation;
    private Quaternion openRotation;
    private void Start()
    {
        closedRotation = transform.localRotation;
        openRotation =
            closedRotation * Quaternion.Euler(0f, openAngle, 0f);
    }
    public void Interact(bool hasKey)
    {
        if(requiresKey && !hasKey)
        {
            Debug.Log("The door is locked.");
            return;
        }
        isOpen = !isOpen;
    }
    private void Update()
    {
        Quaternion targetRotation =
            isOpen ? openRotation : closedRotation;

        transform.localRotation = Quaternion.Slerp(
            transform.localRotation,
            targetRotation,
            openSpeed * Time.deltaTime);
    }

}
