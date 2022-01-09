using System;
using System.Linq;
using System.Collections.Generic;

namespace CheckoutTestTask
{
// Put your solution in the 'checkout' function below.  Feel free to create
// additional functions, objects, pull in outside libraries, etc.

public class Item
{
  public Item(string _name, int _count)
  {
      name = _name;
      count = _count;
  }

  public string name {
    get; set;
  }

  public int count {
    get; set;
  }
}

public class Result
{
    public double subtotal { get; set; }
    public double discount { get; set; }
    public double tax { get; set; }
    public double total { get; set; }

    public override string ToString() {
        return String.Format($"Result: subtotal = {subtotal} discount = {discount} tax = {tax} total = {total}");
    }
}

public static class Maps
{
    public static Dictionary<String, Double> PriceMap = new Dictionary<string, double>
    {
        { "apple", 0.5 },
        { "orange", 0.4 },
        { "banana", 0.2 },
        { "strawberry", 2.0 }
    };

    public static Dictionary<String, Double> TaxByLocationMap = new Dictionary<string, double>
    {
        { "MO", 1.225 },
        { "OR", 0.0 },
        { "GA", 4.0 },
        { "All Others", 2.0 }
    };
}

// To execute C#, please define "static void Main" on a class
// named Solution.

class Solution
{
    static void Main(string[] args)
    {

        List<Item> items = new List<Item>();
        items.Add(new Item("apple",6));
        items.Add(new Item("orange", 3));
        items.Add(new Item("banana", 4));

        //-------------------------------------------------------
        // Part 1 - Basic shopping cart
        //-------------------------------------------------------

        Console.WriteLine(checkout(items));

        // Expected Result
        // {
        //   subtotal: 5,
        //   discount: 0
        //   tax: 0
        //   total: 5
        // }



        //-------------------------------------------------------
        // Part 2 - Tax by location
        //-------------------------------------------------------
        Console.WriteLine(checkout(items,"MO"));

        // Expected Result
        // {
        //   subtotal: 5,
        //   discount: 0
        //   tax: 0.06     // rounded down from 0.0613 to 0.06
        //   total: 5.06
        // }


        //-------------------------------------------------------
        // Part 3 - discount code - 10% off discount code
        //-------------------------------------------------------
        Console.WriteLine(checkout(items,"MO", "tenpercentoff"));

        // Expected Result
        // {
        //   subtotal: 5,
        //   discount: 0.5
        //   tax: 0.06     // rounded up from 0.0551 to 0.06
        //   total: 4.56
        // }

        //-------------------------------------------------------
        // Part 4 - discount code - 2 dollars off 5 dollar or more purchase
        //-------------------------------------------------------
        Console.WriteLine(checkout(items,"MO","2dollarsoff"));

        // Expected Result
        // {
        //   subtotal: 5,
        //   discount: 2
        //   tax: 0.04
        //   total: 3.04
        // }


        //-------------------------------------------------------
        // Part 5 - discount code - buy one, get one free
        //-------------------------------------------------------
        Console.WriteLine(checkout(items,"MO","buyonegetonefree"));


        // Expected Result
        // {
        //   subtotal: 5,
        //   discount: 2.3
        //   tax: 0.03    // rounded down from 0.0331 to 0.03
        //   total: 2.73
        // }
    }

    //-------------------------------------------------------
    // Your Code Here
    //-------------------------------------------------------

    static Double GetTaxRateByLocation(String taxCode) =>
        Maps.TaxByLocationMap.ContainsKey(taxCode)
            ? Maps.TaxByLocationMap[taxCode]
            : 0.0;

    static Result GetWithoutDiscount(Result result, String taxCode)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "Argument cannot be null");
        }

        var taxRate = GetTaxRateByLocation(taxCode);

        result.tax = (taxRate * result.subtotal) / 100;
        result.total = result.subtotal + taxRate;

        return result;
    }

    static Result GetTenpercentOff(Result result, String taxCode)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "Argument cannot be null");
        }

        result.discount = 0.5;

        var discountedSubtotal = result.subtotal - result.discount;

        var taxRate = GetTaxRateByLocation(taxCode);

        result.tax = (taxRate * discountedSubtotal) / 100;
        result.total = discountedSubtotal + result.tax;

        return result;
    }

    static Result Get2dollarsOff(Result result, String taxCode)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "Argument cannot be null");
        }

        result.discount = result.subtotal >= 5.0
            ? 2.0
            : 0.0;

        var discountedSubtotal = result.subtotal - result.discount;

        var taxRate = GetTaxRateByLocation(taxCode);

        result.tax = (taxRate * discountedSubtotal) / 100;
        result.total = discountedSubtotal + result.tax;

        return result;
    }

    static Result GetBuyOneGetOneFree(Result result, List<Item> items, String taxCode)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "Argument cannot be null");
        }

        if (items is null)
        {
            throw new ArgumentNullException(nameof(items), "Argument cannot be null");
        }

        foreach (var item in items.Where(i => !String.IsNullOrWhiteSpace(i.name)))
        {
            if (Maps.PriceMap.TryGetValue(item.name.ToLower(), out var price))
            {
                result.discount += (item.count / 2) * price;
            }
        }

        var discountedSubtotal = result.subtotal - result.discount;

        var taxRate = GetTaxRateByLocation(taxCode);

        result.tax = (taxRate * discountedSubtotal) / 100;
        result.total = discountedSubtotal + result.tax;

        return result;
    }

    static Result CalculateSubtotal(Result result, List<Item> items)
    {
        if (result is null)
        {
            throw new ArgumentNullException(nameof(result), "Argument cannot be null");
        }

        if (items is null)
        {
            throw new ArgumentNullException(nameof(items), "Argument cannot be null");
        }

        foreach (var item in items.Where(i => !String.IsNullOrWhiteSpace(i.name)))
        {
            if (Maps.PriceMap.TryGetValue(item.name.ToLower(), out var price))
            {
                result.subtotal += item.count * price;
            }
        }

        return result;
    }

    static Result checkout(List<Item> items, string taxCode = "", string discount = "")
    {
        Result result = new Result();

        result = CalculateSubtotal(result, items);

        return discount switch
        {
            null or "" => GetWithoutDiscount(result, taxCode),
            "tenpercentoff" => GetTenpercentOff(result, taxCode),
            "2dollarsoff" => Get2dollarsOff(result, taxCode),
            "buyonegetonefree" => GetBuyOneGetOneFree(result, items, taxCode),
            _ => throw new ArgumentException($"Unknown discount type {discount}", nameof(discount))
        };
    }
}
}
