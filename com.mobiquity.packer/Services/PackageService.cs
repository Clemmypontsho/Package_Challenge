using com.mobiquity.packer.Models;
using com.mobiquity.packer.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.mobiquity.packer.Services
{

    // Implements logic for mapping to package models.

    public class PackageService : IPackageService
    {
        public List<Package> CreatePackageList(string[] lines)
        {
            // Initialize a list to hold the package models
            var packageList = new List<Package>();

            foreach (var line in lines)
            {
                // Create a package model from the line and add it to the list
                var package = CreatePackage(line);
                packageList.Add(package);
            }

            return packageList;
        }

        public Package CreatePackage(string itemLine)
        {
            // Initialize a new package model
            var package = new Package();

            // Split the line into weight and items
            var weightItemArray = itemLine.Split(Constants.PackageMaxWeightSeperator, StringSplitOptions.None);

            // Validate the package line
            if (weightItemArray.Length != 2)
                throw new APIException("Error: Invalid package record!");

            // Parse and assign the package weight limit
            if (!int.TryParse(weightItemArray[0].Trim(), out var packageWeightLimit))
                throw new APIException("Error: Package weight limit is corrupted!");

            // Validate the package weight limit constraint
            if (packageWeightLimit <= 0 || packageWeightLimit >= 100)
                throw new APIException("Error: Invalid weight limit for the package!");

            package.WeightLimit = packageWeightLimit;

            // Split the items string into individual item strings
            var itemsStringArr = weightItemArray[1].Trim().Split(Constants.PackageItemSeperator, StringSplitOptions.None);

            // Validate the maximum item number constraint
            if (itemsStringArr.Length > 15)
                throw new APIException("Error: The maximum number of items to choose from cannot exceed 15. Error on item record with maxWeight: " + package.WeightLimit);

            // Create package item models from the item strings and assign them to the package
            package.PackageItems = itemsStringArr.Select(x => CreatePackageItem(x[1..^1])).ToArray();

            return package;
        }

        public PackageItem CreatePackageItem(string items)
        {
            // Split the item string into index, weight, and cost
            var itemArray = items.Split(Constants.ItemPropertySeperator, StringSplitOptions.None);

            // Validate the item record
            if (itemArray.Length != 3)
                throw new APIException("Error: Package item record is corrupted!");

            // Parse and assign the index, weight, and cost of the package item
            if (!int.TryParse(itemArray[0], out var index))
                throw new APIException("Error: Package item index is invalid!");

            if (!float.TryParse(itemArray[1], out var weight))
                throw new APIException("Error: Package item weight is invalid!");

            if (!itemArray[2].StartsWith(Constants.EuroPrefix))
                throw new APIException("Error: Package item cost is invalid!");

            if (!int.TryParse(itemArray[2].Remove(0, 1), out var cost))
                throw new APIException("Error: Package item cost is invalid!");

            return new PackageItem(index, weight, cost);
        }
    }
}