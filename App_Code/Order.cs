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

/// <summary>
/// Summary description for Order
/// </summary>
public class Order
{
    public int ProductID { get; set; }
    public int Quantity { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime DateLastUpdated { get; set; }
    public string OrderStatus { get; set; }
    public string Ship_FirstName { get; set; }
    public string Ship_LastName { get; set; }
    public string Ship_AddressLine1 { get; set; }
    public string Ship_AddressLine2 { get; set; }
    public string Ship_phone { get; set; }
    public string Ship_postalcode { get; set; }
    public double Price { get; set; }
        
	public Order()
	{
		
	}
}