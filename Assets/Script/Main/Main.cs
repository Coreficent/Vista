namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        public TileBase Tower1;

        public TileBase River1;

        [SerializeField]
        private TileFactory tileFactory;

        private HashSet<Vector2Int> set = new HashSet<Vector2Int>();



        private readonly TimeController timeController = new TimeController();


        private int towerCount = 0;

        private float timeGap = 0.01f;

        private Queue<Tuple<Vector2Int, TileBase>> queue = new Queue<Tuple<Vector2Int, TileBase>>();

        private enum GenerationState
        {
            Land,
            Tower,
            River,
            Vista
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
                            timeGap = 0.25f;
                            Vector2Int position = tileFactory.Random();
                            queue.Enqueue(new Tuple<Vector2Int, TileBase>(position, River1)); ;
                            set.Add(position);
                            state = GenerationState.River;
                        }

                        --towerCount;

                        break;

                    case GenerationState.River:

                        if (queue.Count > 0)
                        {
                            var rootedGenesis = queue.Dequeue();
                            Vector2Int position = rootedGenesis.Item1;
                            TileBase genesis = rootedGenesis.Item2;

                            tileFactory.PlaceTile(position, genesis);

                            if (genesis.North.Count > 0)
                            {
                                float offset = 1.0f;
                                float angleOffset = 0.0f;
                                Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));
                                if (tileFactory.ValidRange(newPosition))
                                {
                                    if (!set.Contains(newPosition))
                                    {
                                        queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.North[UnityEngine.Random.Range(0, genesis.North.Count)]));
                                        set.Add(newPosition);
                                    }
                                }
                            }

                            if (genesis.East.Count > 0)
                            {
                                float offset = 1.0f;
                                float angleOffset = 90.0f;
                                Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));
                                {
                                    if (tileFactory.ValidRange(newPosition))
                                    {
                                        if (!set.Contains(newPosition))
                                        {
                                            queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.East[UnityEngine.Random.Range(0, genesis.East.Count)]));
                                            set.Add(newPosition);
                                        }
                                    }
                                }
                            }
                            if (genesis.South.Count > 0)
                            {
                                float offset = 1.0f;
                                float angleOffset = 180.0f;
                                Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));

                                if (tileFactory.ValidRange(newPosition))
                                {
                                    if (!set.Contains(newPosition))
                                    {
                                        queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.South[UnityEngine.Random.Range(0, genesis.South.Count)]));
                                        set.Add(newPosition);
                                    }
                                }
                            }
                            if (genesis.West.Count > 0)
                            {
                                float offset = 1.0f;
                                float angleOffset = 270.0f;
                                Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));

                                DebugUtility.Bug("go west", newPosition);

                                if (tileFactory.ValidRange(newPosition))
                                {
                                    if (!set.Contains(newPosition))
                                    {
                                        queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.West[UnityEngine.Random.Range(0, genesis.West.Count)]));
                                        set.Add(newPosition);
                                    }
                                }
                            }

                        }
                        else
                        {
                            state = GenerationState.Vista;
                        }

                        break;

                    case GenerationState.Vista:

                        // DebugUtility.Log("Vista");

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
