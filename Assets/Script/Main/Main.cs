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
        public Genesis Tower1;

        [SerializeField]
        private TileFactory tileFactory;

        private HashSet<Vector2Int> set = new HashSet<Vector2Int>();



        private readonly TimeController timeController = new TimeController();


        private int towerCount = 0;

        private float timeGap = 0.01f;

        private enum GenerationState
        {
            Land,
            Tower,
            Road
        }

        private GenerationState state = GenerationState.Land;

        protected virtual void Start()
        {
            timeController.Reset();
        }

        protected virtual void Update()
        {
            if (timeController.TimePassed > timeGap)
            {
                switch (state)
                {
                    case GenerationState.Land:

                        if (tileFactory.HasNext())
                        {
                            tileFactory.PlaceTile(tileFactory.Next());
                        }
                        else
                        {
                            timeGap = 0.01f;
                            towerCount = tileFactory.size;
                            state = GenerationState.Tower;
                        }
                        break;

                    case GenerationState.Tower:
                        if (towerCount > 0)
                        {
                            tileFactory.PlaceTile(tileFactory.Random(), Tower1);
                        }
                        else
                        {
                            timeGap = 1.0f;
                            tileFactory.Add(tileFactory.Random());
                            state = GenerationState.Road;
                        }

                        --towerCount;

                        break;

                    case GenerationState.Road:
                        if (tileFactory.QueueCount() > 0)
                        {
                            Vector2Int position = tileFactory.Remove();

                            tileFactory.PlaceTile(position);


                            foreach (var i in tileFactory.GetTile(position).GetNeighbors(position))
                            {
                                if (!set.Contains(i))
                                {
                                    tileFactory.Add(i);
                                }
                            }

                            set.Add(position);


                            // DebugUtility.Log("queue size", tileFactory.QueueCount());
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
