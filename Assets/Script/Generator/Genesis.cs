namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Genesis : ReinforcedBehavior
    {
        protected virtual void Start()
        {
            BoxCollider collider = GetComponent<BoxCollider>();
            collider.isTrigger = true;

            Rigidbody rigidbody = GetComponent<Rigidbody>();
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            DebugUtility.Log("collision");
        }


        public virtual List<Genesis> Generate()
        {
            return new List<Genesis>();
        }

        protected virtual Genesis Create(Genesis genesis, Vector3 position)
        {
            Genesis result = Instantiate(genesis);
            result.transform.position += position;
            return result;
        }
    }
}
