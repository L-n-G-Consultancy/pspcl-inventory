using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Pspcl.Core.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pspcl.Services
{ 
     public class StockMappingService
{
    public readonly IMapper _mapper;
        public Entity entity;

        public StockMappingService()
    {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Model, Entity>();
                cfg.CreateMap<Entity, Model>();

            });
            _mapper = config.CreateMapper();
    }
        public Entity Stock(Model model)
        {
            return _mapper.Map<Model,Entity>(model);
        }
        public Model StockViewModel(Entity entity) 
        {
            return _mapper.Map<Entity, Model>(entity);
        }
       
}
}
