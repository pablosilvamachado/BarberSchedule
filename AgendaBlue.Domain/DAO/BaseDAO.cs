﻿using Microsoft.EntityFrameworkCore;

namespace AgendaBlue.Domain.DAO
{
    public class BaseDAO<T> : IDAO<T> where T : class
    {
        public AgendamentoContext _context { get; set; }

        public BaseDAO()
        {
            _context = new AgendamentoContext();
        }
        public virtual T Get(long id)
        {

            var obj = _context.Set<T>().Find(id);
            if (obj == null)
            {
                throw new OperationCanceledException("Could not find any with the given Id");
            }
            return obj;

        }



        public virtual T Create(T obj)
        {
            _context.Add(obj);
            _context.SaveChanges();

            return obj;

        }
        public virtual T Update(T obj)
        {
            using (_context = new AgendamentoContext())
            {
                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();

                return obj;
            }

        }
        public virtual void Delete(T obj)
        {
            using (_context = new AgendamentoContext())
            {
                if (obj != null)
                {
                    _context.Remove(obj);
                    _context.SaveChanges();
                }
            }

        }
        public virtual void Delete(long id)
        {
            using (_context = new AgendamentoContext())
            {
                var obj = Get(id);

                if (obj != null)
                {
                    _context.Remove(obj);
                    _context.SaveChanges();
                }
            }
        }
        public virtual IEnumerable<T> GetAll()
        {
            var _context = new AgendamentoContext();
            return _context.Set<T>();

        }

        public object GetAll(DateTime d)
        {
            var obj = _context.Set<T>().Find(d);
            if (obj == null)
            {
                throw new OperationCanceledException("Could not find any with the given Date");
            }
            return obj;
        }

        public object GetAll(string v)
        {
            var obj = _context.Set<T>().Find(v);
            if (obj == null)
            {
                throw new OperationCanceledException("Could not find any with the given string");
            }
            return obj;
        }
    }
}
