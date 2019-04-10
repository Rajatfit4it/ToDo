using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using vm = ViewModel;
using dm = DAL.Domain;

namespace WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<dm.Category, vm.Category>().ReverseMap();
        }
    }
}
