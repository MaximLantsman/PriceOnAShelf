using System;
using System.Collections.Generic;

//Product data to get from server
[Serializable]
public class ProdcutsPulledFromJSON
{
    public List<ProductData> products;
}

[Serializable]
public class ProductData
{
    public string name;
    public string description;
    public float price;
}