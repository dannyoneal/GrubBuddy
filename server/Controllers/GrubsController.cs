using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using GrubBuddy.Models;
using GrubBuddy.DataAccess;

namespace GrubBuddy.Controllers
{
    public class GrubsController : Controller
    {
        IGrubsDac _grubsDac;
        public GrubsController(IGrubsDac grubsDac)
        {
            _grubsDac = grubsDac;  
        }

        [HttpGet]
        public IEnumerable<Grubs> Get() {
            return _grubsDac.Get();
        }
        [HttpGet]
        public IEnumerable<Grubs> GetByName(string name) {
            return _grubsDac.GetByName(name);
        }
    }
}


