#region using
using System.Diagnostics;
#endregion

namespace MultiCouche
{
    [DebuggerDisplay("{Espece}")]
    sealed class Iris
    {
        public string Espece { get; set; }
        public double SepaleLongueur { get; set; }
        public double SepaleLargeur { get; set; }
        public double PetaleLongueur { get; set; }
        public double PetaleLargeur { get; set; }
    }
}
