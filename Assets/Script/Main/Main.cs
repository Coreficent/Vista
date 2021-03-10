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
        [SerializeField]
        private Board board;

        [SerializeField]
        private Factory factory;

        private HashSet<Vector3> set = new HashSet<Vector3>();

        private readonly TimeController timeController = new TimeController();
        private float timeGap = 0.01f;
        private Queue<Tuple<Vector3, TileBase>> queue = new Queue<Tuple<Vector3, TileBase>>();


        private Land land;
        private Doodad doodad;
        private Track track;

        private enum GenerationState
        {
            Land,
            Doodad,
            River,
            Vista
        }

        private GenerationState state = GenerationState.Land;

        protected virtual void Start()
        {
            land = new Land(board, factory); ;
            doodad = new Doodad(board, factory);
            track = new Track(board, factory);
            land.Radius = board.Radius;
            doodad.Radius = board.Radius;
            timeController.Reset();
        }

        protected virtual void Update()
        {
            if (timeController.TimePassed > timeGap)
            {
                switch (state)
                {
                    case GenerationState.Land:
                        if (land.HasNext())
                        {
                            //                            board.Place(land.Next(), factory.Create(Factory.Grass));
                            land.Next();
                        }
                        else
                        {
                            timeGap = 0.01f;
                            state = GenerationState.Doodad;
                        }
                        break;
                    case GenerationState.Doodad:
                        if (doodad.HasNext())
                        {
                            //board.Replace(doodad.Next(), factory.Create(Factory.Tower));
                            doodad.Next();
                        }
                        else
                        {
                            timeGap = 1.0f;
                            //Vector3 position = board.Random();
                            //queue.Enqueue(new Tuple<Vector3, TileBase>(position, River1)); ;
                            //set.Add(position);


                            Vector3 position = board.RandomPosition();
                            TileBase river = board.Replace(position, factory.Create(Factory.RiverStraight));
                            track.Add(river);

                            state = GenerationState.River;
                        }


                        break;

                    case GenerationState.River:

                        if (track.HasNext())
                        {
                            track.Next();
                        }
                        else
                        {
                            timeGap = 1.0f;
                            state = GenerationState.Vista;
                        }

                        //if (queue.Count > 0)
                        //{
                        //    var rootedGenesis = queue.Dequeue();
                        //    Vector3 position = rootedGenesis.Item1;
                        //    TileBase genesis = rootedGenesis.Item2;

                        //    board.Replace(position, genesis);

                        //    if (genesis.North.Count > 0)
                        //    {
                        //        float offset = 1.0f;
                        //        float angleOffset = 0.0f;
                        //        Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));
                        //        if (board.ValidRange(newPosition))
                        //        {
                        //            if (!set.Contains(newPosition))
                        //            {
                        //                queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.North[UnityEngine.Random.Range(0, genesis.North.Count)]));
                        //                set.Add(newPosition);
                        //            }
                        //        }
                        //    }

                        //    if (genesis.East.Count > 0)
                        //    {
                        //        float offset = 1.0f;
                        //        float angleOffset = 90.0f;
                        //        Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));
                        //        {
                        //            if (board.ValidRange(newPosition))
                        //            {
                        //                if (!set.Contains(newPosition))
                        //                {
                        //                    queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.East[UnityEngine.Random.Range(0, genesis.East.Count)]));
                        //                    set.Add(newPosition);
                        //                }
                        //            }
                        //        }
                        //    }
                        //    if (genesis.South.Count > 0)
                        //    {
                        //        float offset = 1.0f;
                        //        float angleOffset = 180.0f;
                        //        Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));

                        //        if (board.ValidRange(newPosition))
                        //        {
                        //            if (!set.Contains(newPosition))
                        //            {
                        //                queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.South[UnityEngine.Random.Range(0, genesis.South.Count)]));
                        //                set.Add(newPosition);
                        //            }
                        //        }
                        //    }
                        //    if (genesis.West.Count > 0)
                        //    {
                        //        float offset = 1.0f;
                        //        float angleOffset = 270.0f;
                        //        Vector2Int newPosition = new Vector2Int(position.x + Mathf.RoundToInt(Mathf.Sin((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset), position.y + Mathf.RoundToInt(Mathf.Cos((transform.eulerAngles.y + angleOffset) * Mathf.Deg2Rad) * offset));

                        //        DebugUtility.Bug("go west", newPosition);

                        //        if (board.ValidRange(newPosition))
                        //        {
                        //            if (!set.Contains(newPosition))
                        //            {
                        //                queue.Enqueue(new Tuple<Vector2Int, TileBase>(newPosition, genesis.West[UnityEngine.Random.Range(0, genesis.West.Count)]));
                        //                set.Add(newPosition);
                        //            }
                        //        }
                        //    }

                        //}
                        //else
                        //{
                        //    state = GenerationState.Vista;
                        //}

                        break;

                    case GenerationState.Vista:

                        DebugUtility.Log("Vista");

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
