using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Robotfists
{
    class Instantiator : IInstantiator
    {
        readonly IInjectInto _inject;

        public Instantiator(IInjectInto inject)
        {
            _inject = inject;
        }

        void BeforeInstantiate(GameObject original)
        {
            original.SetActive(false);
        }

        void AfterInstantiate(GameObject original, GameObject newObject)
        {
            original.SetActive(true);
            _inject.Invoke(newObject);
        }

        public GameObject Instantiate(GameObject original)
        {
            BeforeInstantiate(original);
            var newObject = GameObject.Instantiate(original);
            AfterInstantiate(original, newObject);
            return newObject;
        }

        public GameObject Instantiate(GameObject original, Transform parent)
        {
            BeforeInstantiate(original);
            var newObject = GameObject.Instantiate(original, parent);
            AfterInstantiate(original, newObject);
            return newObject;
        }

        public GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation)
        {
            BeforeInstantiate(original);
            var newObject = GameObject.Instantiate(original, position, rotation);
            AfterInstantiate(original, newObject);
            return newObject;
        }

        public GameObject Instantiate(GameObject original, Vector3 position, Quaternion rotation, Transform parent)
        {
            BeforeInstantiate(original);
            var newObject = GameObject.Instantiate(original, position, rotation, parent);
            AfterInstantiate(original, newObject);
            return newObject;
        }

        public GameObject Instantiate(GameObject original, Transform parent, bool instantiateInWorldSpace)
        {
            BeforeInstantiate(original);
            var newObject = GameObject.Instantiate(original, parent, instantiateInWorldSpace);
            AfterInstantiate(original, newObject);
            return newObject;
        }
    }
}
