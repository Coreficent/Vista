namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private Genesis land;

        private Queue<Genesis> landQueue = new Queue<Genesis>();

        private TimeController timeController = new TimeController();

        protected virtual void Start()
        {
            DebugUtility.Log("start");
            landQueue.Enqueue(land);
            timeController.Reset();
            DebugUtility.Log("end");
        }

        protected virtual void Update()
        {
            if (timeController.TimePassed > 2.0f)
            {
                if (landQueue.Count > 0)
                {
                    foreach (var genesis in landQueue.Dequeue().Generate())
                    {
                        landQueue.Enqueue(genesis);
                    }
                }
                timeController.Reset();
            }
        }
    }
}
