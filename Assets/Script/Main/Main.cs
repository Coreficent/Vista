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

        public Genesis River1;

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
                            tileFactory.RepairTile(tileFactory.Next());
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
                            state = GenerationState.Road;
                        }

                        --towerCount;

                        break;

                    case GenerationState.Road:
                        tileFactory.RepairTile(tileFactory.Random());
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
