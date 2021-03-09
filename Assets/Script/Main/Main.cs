namespace Coreficent.Main
{
    using Coreficent.Controller;
    using Coreficent.Generator;
    using Coreficent.Utility;
    using System.Collections.Generic;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        [SerializeField]
        private TileFactory tileFactory;

        private Queue<Genesis> landQueue = new Queue<Genesis>();

        private TimeController timeController = new TimeController();

        private Board board = new Board(7);

        protected virtual void Start()
        {
            timeController.Reset();
        }

        protected virtual void Update()
        {
            if (timeController.TimePassed > 0.1f)
            {
                if(board.HasNext())
                {
                    tileFactory.NextTile(board.Next());
                }
                timeController.Reset();
            }
        }
    }
}
