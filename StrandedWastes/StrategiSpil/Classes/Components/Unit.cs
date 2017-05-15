using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace StrategiSpil
{
    enum unitType { Worker, Soldier, Trooper, ScoutCar, Tank };
    enum unitAllegiance { Player, Enemy };
    class Unit : Component, IUpdateable, ILoadable
    {
        #region Fields
        /// <summary>
        /// Enums for various information, such as unit allegiance, type and a strategy to determine what actions to take.
        /// </summary>
        unitType myType;
        unitAllegiance myTeam;
        IUnitStrat myStrategy;
        #endregion

        #region Properties
        /// <summary>
        /// All properties will be contained in here as to easier find and edit them or add if needed.
        /// </summary>
        public unitType MyType { get => myType; set => myType = value; }


        #endregion
        /// <summary>
        /// Constructor for the Unit class, this is where the Unit is essentially built.
        /// </summary>
        /// <param name="go"></param>
        /// <param name="type"></param>
        /// <param name="team"></param>
        public Unit(GameObject go, unitType type, unitAllegiance team) : base(go)
        {
            MyType = type;
            myTeam = team;
        }


        /// <summary>
        /// Implumentation of the ILoadable interface.
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            switch (myType)
            {
                case unitType.Worker:
                    myStrategy = new WorkerStrat(GameObject);
                    break;
                case unitType.Soldier:
                    myStrategy = new WorkerStrat(GameObject);
                    break;
                case unitType.Trooper:
                    myStrategy = new WorkerStrat(GameObject);
                    break;
                case unitType.ScoutCar:
                    myStrategy = new WorkerStrat(GameObject);
                    break;
                case unitType.Tank:
                    myStrategy = new WorkerStrat(GameObject);
                    break;
            }
        }

        public void Update()
        {
            
        }
    }
}
