using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrategiSpil
{
    interface ICollider
    {
        //NewCollider interface
        void OnCollisionStay(Collider other);

        void OnCollisionEnter(Collider other);

        void OnCollisionExit(Collider other);

    }
}
