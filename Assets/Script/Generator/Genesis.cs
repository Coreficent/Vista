namespace Coreficent.Generator
{
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Genesis : ReinforcedBehavior
    {

        protected List<Vector3> Neighbors = new List<Vector3>();
        protected float Chance = 0.5f;

        protected virtual void Start()
        {

        }

        public virtual List<Genesis> Generate()
        {
            DebugUtility.Log("Generate Empty");
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
