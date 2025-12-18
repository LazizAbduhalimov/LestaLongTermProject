using UnityEngine;


public class Brain : MonoBehaviour
{
    [SerializeField] private StrategySO strategyAsset;

    private StrategySO strategyInstance;
    private Coroutine aiRoutine;

    private void Start()
    {
        if (strategyAsset == null)
        {
            Debug.LogError("Brain: Strategy is missing", this);
            return;
        }

        strategyInstance = Instantiate(strategyAsset);
        strategyInstance.Init(gameObject);

        aiRoutine = StartCoroutine(strategyInstance.AI());
    }
    private void OnEnable()
    {
        if (strategyInstance == null && strategyAsset != null)
        {
            strategyInstance = Instantiate(strategyAsset);
            strategyInstance.Init(gameObject);
        }

        if (strategyInstance != null)
            aiRoutine = StartCoroutine(strategyInstance.AI());

    }

    private void OnDisable()
    {
        if (aiRoutine != null)
            StopCoroutine(aiRoutine);
    }
}
