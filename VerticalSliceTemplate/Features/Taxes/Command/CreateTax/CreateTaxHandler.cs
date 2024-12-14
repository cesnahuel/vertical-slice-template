using MediatR;
using VerticalSliceTemplate.Infrastructure.Persistence;

namespace VerticalSliceTemplate.Features.Taxes.Command.CreateTax
{
    public record CreateTaxCommand(string Group_id, Guid FiscalFolioUUID, int Year) : IRequest<CreateTaxResponse>;
    public class CreateTaxHandler : IRequestHandler<CreateTaxCommand, CreateTaxResponse>
    {
        private readonly ILogger<CreateTaxHandler> _logger;
        private readonly ApplicationDbContext _context;

        public CreateTaxHandler(
            ILogger<CreateTaxHandler> logger,
            ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<CreateTaxResponse> Handle(CreateTaxCommand request, CancellationToken cancellationToken)
        {
            var newTax = new Entities.Tax();

            _context.Tax.Add(newTax);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Created new tax  with id for {newTax.Id}");

            return new CreateTaxResponse(newTax.Id);
        }
    }

    public record CreateTaxResponse(int Id);
}
