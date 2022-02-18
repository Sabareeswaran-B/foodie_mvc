using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using foodie_mvc.Models;
using System.Security.Claims;

namespace foodie_mvc.Controllers;
using System.Runtime.Serialization;

public class HomeController : Controller
{
    private readonly foodieContext _context;

    public HomeController(foodieContext context)
    {
        _context = context;
    }

    [DataContract]
    public class BarChartDataPoint
    {
        public BarChartDataPoint(string label, double y)
        {
            this.Label = label;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "label")]
        public string Label = "";

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }

    [DataContract]
    public class SplineChartDataPoint
    {
        public SplineChartDataPoint(double x, double y)
        {
            this.x = x;
            this.Y = y;
        }

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "x")]
        public Nullable<double> x = null;

        //Explicitly setting the name to be used while serializing to JSON.
        [DataMember(Name = "y")]
        public Nullable<double> Y = null;
    }

    public IActionResult Index()
    {
        List<BarChartDataPoint> barChart = new List<BarChartDataPoint>();
        List<SplineChartDataPoint> splineChart = new List<SplineChartDataPoint>();

        var users = (from u in _context.Users select u).Count();
        var adminUsers = (from au in _context.Users select au)
            .Where(s => s.UserRole == "admin")
            .Count();
        var store = (from s in _context.Stores select s).Count();
        var products = (from p in _context.Products select p).Count();
        var orders = (from p in _context.Orders select p).Count();

        var dailyOrders =
            from o in _context.Orders
            select new Order
            {
                OrderedAt = ((DateTime)o.OrderedAt!).Date,
                TotalPrice = o.TotalPrice,
                OrderId = o.OrderId,
                OrderStatus = o.OrderStatus,
            };
        var orderSum = dailyOrders
            // .Where(w => w.OrderStatus != "Cancelled")
            .GroupBy(g => g.OrderedAt)
            .Select(s => Tuple.Create(s.Key, s.Sum(a => a.TotalPrice)));

        barChart.Add(new BarChartDataPoint("User", users));
        barChart.Add(new BarChartDataPoint("Admin Users", adminUsers));
        barChart.Add(new BarChartDataPoint("Store", store));
        barChart.Add(new BarChartDataPoint("Product", products));
        barChart.Add(new BarChartDataPoint("Orders", orders));

        foreach (var item in orderSum)
        {
            var dt = (DateTime)item.Item1!;

            double epoch = (double)(dt.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;

            splineChart.Add(new SplineChartDataPoint(epoch, (double)item.Item2!));
        }

        ViewBag.BarChart = JsonConvert.SerializeObject(barChart);
        ViewBag.SplineChart = JsonConvert.SerializeObject(splineChart);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(
            new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }
        );
    }
}
