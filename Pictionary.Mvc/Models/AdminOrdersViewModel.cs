namespace Pictionary.Mvc.Models;

using Pictionary.Domain.OrderModel;

public class AdminOrdersViewModel
{
    public AdminOrdersViewModel()
    {
    }

    public AdminOrdersViewModel(List<Order> orders)
    {
        Orders = orders;
    }

    public AdminOrdersViewModel(List<Order> orders, int page)
    {
        Orders = orders;
        Page = page;
    }

    public List<Order> Orders { get; set; } = new();

    public int Page { get; set; }
}