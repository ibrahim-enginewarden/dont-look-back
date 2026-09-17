using UnityEngine;

public class Note : MonoBehaviour
{
    [TextArea(3, 8)]
    [SerializeField]
    private string message =
        "I left the spare key where we always used to hide it.";
    public void Read()
    {
        Debug.Log(message);
    }
}