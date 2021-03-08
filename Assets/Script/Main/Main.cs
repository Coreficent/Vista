namespace Coreficent.Main
{
    using Coreficent.Utility;
    using UnityEngine;

    public class Main : ReinforcedBehavior
    {
        public GameObject publicVar;

        [SerializeField]
        private GameObject serializeVar;

        private GameObject privateVar;

        void Start()
        {
            DebugUtility.Log("start");
            DebugUtility.Log("end");
        }
    }
}
