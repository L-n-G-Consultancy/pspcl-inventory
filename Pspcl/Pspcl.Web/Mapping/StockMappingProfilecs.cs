using Pspcl.Web.Models;
using System.Collections;
using AutoMapper;
using System.Collections.Generic;
using Pspcl.Core.Domain;

namespace Pspcl.Web.Mapping
{
    public class StockMappingProfilecs:Profile
    {
        public StockMappingProfilecs()
        {
            CreateMap <StockViewModel, Stock>();
            CreateMap <Stock, StockViewModel>();
        }
    }
}
