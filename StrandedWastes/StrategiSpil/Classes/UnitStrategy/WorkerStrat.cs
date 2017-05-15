using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategiSpil
{
    class WorkerStrat : IUnitStrat
    {
        GameObject gameObject;
        int ressources;
        Vector2 target;

        public WorkerStrat(GameObject go)
        {
            gameObject = go;

        }

        public void Execute()
        {
            
        }

        private void Mine()
        {

        }


    }
}
