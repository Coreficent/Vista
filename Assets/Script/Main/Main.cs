namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Utility;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private Camera mainCamera;

        [SerializeField]
        private Board board;

        [SerializeField]
        private Factory factory;

        [SerializeField]
        private Text text;

        private string statePrefix = "State: ";

        private readonly TimeController timeController = new TimeController();

        private Land land;
        private Doodad doodad;
        private Track river;
        private Track road;
        private Rectifier rectifier;

        private enum GenerationState
        {
            Land,
            Doodad,
            River,
            Road,
            Rectification,
            Vista
        }

        private GenerationState state = GenerationState.Land;

        protected virtual void Start()
        {
            land = new Land(board, factory); ;
            doodad = new Doodad(board, factory);
            river = new Track(board, factory, factory.RiverStraight, board.Size * 2);
            road = new Track(board, factory, factory.RoadStraight, board.Size * 4);
            rectifier = new Rectifier(board, factory);
            float center = board.Size % 2 == 0 ? board.Size / 2 - 0.5f : board.Size / 2 + 0.0f;
            mainCamera.transform.position = new Vector3(center, center, -board.Size);
            land.Size = board.Size;
            doodad.Size = board.Size;
            text.text = statePrefix + "Generating Land";
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
                        if (!Iterate(land, 100))
                        {
                            text.text = statePrefix + "Generating Doodad";
                            state = GenerationState.Doodad;
                        }
                        break;
                    case GenerationState.Doodad:
                        if (!Iterate(doodad, 10))
                        {
                            river.Stage();
                            text.text = statePrefix + "Generating River";
                            state = GenerationState.River;
                        }
                        break;

                    case GenerationState.River:
                        if (!Iterate(river, 10))
                        {
                            road.Stage();
                            text.text = statePrefix + "Generating Road";
                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        if (!Iterate(road, 10))
                        {
                            rectifier.Stage();
                            text.text = statePrefix + "Rectifying Scene";
                            state = GenerationState.Rectification;
                        }
                        break;

                    case GenerationState.Rectification:
                        if (!Iterate(rectifier, 100))
                        {
                            text.text = statePrefix + "Complete";
                            state = GenerationState.Vista;
                        }
                        break;

                    case GenerationState.Vista:
                        float offset = 30.0f;
                        float speed = 15.0f;

                        if (board.transform.eulerAngles.x < offset)
                        {
                            board.transform.eulerAngles += new Vector3(speed * Time.deltaTime, 0.0f, 0.0f);
                        }
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
