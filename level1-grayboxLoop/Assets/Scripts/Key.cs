using UnityEngine;

//when a key is collected, it is added to the counter and disappears
public class Keys : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key Collected!");
            gameObject.SetActive(false);
        }
    }
}
