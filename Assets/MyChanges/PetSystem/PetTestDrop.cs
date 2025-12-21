using PoolSystem.Alternative;
using System.Collections.Generic;
using UnityEngine;

public class PetTestDrop : MonoBehaviour
{
    [SerializeField] private GameObject _petForSpawn;
    [SerializeField] private int _petPoolCount;

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

        var poolService = new PoolService("DropSystem");
        var pool = poolService.GetOrRegisterPool<FollowingPet>(_petForSpawn.GetComponent<FollowingPet>(), _petPoolCount,null ,true);

        var pet = pool.GetFreeElement(true).GetComponent<IPet>();
        petManager.AddPet(pet);





        gameObject.SetActive(false);
    }
}