using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GrubBuddy.Models;
using GrubBuddy.DataAccess;
using System.Threading.Tasks;
using GrubBuddy.Responses;

namespace GrubBuddy.Api.Controllers
{
    public class GrubsController : Controller
    {
        readonly IGrubsRepository _grubsRepository;
        public GrubsController(IGrubsRepository grubsRepository)
        {
            _grubsRepository = grubsRepository;
        }

        [HttpGet]
        public IEnumerable<Grub> Get()
        {
            return _grubsRepository.Get();
        }

        [HttpGet]
        public IEnumerable<Grub> GetByName(string name)
        {
            return _grubsRepository.GetByName(name);
        }

        [HttpPost]
        public async Task<ApiResponse> Create([FromBody]Grub grub)
        {
            var response = new ApiResponse();
            try
            {
                var insertedGrub = await _grubsRepository.Insert(grub);
                response.Result = insertedGrub;
            }
            catch (Exception ex) //todo log this
            {
                response.Errors = new []
                {
                    "An internal server error has occured while saving the grub"
                };
            }

            return response;
        }
    }
}


