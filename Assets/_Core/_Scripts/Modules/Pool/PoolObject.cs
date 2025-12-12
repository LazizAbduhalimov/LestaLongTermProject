using System.Collections;
using UnityEngine;

namespace PoolSystem.Alternative
{
    public class PoolObject : MonoBehaviour
    {
        public float LifeTime = 2f;

        protected virtual void OnEnable()
        {
            if (LifeTime > 0)
                StartCoroutine(nameof(LifeCoroutine));
        }

        protected virtual void OnDisable()
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
