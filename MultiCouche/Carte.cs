#region using
using System.Diagnostics; 
#endregion

namespace MultiCouche
{
    [DebuggerDisplay("{Cible}: ({X},{Y})")]
    sealed class Carte
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Cible { get; set; }
    }
}
