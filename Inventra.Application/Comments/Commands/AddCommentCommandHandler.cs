using Inventra.Application.Interfaces;
using Inventra.Domain.Entities;
using Inventra.Domain.Interfaces;
using MediatR;

namespace Inventra.Application.Comments.Commands
{
    public class AddCommentCommandHandler : IRequestHandler<AddCommentCommand>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public AddCommentCommandHandler(ICommentRepository commentRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(AddCommentCommand request, CancellationToken cancellationToken)
        {
            var comment = new Comment
            {
                InventoryId = request.InventoryId,
                AuthorId = request.AuthorId,
                Content = request.Content,
                CreatedAt = DateTime.UtcNow
            };
            await _commentRepository.AddAsync(comment);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
