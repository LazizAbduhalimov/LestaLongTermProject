using System.Collections;
using UnityEngine;

namespace PoolSystem.Alternative
{
    public class PoolObject : MonoBehaviour
    {
        public float LifeTime = 2f;

        private void OnEnable()
        {
            if (LifeTime > 0)
                StartCoroutine(nameof(LifeCoroutine));
        }

        private void OnDisable()
        {
            if (LifeTime > 0)
                StopCoroutine(nameof(LifeCoroutine));
        }

        private IEnumerator LifeCoroutine()
        {
            yield return new WaitForSecondsRealtime(LifeTime);
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
