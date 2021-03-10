namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Tile;
    using Coreficent.Utility;
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private Camera mainCamera;

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
            float center = board.Size % 2 == 0 ? board.Size / 2 - 0.5f : board.Size / 2 + 0.0f;
            mainCamera.transform.position = new Vector3(center, center, -board.Size);
            land.Size = board.Size;
            doodad.Size = board.Size;
            timeController.Reset();
        }

        protected virtual void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }

            if (timeController.TimePassed > timeGap)
            {
                switch (state)
                {
                    case GenerationState.Land:
                        if (land.HasNext())
                        {
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
                            doodad.Next();
                        }
                        else
                        {
                            timeGap = 0.01f;

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
                            timeGap = 0.1f;
                            state = GenerationState.Vista;
                        }
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
