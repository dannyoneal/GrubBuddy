using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using GrubBuddy.Models;
using GrubBuddy.DataAccess;
using System.Net.Http;

namespace GrubBuddy.Api.Controllers
{
    public class GrubsController : Controller
    {
        IGrubsDac _grubsDac;
        public GrubsController(IGrubsDac grubsDac)
        {
            _grubsDac = grubsDac;
        }

        [HttpGet]
        public IEnumerable<Grub> Get()
        {
            return _grubsDac.Get();
        }
        [HttpGet]
        public IEnumerable<Grub> GetByName(string name)
        {
            return _grubsDac.GetByName(name);
        }

        [HttpPost]
        public HttpResponseMessage Create([FromBody]Grub grub)
        {
            return new HttpResponseMessage();
        }
    }
}


