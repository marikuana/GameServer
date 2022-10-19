﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameServerCore
{
    public abstract class GameObject
    {
        public Guid Id { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 Rotation { get; set; }
    }
}