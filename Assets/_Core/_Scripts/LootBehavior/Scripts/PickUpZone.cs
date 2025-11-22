using UnityEngine;

public class PickUpZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var drop = other.GetComponent<IDrop>();
        if (drop != null)
        {
            drop.OnPickedUp();
        }
    }

}
