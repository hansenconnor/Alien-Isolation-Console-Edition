﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAionProject
{
    public abstract class GameObject
    {
        public abstract int Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract int RoomId { get; set; }
        public abstract int[] Coords { get; set; }
        public abstract string Icon { get; set; }
    }
}
