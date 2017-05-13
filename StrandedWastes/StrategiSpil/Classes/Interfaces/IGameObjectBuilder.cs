using Microsoft.Xna.Framework;

namespace StrategiSpil
{
    internal interface IGameObjectBuilder
    {
        void Build(Vector2 position, float layerDepth, float animationSpeed);

        GameObject GetResult();
    }
}