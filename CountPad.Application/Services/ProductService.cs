﻿// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by CountPad Team
// --------------------------------------------------------

using System;
using System.Threading.Tasks;
using CountPad.Application.Interfaces.RepositoryInterfaces;
using CountPad.Application.Interfaces.ServiceInterfaces;
using CountPad.Domain.Models.Products;

namespace CountPad.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository productRepository) =>
            this.productRepository = productRepository;

        public Task<int> AddProductAsync(Product product)
        {
            return this.productRepository.AddAsync(product);
        }

        public Task<int> DeleteProductAsync(Guid id)
        {
            return this.productRepository.DeleteAsync(id);
        }

        public Task<int> DeleteProductAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}

