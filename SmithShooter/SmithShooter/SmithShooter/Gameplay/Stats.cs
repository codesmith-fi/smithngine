using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Codesmith.SmithShooter.Gameplay
{
    [Serializable()]
    public class Stats
    {
        public int ShotsFired { get; set; }
        public int ShotsHitOnTarget { get; set; }

        public Stats()
        { 
        }
    }
}
