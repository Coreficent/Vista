namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private TileFactory tileFactory;

        private Queue<Tuple<int, int>> tileQueue = new Queue<Tuple<int, int>>();

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
                            tileFactory.NextTile(tileFactory.Tile, tileFactory.Next());
                        }
                        else
                        {
                            tileQueue.Enqueue(tileFactory.Random());
                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        
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
