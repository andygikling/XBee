﻿using BinarySerialization;

namespace XBee.Frames.AtCommands
{
    internal class PanIdCommandExt : AtCommand
    {
        public PanIdCommandExt() : base(PanIdCommand.Name)
        {
        }

        public PanIdCommandExt(ulong id) : this()
        {
            Parameter = id;
        }

        [Ignore]
        public ulong Id => (ulong?)Parameter ?? default(ulong);
    }
}
