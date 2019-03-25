using AplikacjaRestAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AplikacjaRestAPI.Data
{
    public class ProductValidator
    {
        private static readonly List<string> _allowedCategory = new List<string>()
        {
            "Category1",
            "Category2",
            "Category3",
            "Category4",
            "Category5",
        };

        public static bool ValidateProduct(Products products)
        {
            if (!ValidateCategory(products.Category)) return false;
            if (!ValidateId(products.Id)) return false;
            if (!ValidateCost(products.Cost)) return false;
            if (!ValidateName(products.Name)) return false;

            return true;
        }

        private static bool ValidateCategory(string category)
        {
            return _allowedCategory.Any(x => x == category);
        }

        private static bool ValidateId(int Id)
        {
            return Id >= 0;
        }

        private static bool ValidateCost(decimal? cost)
        {
            return cost == null || cost >= 0;
        }

        private static bool ValidateName(string name)
        {
            if (Regex.IsMatch(name, @"^\d+"))
               return false;

            return true;
        }
    }
}
