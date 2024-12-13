
using LibrarieModele;

using Microsoft.EntityFrameworkCore;
using NivelAccesDate.Accessors.Abstraction;

using Repository_CodeFirst;

namespace NivelAccesDate.Accessors
{
    public class PaymentsAccessor : IPaymentsAccessor
    {
        private readonly FitnessDBContext _context;

        public PaymentsAccessor(FitnessDBContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        public async Task<Payment> GetPayment(int id)
        {
            return await _context.Payments.FirstOrDefaultAsync(m => m.PaymentId == id);
        }

        public async Task<Payment> CreatePayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();

            return payment;
        }

        public async Task UpdatePayment(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePayment(int id)
        {
            var payment = await _context.Payments.FirstOrDefaultAsync(m => m.PaymentId == id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
        }
        public async Task<bool> PaymentExists(int id)
        {
            return await _context.Payments.AnyAsync(u => u.PaymentId == id);
        }
    }
}
