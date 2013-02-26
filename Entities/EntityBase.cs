using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using SFML.Graphics;
using SFML.Window;


namespace OpenEmpires.Entities
{
    public abstract class EntityBase : Drawable
    {
        public enum CommandTypes : byte
        {
            Move,
            AttackMove, // <- FINALLY AN ATTACK COMMAND IN AOE
            Build,
            Custom, // <- This depends on the unit, for example, tretchbuckets have the pack command that only applies to that entity
        }

        public Vector2f Position;
        public byte Health;

        protected EntityBase entityToUse;

        public enum EntityIds : byte
        {
            DEFAULT_NOTSET = 0,
            Resource = 1,
            Unit = 2,
        }

        public EntityIds EntityId
        {
            get;
            protected set;
        }
        public short WorldId;


        public EntityBase()
        {
            EntityId = EntityIds.DEFAULT_NOTSET;
            WorldId = -1;
            Health = 0;
            Position.X = 0;
            Position.Y = 0;
        }

        public abstract void Update();

        public MemoryStream ToStream()
        {
            var stream = new MemoryStream();
            var writer = new BinaryWriter(stream);

            writer.Write((byte)EntityId);
            writer.Write((short)WorldId);
            writer.Write((bool)(entityToUse != null));

            if(entityToUse != null)
                writer.Write((short)entityToUse.WorldId);

            writer.Write(Position.X);
            writer.Write(Position.Y);
            writer.Write(Health);
            
            writer.Write(GetCustomData().ToArray());
            return stream;
        }

        public void ParseStream(MemoryStream memory)
        {
            var reader = new BinaryReader(memory);
            EntityId = (EntityIds)reader.ReadByte();
            WorldId = reader.ReadInt16();

            entityToUse = null;
            if (reader.ReadBoolean()) //is this entity using another entity?
            {
                entityToUse = Game.EntManager.EntityContainer[reader.ReadInt16()];
            }

            Position = new Vector2f(reader.ReadSingle(), reader.ReadSingle());
            Health = reader.ReadByte();

            ParseCustomStream(reader);
        }

        public void Draw(RenderTarget rt, RenderStates states)
        {
            //drawing is handled by inherrited entities
        }

        //Data sent over the network to give information about entity
        protected abstract MemoryStream GetCustomData();

        //Fucction to parse the data sent from GetMemoryData
        public abstract void ParseCustomStream(BinaryReader reader);

        //tagData is optional data that can apply to certain entities, liek the tretchbuckets
        public delegate void CallCommandDelegate(CommandTypes type, float xpos, float ypos, byte[] tagData);
        public event CallCommandDelegate OnCallCommand;

        public delegate void AttackDelegate(EntityBase entity);
        public event AttackDelegate OnAttack;

        public delegate void TakeDamageDelegate(float damage, EntityBase inflicter);
        public event TakeDamageDelegate OnTakeDamage;
    }
}
