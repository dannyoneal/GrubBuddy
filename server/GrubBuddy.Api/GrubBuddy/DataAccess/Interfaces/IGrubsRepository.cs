using GrubBuddy.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrubBuddy.DataAccess.Interfaces
{
    public interface IGrubsRepository
    {
        IEnumerable<Grub> Get();
        IEnumerable<Grub> GetByName(string name);
        Task<Grub> Insert(Grub grub);
    }
}
