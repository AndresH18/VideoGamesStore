﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GameStore.Data.Models;

public class Order
{
    // TODO: Create Order model, See if this is the correct way to store in database.
    [BindNever] public int Id { get; set; }

    [Required(ErrorMessage = "Please enter a Name")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Please enter an address")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Please enter a Country")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "Please enter a City")]
    public string? City { get; set; }

    [BindNever] public double TotalValue { get; set; }

    [BindNever] public int TotalItems { get; set; }

    [BindNever] public DateTime CreationDate { get; set; }

    [BindNever] public List<OrderItem> OrderItems { get; set; } = new();

    [BindNever] public bool Shipped { get; set; } = false;
}