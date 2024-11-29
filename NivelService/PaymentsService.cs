using AutoMapper;
using LibrarieModele.DTOs;
using LibrarieModele;
using NivelAccesDate.Accessors.Abstraction;

using NivelService.Abstraction;

namespace NivelService
{
    public class PaymentsService : IPaymentsService
    {
        private readonly IMapper _mapper;
        private readonly IPaymentsAccessor _paymentsAccessor;

        public PaymentsService(IMapper mapper, IPaymentsAccessor paymentsAccessor)
        {
            _mapper = mapper;
            _paymentsAccessor = paymentsAccessor;
        }

        public async Task<List<PaymentDTO>> GetPayments()
        {
            var payments = await _paymentsAccessor.GetPayments();

            return payments.Select(ent => _mapper.Map<PaymentDTO>(ent)).ToList();
        }

        public async Task<PaymentDTO> GetPayment(int id)
        {
            return _mapper.Map<PaymentDTO>(await _paymentsAccessor.GetPayment(id));
        }

        public async Task CreatePayment(PaymentDTO paymentDTO)
        {
            var toEntity = _mapper.Map<Payment>(paymentDTO);

            await _paymentsAccessor.CreatePayment(toEntity);
        }

        public async Task<PaymentDTO> UpdatePayment(PaymentDTO paymentDTO, int id)
        {
            var foundPayment = await _paymentsAccessor.GetPayment(id);
            if (foundPayment == null)
            {
                throw new ArgumentException("Wrong Id given.");
            }

            _mapper.Map(paymentDTO, foundPayment);

            await _paymentsAccessor.UpdatePayment(foundPayment);

            return await GetPayment(id);
        }

        public async Task DeletePayment(int id)
        {
            await _paymentsAccessor.DeletePayment(id);
        }

        public async Task<bool> PaymentExists(int id)
        {
            return await _paymentsAccessor.PaymentExists(id);
        }
    }
}
