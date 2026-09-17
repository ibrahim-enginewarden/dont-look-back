using UnityEngine;

public class Key : MonoBehaviour
{
    public void Collect()
    {
        Debug.Log("Key collected.");

        Destroy(gameObject);
    }
}
