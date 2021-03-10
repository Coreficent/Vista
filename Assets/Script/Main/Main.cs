namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Tile;
    using Coreficent.Utility;
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

        private readonly TimeController timeController = new TimeController();

        private Land land;
        private Doodad doodad;
        private Track river;
        private Track road;

        private enum GenerationState
        {
            Land,
            Doodad,
            River,
            Road,
            Vista
        }

        private GenerationState state = GenerationState.Land;

        protected virtual void Start()
        {
            land = new Land(board, factory); ;
            doodad = new Doodad(board, factory);
            river = new Track(board, factory);
            road = new Track(board, factory);
            float center = board.Size % 2 == 0 ? board.Size / 2 - 0.5f : board.Size / 2 + 0.0f;
            mainCamera.transform.position = new Vector3(center, center, -board.Size);
            land.Size = board.Size;
            doodad.Size = board.Size;
            timeController.Reset();
            timeController.SetTime(0.01f);
        }

        protected virtual void Update()
        {
            if (Input.GetKey(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }

            if (timeController.Reached)
            {
                switch (state)
                {
                    case GenerationState.Land:
                        if (!Iterate(land, 1000))
                        {
                            state = GenerationState.Doodad;
                        }
                        break;
                    case GenerationState.Doodad:
                        if (!Iterate(doodad, 1000))
                        {
                            Vector3 position = board.RandomPosition();
                            TileBase riverTile = board.Replace(position, factory.RiverStraight);
                            river.Add(riverTile);

                            state = GenerationState.River;
                        }
                        break;

                    case GenerationState.River:
                        if (!Iterate(river, 1))
                        {
                            Vector3 position = board.RandomPosition();
                            TileBase roadTile = board.Replace(position, factory.RoadStraight);
                            road.Add(roadTile);

                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        if (!Iterate(road, 1))
                        {
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

        private bool Iterate(IIterator iterator, int iterationPerSecond)
        {
            int iteration = Mathf.RoundToInt(iterationPerSecond * Time.deltaTime);

            if (iteration < 1)
            {
                ++iteration;
            }

            for (var i = 0; i < iteration; ++i)
            {
                if (iterator.HasNext())
                {
                    iterator.Next();
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
