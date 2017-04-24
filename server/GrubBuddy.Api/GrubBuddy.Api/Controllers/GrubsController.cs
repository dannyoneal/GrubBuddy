using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GrubBuddy.Models;
using System.Threading.Tasks;
using GrubBuddy.Responses;
using Microsoft.Extensions.Logging;
using GrubBuddy.DataAccess.Interfaces;

namespace GrubBuddy.Api.Controllers
{
    public class GrubsController : Controller
    {
        private readonly IGrubsRepository _grubsRepository;
        private readonly ILogger _logger;
        public GrubsController(IGrubsRepository grubsRepository, ILoggerFactory loggerFactory)
        {
            _grubsRepository = grubsRepository;
            _logger = loggerFactory.CreateLogger<GrubsController>();
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
            catch (Exception ex)
            {
                _logger.LogError(new EventId(), ex, ex.StackTrace);
                response.Errors = new []
                {
                    "An internal server error has occured while saving the grub"
                };
            }

            return response;
        }
    }
}


