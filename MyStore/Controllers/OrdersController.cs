using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyStore.Domain.Entities;
using MyStore.Infrastructure;
using MyStore.Models;
using MyStore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase

    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }
        // GET: api/<OrdersControlleer>
        [HttpGet]

        public ActionResult<IEnumerable<OrderModel>> Get(string? city, [FromQuery] List<string>? country,Shippers shippers)
        {
            //1.shipcity ->free value from a parameter
            //2/predefine value for 
      
            var allOrders = orderService.GetAll(city,country,shippers);
            var mappedOrders = mapper.Map<List<OrderModel>>(allOrders);

            return Ok(mappedOrders);
        }
        //[HttpGet]
        //[Route("GetOrdersByCountry/{country}")]
        //public IEnumerable<Order> Get(string country)
        //{
        //    var orderList = orderService.GetAll(country);
        //    return orderList;
        //}


        // GET api/<OrdersControlleer>/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var result = orderService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

 
        [HttpPost]
        
        public IActionResult Post([FromBody] OrderModel model)
        {

            var order = mapper.Map<Order>(model);

            var addedOrder = orderService.AddNewOrder(order);

            //do reverse mapping Order ->OrderModel
            return CreatedAtAction("Get", new { id = addedOrder.Orderid },mapper.Map<OrderModel>(addedOrder));
        }

        // PUT api/<OrdersControlleer>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] OrderModel orderToUpdate)
        {
            if (id != orderToUpdate.Orderid)
            {
                return BadRequest();
            }
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            orderService.UpdateOrder(orderToUpdate);
            return NoContent();
        }

        // DELETE api/<OrdersControlleer>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (!orderService.Exists(id))
            {
                return NotFound();
            }
            var isDeleted = orderService.Delete(id);
            return NoContent();
        }
    }
}
