using LibrarieModele.DTOs;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NivelService.Abstraction
{
    public interface IPaymentsService
    {
        Task<List<PaymentDTO>> GetPayments();
        Task<PaymentDTO> GetPayment(int id);
        Task CreatePayment(PaymentDTO paymentDTO);
        Task<PaymentDTO> UpdatePayment(PaymentDTO paymentDTO, int id);
        Task DeletePayment(int id);
        Task<bool> PaymentExists(int id);
    }
}
