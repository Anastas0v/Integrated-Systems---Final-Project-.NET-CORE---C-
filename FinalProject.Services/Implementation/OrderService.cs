﻿using FinalProject.Domain.DomainModels;
using FinalProject.Repository.Interface;
using FinalProject.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalProject.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this._orderRepository = orderRepository;
        }
        public List<Order> GetAllOrders()
        {
            return this._orderRepository.GetAllOrders();
        }

        public Order GetOrderDetails(BaseEntity model)
        {
            return this._orderRepository.GetOrderDetails(model);
        }
    }
}
