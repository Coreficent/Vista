namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Genesis : ReinforcedBehavior
    {
        protected virtual void Start()
        {

        }

        public virtual List<Genesis> Generate(Genesis genesis)
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
