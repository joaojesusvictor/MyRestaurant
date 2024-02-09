using Microsoft.AspNetCore.Mvc;
using MyRestaurant.Api.Persistence;

namespace MyRestaurant.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OrderController : ControllerBase
    {
        private readonly OrderDbContext _context;

        public OrderController(OrderDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all Orders from a specific Area
        /// </summary>
        /// <param name="area">Area of kitchen to see Orders</param>
        /// <returns>String with Orders</returns>
        /// <response code="200">Success</response>
        /// <response code="400">BadRequest</response>
        [HttpGet("{area}")]
        public IActionResult Get(string area)
        {
            //Valid areas
            string[] areas = ["fries", "grill", "salad", "drink", "desert"];

            //Checking if the informed area is valid
            if (areas.Contains(area.ToLower()))
            {
                string values = string.Empty;

                //Taking orders from the queue
                if (area.ToLower() == "fries")
                    values = PrintOrders(_context.Fries);
                else if (area.ToLower() == "grill")
                    values = PrintOrders(_context.Grill);
                else if (area.ToLower() == "salad")
                    values = PrintOrders(_context.Salad);
                else if (area.ToLower() == "drink")
                    values = PrintOrders(_context.Drink);
                else
                    values = PrintOrders(_context.Desert);

                return Ok(values);
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// Add Order to Queue
        /// </summary>
        /// <param name="order">Order</param>
        /// <param name="area">Area of kitchen</param>
        /// <returns>Status Code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">BadRequest</response>
        [HttpPost("{order}, {area}")]
        public IActionResult AddOrder(string order, string area)
        {
            //Valid areas
            string[] areas = ["fries", "grill", "salad", "drink", "desert"];

            //Checking if the informed area is valid
            if (areas.Contains(area.ToLower()))
            {
                if (string.IsNullOrEmpty(order))
                    return BadRequest();

                //Adding the order to the queue
                if (area.ToLower() == "fries")
                    _context.Fries.Enqueue(order);
                else if (area.ToLower() == "grill")
                    _context.Grill.Enqueue(order);
                else if (area.ToLower() == "salad")
                    _context.Salad.Enqueue(order);
                else if (area.ToLower() == "drink")
                    _context.Drink.Enqueue(order);
                else
                    _context.Desert.Enqueue(order);

                return Ok();
            }
            else
                return BadRequest();
        }

        /// <summary>
        /// Delete a Order from Queue
        /// </summary>
        /// <param name="area">Kitchen area from where the Order should be deleted</param>
        /// <returns>Status Code</returns>
        /// <response code="200">Success</response>
        /// <response code="400">BadRequest</response>
        [HttpDelete("{area}")]
        public IActionResult DeleteOrder(string area)
        {
            //Valid areas
            string[] areas = ["fries", "grill", "salad", "drink", "desert"];

            //Checking if the informed area is valid
            if (areas.Contains(area.ToLower()))
            {
                //Removing order from queue
                if (area.ToLower() == "fries")
                    _context.Fries.Dequeue();
                else if (area.ToLower() == "grill")
                    _context.Grill.Dequeue();
                else if (area.ToLower() == "salad")
                    _context.Salad.Dequeue();
                else if (area.ToLower() == "drink")
                    _context.Drink.Dequeue();
                else
                    _context.Desert.Dequeue();

                return Ok();
            }
            else
                return BadRequest();
        }

        //Function to look all queue and print them
        public static string PrintOrders(Queue<string> myCollection)
        {
            string values = "";

            foreach (string obj in myCollection)
                values += obj + ", ";

            return values.Substring(0, values.Length - 2);
        }
    }
}
