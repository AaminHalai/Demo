using AutoMapper;
using DemoAPI.MappingProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoAPI.App_Start
{
    public class ViewModelMappingConfig
    {
        public static void CreateMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DemoMappingProfile>();
            });
        }
    }
}