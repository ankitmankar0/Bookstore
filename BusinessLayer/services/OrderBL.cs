using BusinessLayer.Interface;
using CommonLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.services
{
    public class OrderBL : IOrderBL
    {
        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }

        public OrderModel AddOrder(OrderModel orderModel, int userId)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public List<OrderResponse> GetAllOrders(int userId)
        {
            try
            {
                return this.orderRL.GetAllOrders(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
