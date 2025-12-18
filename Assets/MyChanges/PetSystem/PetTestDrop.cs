using PoolSystem.Alternative;
using System.Collections.Generic;
using UnityEngine;

public class PetTestDrop : MonoBehaviour
{
    [SerializeField] private List<PoolContainer> _pool = new List<PoolContainer>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out PetManager petManager))
        {
            return;
        }

        if (!petManager.CanAdd())
        {
            gameObject.SetActive(false);
            return;
        }

        var randomContainer = _pool[Random.Range(0, _pool.Count)];
        var obj = randomContainer.Pool.GetFreeElement(true);

        obj.SetActive(true);

        if (obj.TryGetComponent<IPet>(out var pet))
        {
            petManager.AddPet(pet);
        }

        gameObject.SetActive(false);
    }
}