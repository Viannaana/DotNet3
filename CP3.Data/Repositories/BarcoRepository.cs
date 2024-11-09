using CP3.Data.AppData;
using CP3.Domain.Entities;
using CP3.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CP3.Data.Repositories
{
    public class BarcoRepository : IBarcoRepository
    {
        private readonly BarcoApplicationContext _context;  // Alterado para BarcoApplicationContext

        public BarcoRepository(BarcoApplicationContext context)  // Alterado para BarcoApplicationContext
        {
            _context = context;
        }

        public IEnumerable<BarcoEntity> ObterTodos()
        {
            return _context.Barcos.ToList();  // Certifique-se de que a tabela se chama "Barcos"
        }

        public BarcoEntity? ObterPorId(int id)
        {
            return _context.Barcos.Find(id);  // Certifique-se de que a tabela se chama "Barcos"
        }

        public BarcoEntity? Adicionar(BarcoEntity barco)
        {
            _context.Barcos.Add(barco);  // Certifique-se de que a tabela se chama "Barcos"
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Editar(BarcoEntity barco)
        {
            _context.Barcos.Update(barco);  // Certifique-se de que a tabela se chama "Barcos"
            _context.SaveChanges();
            return barco;
        }

        public BarcoEntity? Remover(int id)
        {
            var barco = _context.Barcos.Find(id);  // Certifique-se de que a tabela se chama "Barcos"
            if (barco != null)
            {
                _context.Barcos.Remove(barco);  // Certifique-se de que a tabela se chama "Barcos"
                _context.SaveChanges();
            }
            return barco;
        }
    }
}