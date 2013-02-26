using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace OpenEmpires.Entities
{
    class Resource : EntityBase
    {
        public ushort Amount; //Resources drain in amount each time gathered
        
        public Resource()
        {
            EntityId = EntityIds.Resource;
            Amount = 100;
        }

        public override void Update()
        {
            //resources don't really do anything in real time
        }

        protected override MemoryStream GetCustomData()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write(Amount);

            return stream;
        }

        public override void ParseCustomStream(BinaryReader reader)
        {
            Amount = reader.ReadUInt16();
        }
    }
}
