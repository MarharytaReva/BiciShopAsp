using AutoMapper;
using BLL.DTO;
using DAL;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class HandlePhaseService : ServiceBase<HandlePhase, HandlePhaseDTO>
    {
        public HandlePhaseService(IRepository<HandlePhase> repo) : base(repo)
        {
            MapperConfiguration configuration = new MapperConfiguration(x =>
            {
                x.CreateMap<HandlePhase, HandlePhaseDTO>().ReverseMap();
            });
            mapper = new Mapper(configuration);
        }
        public async Task<IEnumerable<HandlePhaseDTO>> GetAllAsync()
        {
            return await Task.Run(() =>
            {
                var collection = repo.GetAll();
                return mapper.Map<IEnumerable<HandlePhase>, IEnumerable<HandlePhaseDTO>>(collection);
            });
        }
        public async Task<HandlePhaseDTO> GetDefaultPhase()
        {
            return await Task.Run(() =>
            {
                string phaseName = "sended to processing";
                HandlePhase phase = repo.GetAll().FirstOrDefault(x => x.PhaseName.Contains(phaseName));
                return mapper.Map<HandlePhase, HandlePhaseDTO>(phase);
            });
        }
    }
}
