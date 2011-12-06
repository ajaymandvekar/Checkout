/*
 * Copyright 2011 
 * Ajay Mandvekar(ajaymandvekar@gmail.com),Mugdha Kolhatkar(himugdha@gmail.com),Vishakha Channapattan(vishakha.vc@gmail.com)
 * 
 * This file is part of Checkout.
 * Checkout is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 * 
 * Checkout is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with Checkout.  If not, see <http://www.gnu.org/licenses/>.
 *
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;


[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public Service () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public double Compute_Total_Price(int OrgID, int ProductID, int Quantity)
    {
        double total = 0.0;
        int BookObjID = 0;

        localhost.Service serviceObj = new localhost.Service();
        localhost.Table[] Tarray = serviceObj.GetTables(OrgID);
        List<localhost.Table> listOfTables = new List<localhost.Table>(Tarray);
        for (int i = 0; i < listOfTables.Count; i++)
        {
            if (listOfTables[i].TNameProperty == "Book")
            {
                BookObjID = listOfTables[i].ObjIDProperty;
            }
        }

        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(OrgID, BookObjID, ProductID);
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;

        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "Cost")
            {
                 double cost = Convert.ToInt32(valuearr[counter].ToString());
                 total = cost * Quantity;
                 return total;
            }
            counter++;
        }

        return total;
    }

    [WebMethod]
    public double apply_discount(int OrgID, double price, int CustomerID)
    {
        double discount = 0.0;
        int CustomerCategoryObjID = 0;
        int CustomerObjID = 0;

        localhost.Service serviceObj = new localhost.Service();
        localhost.Table[] Tarray = serviceObj.GetTables(OrgID);
        List<localhost.Table> listOfTables = new List<localhost.Table>(Tarray);
        for (int i = 0; i < listOfTables.Count; i++)
        {
            if (listOfTables[i].TNameProperty == "CustomerCategory")
            {
                CustomerCategoryObjID = listOfTables[i].ObjIDProperty;
            }
            if (listOfTables[i].TNameProperty == "Customer")
            {
                CustomerObjID = listOfTables[i].ObjIDProperty;
            }
        }

        localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(OrgID, CustomerObjID, CustomerID);
        string[] arr = obj.FieldNamesProperty;
        List<string> array = new List<string>(arr);
        string[] values = obj.FieldValuesProperty;
        List<string> valuearr = new List<string>(values);
        int counter = 0;
        int categoryid = 0;
        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "CustCategoryID")
            {
                categoryid = Convert.ToInt32(valuearr[counter].ToString());
            }
            counter++;
        }

        double total = 0;

        obj = serviceObj.ReadDataWithGUID(OrgID, CustomerCategoryObjID, CustomerID);
        arr = obj.FieldNamesProperty;
        array = new List<string>(arr);
        values = obj.FieldValuesProperty;
        valuearr = new List<string>(values);
        counter = 0;
        for (int j = 0; j < array.Count; j++)
        {
            if (arr[j].ToString() == "DiscountPercentage")
            {
                discount = Convert.ToDouble(valuearr[counter].ToString());
                total = price - (price * discount) / 100;
            }
            counter++;
        }

        return total;
    }

    [WebMethod]
    public double calculatetax(double price, double tax)
    {
        double total = price + tax;
        return total;
    }

    [WebMethod]
    public int bill_to_card_displayConfirmation(int OrgID,int CustomerID,int ProductID,int Quantity,double price)
    {
        try
        {
            bool success = false;
            Order order_obj = new Order();
            int CustomerObjID = 0;
            int OrderObjID = 0;

            localhost.Service serviceObj = new localhost.Service();
            localhost.Table[] Tarray = serviceObj.GetTables(OrgID);
            List<localhost.Table> listOfTables = new List<localhost.Table>(Tarray);
            for (int i = 0; i < listOfTables.Count; i++)
            {
                if (listOfTables[i].TNameProperty == "Customer")
                {
                    CustomerObjID = listOfTables[i].ObjIDProperty;
                }
                if (listOfTables[i].TNameProperty == "Order")
                {
                    OrderObjID = listOfTables[i].ObjIDProperty;
                }
            }

            if (CustomerObjID != 0)
            {
                localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(OrgID, CustomerObjID, CustomerID);
                string[] arr = obj.FieldNamesProperty;
                List<string> array = new List<string>(arr);
                string[] values = obj.FieldValuesProperty;
                List<string> valuearr = new List<string>(values);
                int counter = 0;

                for (int j = 0; j < array.Count; j++)
                {
                    if (arr[j].ToString() == "Ship_FirstName")
                    {
                        order_obj.Ship_FirstName = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "Ship_LastName")
                    {
                        order_obj.Ship_LastName = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "Ship_phone")
                    {
                        order_obj.Ship_phone = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "Ship_postalcode")
                    {
                        order_obj.Ship_postalcode = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "Ship_AddressLine1")
                    {
                        order_obj.Ship_AddressLine1 = valuearr[counter].ToString();
                    }
                    if (arr[j].ToString() == "Ship_AddressLine2")
                    {
                        order_obj.Ship_AddressLine2 = valuearr[counter].ToString();
                    }
                    counter++;
                }

                order_obj.ProductID = ProductID;
                order_obj.Quantity = Quantity;
                order_obj.OrderStatus = "Billed";
                order_obj.OrderDate = DateTime.Now;
                order_obj.DateLastUpdated = DateTime.Now;

                List<String> fieldnames = new List<String>();
                fieldnames.Add("0");
                fieldnames.Add("1");
                fieldnames.Add("2");
                fieldnames.Add("3");
                fieldnames.Add("4");
                fieldnames.Add("5");
                fieldnames.Add("6");
                fieldnames.Add("7");
                fieldnames.Add("8");
                fieldnames.Add("9");
                fieldnames.Add("10");
                fieldnames.Add("11");
                fieldnames.Add("12");

                List<String> valueNames = new List<String>();
                valueNames.Add(order_obj.ProductID.ToString());
                valueNames.Add(order_obj.Quantity.ToString());
                valueNames.Add(order_obj.OrderDate.ToString());
                valueNames.Add(order_obj.DateLastUpdated.ToString());
                valueNames.Add(order_obj.OrderStatus.ToString());
                valueNames.Add(order_obj.Ship_FirstName.ToString());
                valueNames.Add(order_obj.Ship_LastName.ToString());
                valueNames.Add(order_obj.Ship_AddressLine1.ToString());
                valueNames.Add(order_obj.Ship_AddressLine2.ToString());
                valueNames.Add(order_obj.Ship_phone.ToString());
                valueNames.Add(order_obj.Ship_postalcode.ToString());
                valueNames.Add(order_obj.Price.ToString());
                valueNames.Add(CustomerID.ToString());

                int custorderID = 0;
                success = serviceObj.InsertData(OrgID, OrderObjID, "Order-Instance-" + OrgID.ToString(), fieldnames.ToArray(), valueNames.ToArray());
                if (success)
                {
                    obj = serviceObj.ReadData(OrgID, OrderObjID);
                    arr = obj.FieldNamesProperty;
                    array = new List<string>(arr);
                    values = obj.FieldValuesProperty;
                    valuearr = new List<string>(values);
                    int countRow = valuearr.Count / array.Count;
                    counter = 0;
                    for (int i = 0; i < countRow; i++)
                    {
                        for (int j = 0; j < array.Count; j++)
                        {
                            if (arr[j].ToString() == "GUID")
                            {
                                custorderID = Convert.ToInt32(valuearr[counter].ToString());
                            }
                            counter++;
                        }
                    }
                    return custorderID;
                }
                else
                {
                    return custorderID;
                }
            }
            else
            {
                return 0;
            }
        }
        catch (Exception ex)
        {
            throw;
        }      
    }

    [WebMethod]
    public Order Displayinfo(int OrgID, int OrderID)
    {
        Order order_obj = null;
        int ObjID = 0;
        localhost.Service serviceObj = new localhost.Service();
        localhost.Table[] Tarray = serviceObj.GetTables(OrgID);
        List<localhost.Table> listOfTables = new List<localhost.Table>(Tarray);
        for (int i = 0; i < listOfTables.Count; i++)
        {
            if (listOfTables[i].TNameProperty == "Order")
            {
                ObjID = listOfTables[i].ObjIDProperty;
            }
        }

        if (ObjID != 0)
        {
            localhost.TenantTableInfo obj = serviceObj.ReadDataWithGUID(OrgID, ObjID, OrderID);
            string[] arr = obj.FieldNamesProperty;
            List<string> array = new List<string>(arr);
            string[] values = obj.FieldValuesProperty;
            List<string> valuearr = new List<string>(values);
            int counter = 0;
            order_obj = new Order();

            for (int j = 0; j < array.Count; j++)
            {
                if (arr[j].ToString() == "OrderDate")
                {
                    order_obj.OrderDate = Convert.ToDateTime(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "DateLastUpdated")
                {
                    order_obj.DateLastUpdated = Convert.ToDateTime(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "Quantity")
                {
                    order_obj.Quantity = Convert.ToInt32(valuearr[counter].ToString());
                }
                if (arr[j].ToString() == "OrderStatus")
                {
                    order_obj.OrderStatus = valuearr[counter].ToString();
                }
                if (arr[j].ToString() == "ProductID")
                {
                    order_obj.ProductID = Convert.ToInt32(valuearr[counter].ToString());
                }
                counter++;
            }
        }

        return order_obj;
    }
}