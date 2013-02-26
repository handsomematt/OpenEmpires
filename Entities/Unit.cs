using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenEmpires.Entities
{
    public class Unit : EntityBase
    {
        public enum UnitStates : byte
        {
            Moving,
            AttackMoving,
            Patrol,
            Stopped,
        }

        public UnitStates State;

        public Unit()
        {
            EntityId = EntityIds.Unit;
            State = UnitStates.Stopped;
        }


        public override void ParseCustomStream(BinaryReader reader)
        {
            throw new NotImplementedException();
        }

        protected override System.IO.MemoryStream GetCustomData()
        {
            throw new NotImplementedException();
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
