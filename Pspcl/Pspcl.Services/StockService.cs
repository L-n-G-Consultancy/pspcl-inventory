﻿
using Microsoft.EntityFrameworkCore;
using Pspcl.Core.Domain;
using Pspcl.DBConnect;
using Pspcl.Services.Interfaces;
using Pspcl.Services.Models;

namespace Pspcl.Services
{

    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _dbcontext;
        public StockService(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public List<MaterialGroup> GetAllMaterialGroups(bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialGroup.ToList();
            }

            return _dbcontext.MaterialGroup.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();

        }




        public List<MaterialType> GetAllMaterialTypes(int materialGroupId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialType.Where(x => x.MaterialGroupId == materialGroupId).ToList();
            }
            var materialTypes = _dbcontext.MaterialType.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return materialTypes.Where(x => x.MaterialGroupId == materialGroupId).ToList();

        }

        public List<MaterialType> GetAllMaterialRatings(int materialTypeId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).ToList();
            }
            var rating = _dbcontext.MaterialType.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return rating.Where(x => x.Id == materialTypeId).ToList();
        }

        public List<Material> GetAllMaterialCodes(int materialTypeId, bool? onlyActive = false)
        {
            if (!onlyActive.HasValue)
            {
                return _dbcontext.Material.Where(x => x.MaterialTypeId == materialTypeId).ToList();
            }
            var materialCodes = _dbcontext.Material.Where(x => (onlyActive.Value && x.IsActive) || (!onlyActive.Value)).ToList();
            return materialCodes.Where(x => x.MaterialTypeId == materialTypeId).ToList();
        }
        public int AddStock(Stock stock)
        {
            _dbcontext.Set<Stock>().Add(stock);
            _dbcontext.SaveChanges();
            return stock.Id;
        }

        public int AddStockMaterial(StockMaterial stockMaterial)
        {
            _dbcontext.Set<StockMaterial>().Add(stockMaterial);
            _dbcontext.SaveChanges();
            return stockMaterial.Id;
        }
        public void AddStockMaterialSeries(StockMaterialSeries stockMaterialSeries)
        {
            _dbcontext.Set<StockMaterialSeries>().AddRange(stockMaterialSeries);
            _dbcontext.SaveChanges();
        }

        public List<StockInModel> GetStockInModels()
        {
            var stockInModels = _dbcontext.Stock
               .Select(s => new StockInModel
               {
                   Stock = s,
                   MaterialGroup = _dbcontext.MaterialGroup.Where(mg => mg.Id == s.MaterialGroupId).Select(mg => mg.Name).FirstOrDefault(),
                   MaterialName = _dbcontext.MaterialType.Where(mt => mt.Id == s.MaterialTypeId).Select(mt => mt.Name).FirstOrDefault(),
                   MaterialCode = _dbcontext.Material.Where(mt => mt.Id == s.MaterialId).Select(mt => mt.Code).FirstOrDefault(),
                   Quantity = _dbcontext.StockMaterial.Where(sm => sm.StockId == s.Id).Sum(sm => sm.Quantity)
               })
               .ToList();

            return stockInModels;
        }

        public string GetMaterialGroupById(int? materialGroupId)
        {
            var response = _dbcontext.MaterialGroup.Where(x => x.Id == materialGroupId).Select(x => x.Name).FirstOrDefault();
            string materialGroupName = response.ToString();
            return materialGroupName;
        }
        public string GetMaterialTypeById(int? materialTypeId)
        {
            var response = _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).Select(x => x.Name).FirstOrDefault();
            string materialTypeName = response.ToString();
            return materialTypeName;
        }
        public string GetMaterialCodeById(int? materialIdByCode)
        {
            var response = _dbcontext.Material.Where(x => x.Id == materialIdByCode).Select(x => x.Code).FirstOrDefault();
            if (response == null)
            {
                return "None";
            }
            string materialCodeName = response.ToString();
            return materialCodeName;
        }
        public string GetRatingNameById(int? materialTypeId)
        {
            var response = _dbcontext.MaterialType.Where(x => x.Id == materialTypeId).Select(x => x.Rating).FirstOrDefault();
            if (response == null)
            {
                return "None";
            }
            string Rating = response.ToString();
            return Rating;
        }
    }
}
