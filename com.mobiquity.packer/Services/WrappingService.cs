using com.mobiquity.packer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{
    public class WrappingService : IWrappingService
    {
        public List<Package> Wrap(List<Package> packageList)
        {
            return packageList.Select(x => OptimizePackage(x)).ToList();
        }

        private static Package OptimizePackage(Package package)
        {
            // Order package items by weight in ascending order
            package.PackageItems = package.PackageItems.OrderBy(x => x.Weight).ToArray();

            int optimalCost = 0;
            Package optimizedPackage = new()
            {
                WeightLimit = package.WeightLimit
            };

            int[] itemWeights = { 0 };
            itemWeights = itemWeights.Concat(package.PackageItems.Select(x => (int)x.Weight)).ToArray();
            int[] itemCosts = { 0 };
            itemCosts = itemCosts.Concat(package.PackageItems.Select(x => x.Cost)).ToArray();

            int[,] data = new int[package.PackageItems.Length + 1, package.WeightLimit + 1];

            int itemCount = package.PackageItems.Length;
            int maxWeight = package.WeightLimit;

            // Dynamic programming approach to find optimal cost
            for (int itemNum = 0; itemNum <= itemCount; itemNum++)
            {
                for (int weight = 0; weight <= maxWeight; weight++)
                {
                    if (itemNum == 0 || weight == 0)
                    {
                        // Base case: no items or weight limit reached
                        data[itemNum, weight] = 0;
                    }
                    else if (itemWeights[itemNum] <= weight)
                    {
                        // Include the item if its weight is less than or equal to the remaining weight
                        data[itemNum, weight] = Math.Max(itemCosts[itemNum] + data[itemNum - 1, weight - itemWeights[itemNum]], data[itemNum - 1, weight]);
                    }
                    else
                    {
                        // Exclude the item as its weight exceeds the remaining weight
                        data[itemNum, weight] = data[itemNum - 1, weight];
                    }
                }
            }

            // The optimal cost of the package
            optimalCost = data[itemCount, maxWeight];

            // Determine the included package items based on the optimal cost
            optimizedPackage.PackageItems = GetIncludedPackageItems(itemCount, maxWeight, itemWeights, itemCosts, package, data).OrderBy(x => x.Index).ToArray();

            return optimizedPackage;
        }

        private static List<PackageItem> GetIncludedPackageItems(int itemCount, int maxWeight, int[] itemWeights, int[] itemCosts, Package packageToOptimize, int[,] data)
        {
            List<PackageItem> result = new();

            int i = itemCount;
            int j = maxWeight;

            // Trace back to find the included items based on the optimal cost
            while (i > 0 && j > 0)
            {
                if (data[i, j] != data[i - 1, j])
                {
                    // Include the item in the result
                    result.Add(packageToOptimize.PackageItems[i - 1]);
                    j -= itemWeights[i];
                }
                i--;
            }

            return result;
        }
    }
}