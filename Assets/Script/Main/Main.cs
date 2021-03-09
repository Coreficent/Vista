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

        private HashSet<Vector2Int> set = new HashSet<Vector2Int>();



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
                            tileFactory.Enqueue(tileFactory.Random());
                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        if (tileFactory.QueueCount() > 0)
                        {
                            Vector2Int position = tileFactory.Dequeue();

                            tileFactory.NextTile(tileFactory.TileRiverStraight, position);

                            if (!set.Contains(position))
                            {
                                tileFactory.Enqueue(new Vector2Int(position.x, position.y + 1));
                                tileFactory.Enqueue(new Vector2Int(position.x, position.y - 1));
                                set.Add(position);
                            }

                            DebugUtility.Log("queue size", tileFactory.QueueCount());


                            string r = "";
                            foreach (var i in set)
                            {
                                r += i;
                            }
                            DebugUtility.Log("set size", r);

                        }
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
