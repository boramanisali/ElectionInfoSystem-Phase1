﻿using BLL.DAL;
using BLL.Models;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IOblastsService
    {
        public IQueryable<OblastsModel> Query();
        public ServiceBase Create(Oblasts record);
        public ServiceBase Update(Oblasts oblasts);
        public ServiceBase Delete(int id);
    }

    public class OblastsService : ServiceBase, IOblastsService
    {
        public OblastsService(Db db) : base(db)
        {
            
        }

        public ServiceBase Create(Oblasts record)
        {
            if (_db.Oblasts.Any(s => s.OblastName.ToUpper() == record.OblastName.ToUpper().Trim()))
                return Error("This oblast is existing in the system!");
            record.OblastName = record.OblastName?.Trim();
            _db.Oblasts.Add(record);
            _db.SaveChanges();
            return Success("The oblast with deputy number is added to the system");
        }

        public ServiceBase Delete(int id)
        {
            var entity = _db.Oblasts.Include(o => o.Rayons).Include(o => o.Parties).SingleOrDefault(o => o.Id == id);
            if (entity is null)
                return Error("Oblast is not found at our system!");
            _db.Rayons.RemoveRange(entity.Rayons);
            _db.Participations.RemoveRange(entity.Parties);
            _db.Oblasts.Remove(entity);
            _db.SaveChanges();
            return Success("The oblast with deputy number is deleted from the system");
        }

        public IQueryable<OblastsModel> Query()
        {
            return _db.Oblasts.OrderBy(o => o.DeputyNumber).ThenBy(o => o.OblastName).Select(o => new OblastsModel() { Record = o});
        }

        public ServiceBase Update(Oblasts oblasts)
        {
            if (_db.Oblasts.Any(o => o.Id != oblasts.Id && o.OblastName.ToUpper() == oblasts.OblastName.ToUpper().Trim()))
                return Error("This oblast is existing in the system!");
            var entity = _db.Oblasts.Find(oblasts.Id);
            entity.OblastName = oblasts.OblastName?.Trim();
            entity.DeputyNumber = oblasts.DeputyNumber;
            _db.Oblasts.Update(entity);
            _db.SaveChanges();
            return Success("The oblast with deputy number is updated to the system");
        }
    }
}
