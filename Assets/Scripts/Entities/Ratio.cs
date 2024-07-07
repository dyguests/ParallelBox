using UnityEngine;

namespace Entities
{
    public struct Ratio
    {
        public int molecule;
        public int letter;
        public bool isStatic;

        public float Value => ((float)molecule) / letter;

        public Ratio(int molecule, int letter, bool isStatic)
        {
            this.molecule = molecule;
            this.letter = letter;
            this.isStatic = isStatic;
        }

        public Ratio Split(int count)
        {
            if (isStatic) return this;
            Debug.Assert(count > 0);
            if (molecule % count == 0)
            {
                return new Ratio(molecule / count, letter, isStatic);
            }

            return new Ratio(molecule, letter * count, isStatic);
        }
    }
}