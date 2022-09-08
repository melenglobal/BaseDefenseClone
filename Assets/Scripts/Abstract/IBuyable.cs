using System.CodeDom;
using Enums;

namespace Abstract
{
    public interface IBuyable
    {
        public int Cost { get; set; }

        public int PayedAmount { get; set; }
        
    }
}