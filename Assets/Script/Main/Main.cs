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
        private TileFactory tileFactory;

        private Queue<Genesis> tileQueue = new Queue<Genesis>();

        private readonly TimeController timeController = new TimeController();

        private enum GenerationState
        {
            Land,
            Road
        }

        private GenerationState state = GenerationState.Land;

        protected virtual void Start()
        {
            timeController.Reset();
        }

        protected virtual void Update()
        {
            if (timeController.TimePassed > 0.1f)
            {
                switch (state)
                {
                    case GenerationState.Land:

                        if (tileFactory.HasNext())
                        {
                            tileFactory.NextTile(tileFactory.Next());
                        }
                        else
                        {
                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        DebugUtility.Log("road");
                        break;
                    default:
                        DebugUtility.Warn("unexpected generation state");
                        break;
                }

                timeController.Reset();
            }
        }
    }
}
