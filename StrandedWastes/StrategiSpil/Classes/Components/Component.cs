namespace StrategiSpil
{
    public class Component
    {
        private GameObject gameObject;
        public GameObject GameObject { get { return gameObject; } }

        public Component(GameObject gameObject)
        {
            this.gameObject = gameObject;
        }

        public Component()
        {
        }
    }
}
