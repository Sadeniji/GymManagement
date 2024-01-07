using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ErrorOr;
using GymManagement.Application.Common.Interfaces;
using MediatR;

namespace GymManagement.Application.Subscriptions.Commands.DeleteSubscription
{
    public class DeleteSubscriptionCommandHandler : IRequestHandler<DeleteSubscriptionCommand, ErrorOr<Deleted>>
    {
        private readonly IAdminsRepository _adminsRepository;
        private readonly ISubscriptionsRepository _subscriptionsRepository;
        private readonly IGymsRepository _gymsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSubscriptionCommandHandler(
            ISubscriptionsRepository subscriptionsRepository, 
            IUnitOfWork unitOfWork, 
            IAdminsRepository adminsRepository, 
            IGymsRepository gymsRepository)
        {
            _subscriptionsRepository = subscriptionsRepository;
            _unitOfWork = unitOfWork;
            _adminsRepository = adminsRepository;
            _gymsRepository = gymsRepository;
        }

        public async Task<ErrorOr<Deleted>> Handle(DeleteSubscriptionCommand command, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionsRepository.GetSubscriptionByIdAsync(command.SubscriptionId);

            if (subscription == null)
            {
                return Error.NotFound(description: "Subscription not found");
            }

            var admin = await _adminsRepository.GetAdminByIdAsync(subscription.AdminId);

            if (admin == null)
            {
                return Error.NotFound(description: "Admin not found");
            }

            admin.DeleteSubscription(command.SubscriptionId);

            var gymsToDelete = await _gymsRepository.ListGymsBySubscriptionIdAsync(command.SubscriptionId);

            await _adminsRepository.UpdateAdminAsync(admin);
            await _subscriptionsRepository.RemoveSubscriptionAsync(subscription);
            await _gymsRepository.RemoveGymsRangeAsync(gymsToDelete);
            await _unitOfWork.CommitChangesAsync();

            return Result.Deleted;
        }
    }
}