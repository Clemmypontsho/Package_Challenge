using System;

namespace com.mobiquity.packer.Models
{
    // Represents an item in the package.
   
    public class PackageItem
    {
        public int Index { get; private set; }
        public float Weight { get; private set; }
        public int Cost { get; private set; }

        // Initializes a new instance of the <see cref="PackageItem"/> class.
        
        // <param name="index">The index of the item.</param>
        // <param name="weight">The weight of the item.</param>
        // <param name="cost">The cost of the item.</param>
        // <exception cref="APIException">Thrown when the weight or cost exceeds 100.</exception>
        public PackageItem(int index, float weight, int cost)
        {
            if (weight > 100)
            {
                throw new APIException("Error: The weight cannot be greater than 100.");
            }

            if (cost > 100)
            {
                throw new APIException("Error: The cost cannot be greater than 100.");
            }

            Index = index;
            Weight = weight;
            Cost = cost;
        }
    }
}