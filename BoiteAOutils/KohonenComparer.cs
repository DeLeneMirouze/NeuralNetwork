#region using
using System.Collections.Generic; 
#endregion

namespace BoiteAOutils
{
    public class KohonenComparer : IEqualityComparer<KohonenNeurone>
    {
        public bool Equals(KohonenNeurone x, KohonenNeurone y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x.Couleur == y.Couleur;
        }

        public int GetHashCode(KohonenNeurone obj)
        {
            return obj.GetHashCode();
        }
    }
}
