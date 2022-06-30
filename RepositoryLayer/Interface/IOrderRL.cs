using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        public OrderModel AddOrder(OrderModel orderModel, int userId);
        public List<OrderResponse> GetAllOrders(int userId);
    }
}
