using System.Collections.Generic;
using UnityEngine;

public class PetManager : MonoBehaviour
{
    [SerializeField] private SerializableQueue<Transform> _freePositions = new();
    private List<IPet> _pets = new();

    private void UpdatePets()
    {
        foreach (var pet in _pets)
        {
            pet.UpdateBehavior();
        }
    }
    private void Update()
    {
        UpdatePets();
    }
    public void AddPet(IPet pet)
    {
        _pets.Add(pet);
        pet.InitialUpdate(_freePositions.Dequeue());
    }

    public void AddPosition(Transform transform)
    {
        _freePositions.Enqueue(transform);
    }
    public bool CanAdd()
    {
        if (_freePositions.Count > 0) return true;

        return false;
    }

}
