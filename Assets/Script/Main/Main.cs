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

        private readonly TimeController timeController = new TimeController();
        private readonly string statePrefix = "State: ";

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
            land = new Land(board, factory, Random.Range(0.025f, 0.75f)); ;
            doodad = new Doodad(board, factory, Random.Range(0.05f, 0.2f));
            river = new Track(board, factory, factory.RiverStraight, Random.Range(0.05f, 0.10f));
            road = new Track(board, factory, factory.RoadStraight, Random.Range(0.075f, 0.15f));
            rectifier = new Rectifier(board, factory);
            float center = board.Size % 2 == 0 ? board.Size / 2 - 0.5f : board.Size / 2 + 0.0f;
            mainCamera.transform.position = new Vector3(center, center, -board.Size);
            land.Size = board.Size;
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
                        if (!Iterate(land, board.Size * 4))
                        {
                            text.text = statePrefix + "Generating Doodad";
                            state = GenerationState.Doodad;
                        }
                        break;
                    case GenerationState.Doodad:
                        if (!Iterate(doodad, board.Size))
                        {
                            river.Stage();
                            text.text = statePrefix + "Generating River";
                            state = GenerationState.River;
                        }
                        break;

                    case GenerationState.River:
                        if (!Iterate(river, board.Size))
                        {
                            road.Stage();
                            text.text = statePrefix + "Generating Road";
                            state = GenerationState.Road;
                        }
                        break;
                    case GenerationState.Road:
                        if (!Iterate(road, board.Size))
                        {
                            rectifier.Stage();
                            text.text = statePrefix + "Rectifying Scene";
                            state = GenerationState.Rectification;
                        }
                        break;

                    case GenerationState.Rectification:
                        if (!Iterate(rectifier, board.Size * 10))
                        {
                            text.text = statePrefix + "Complete. Use Q, E, and movement keys to move the camera.";
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

                        float moveSpeed = 10.0f;

                        float zoomSpeed = 10.0f;

                        float zoom = 0.0f;

                        if (Input.GetKey(KeyCode.Q))
                        {
                            zoom += zoomSpeed;
                        }
                        if (Input.GetKey(KeyCode.E))
                        {
                            zoom -= zoomSpeed;
                        }

                        mainCamera.transform.position += new Vector3(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime, zoom * Time.deltaTime);

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
