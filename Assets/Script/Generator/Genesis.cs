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

        public virtual List<Genesis> Generate()
        {
            return new List<Genesis>();
        }

        protected virtual IEnumerable<Genesis> Create(Genesis genesis, Vector3 position)
        {
            if (!Physics.CheckBox(transform.position + position, new Vector3(0.25f, 0.25f, 0.25f)))
            {
                Genesis clone = Instantiate(genesis);
                clone.transform.position += position;
                yield return clone;
            }
        }
    }
}
