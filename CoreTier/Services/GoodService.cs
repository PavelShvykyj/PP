﻿using AutoMapper;
using CoreTier.Interfaces;
using DataTier.Models;
using DTO.APIResourses;
using DTO.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreTier.Services
{
    internal class GoodService : IGoodService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GoodService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<GoodDTO> CreateAsync(GoodResource resource)
        {
            Good newGood = _mapper.Map<GoodResource, Good>(resource);
            _unitOfWork.Goods.Create(newGood);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Good, GoodDTO>(newGood);
        }

        public GoodDTO Get(int id)
        {
            Good good = _unitOfWork.Goods.Get(id);
            if (good == null)
            {
                return null;
            }
            return _mapper.Map<Good, GoodDTO>(good);

        }

        public IEnumerable<GoodDTO> GetList(int take, int skip)
        {
            Good[] Goods = _unitOfWork.Goods.GetList(take, skip)
               .Skip(skip)
               .Take(take)
               .ToArray();

            GoodDTO[] GoodDTOs = _mapper.Map<Good[], GoodDTO[]>(Goods);
            return GoodDTOs;
        }

        public async Task<int> SetPriceAsync(ushort id, decimal price)
        {
            _unitOfWork.Goods.SetPrice(id, price);
            var count =  await _unitOfWork.SaveAsync();
            return count;
        }

        public async Task<int> SetPriceToManyAsync(List<GoodSetPriceResouce> resource)
        {
            List<Good> goods = _mapper.Map<List<GoodSetPriceResouce>, List<Good>>(resource);
            _unitOfWork.Goods.SetPriceToMany(goods);
            var count = await _unitOfWork.SaveAsync();
            return count;
        }

        public async Task<int> SetRestAsync(ushort id, ushort rest)
        {
            _unitOfWork.Goods.SetRest(id, rest);
            var count =  await _unitOfWork.SaveAsync();
            return count;
        }

        public async Task<int> SetRestToManyAsync(List<GoodSetRestResouce> resource)
        {
            
            List<Good> goods = _mapper.Map<List<GoodSetRestResouce>, List<Good>>(resource);
            _unitOfWork.Goods.SetRestToMany(goods);
            var count = await _unitOfWork.SaveAsync();
            return count;
        }

        public async Task<GoodDTO> UpdateAsync(int id, GoodResource resource)
        {
            var Good = _unitOfWork.Goods.Get(id);
            if (Good is null)
            {
                return null;
            }
            _mapper.Map<GoodResource, Good>(resource);
            _unitOfWork.Goods.Update(Good);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Good, GoodDTO>(Good);
        }

        public async Task<GoodDTO> PatchAsync(ushort id, JsonPatchDocument<Good> resource, ModelStateDictionary modelState)
        {
            var Good = _unitOfWork.Goods.Get(id);
            if (Good is null)
            {
                return null;
            }
            resource.ApplyTo(Good, modelState);
            if (!modelState.IsValid) 
            {
                return null;
            }
            _unitOfWork.Goods.Update(Good);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<Good, GoodDTO>(Good);
        }
    }
}
