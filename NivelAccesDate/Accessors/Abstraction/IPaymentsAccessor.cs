using LibrarieModele;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelAccesDate.Accessors.Abstraction
{
    public interface IPaymentsAccessor
    {
        Task<List<Payment>> GetPayments();
        Task<Payment> GetPayment(int id);
        Task CreatePayment(Payment payment);
        Task UpdatePayment(Payment payment);
        Task DeletePayment(int id);
        Task<bool> PaymentExists(int id);
    }
}
