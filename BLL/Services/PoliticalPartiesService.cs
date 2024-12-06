using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IPoliticalPartiesService
    {
        public IQueryable<PoliticalPartiesModel> Query();

        public ServiceBase Create(PoliticalParties record);

        public ServiceBase Update(PoliticalParties record);

        public ServiceBase Delete(int id);
    }
    public class PoliticalPartiesService : ServiceBase , IPoliticalPartiesService
    {
        public PoliticalPartiesService(Db db) : base(db)
        {
        }

        public ServiceBase Create(PoliticalParties record)
        {
            if (_db.PoliticalParties.Any(s => s.Name.ToUpper() == record.Name.ToUpper().Trim()))
                return Error("This political party is existing in the system!");
            record.Name = record.Name?.Trim();
            _db.PoliticalParties.Add(record);
            _db.SaveChanges();
            return Success("Political Party is registered to elections successfuly !");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.PoliticalParties.Include(s => s.OblastParticipation).SingleOrDefault(s => s.Id == id);
            if (entity is null)
                return Error("Political Party is not found at our system!");
            if (entity.OblastParticipation.Any())
                return Error("Political Party is attending at least in one oblast! ");
            _db.PoliticalParties.Remove(entity);
            _db.SaveChanges();
            return Success("Political Party is deleted from attendence list for elections !");
        }

        public IQueryable<PoliticalPartiesModel> Query()
        {
            return _db.PoliticalParties.OrderBy(s => s.Name).Select(s => new PoliticalPartiesModel() {Record = s});
        }

        public ServiceBase Update(PoliticalParties politicalparties)
        {
            if (_db.PoliticalParties.Any(s => s.Id !=politicalparties.Id && s.Name.ToUpper() == politicalparties.Name.ToUpper().Trim()))
                return Error("This political party is existing in the system!");
            var entity = _db.PoliticalParties.SingleOrDefault(s => s.Id == politicalparties.Id);
            if (entity is null)
                return Error("Political Party is not found at our system!");
            entity.Name = politicalparties.Name?.Trim();
            entity.LastPercentage = politicalparties.LastPercentage;
            entity.RegDate = politicalparties.RegDate;
            _db.PoliticalParties.Update(entity);
            _db.SaveChanges();
            return Success("Political Party is registered to elections successfuly !");
        }
    }
}
