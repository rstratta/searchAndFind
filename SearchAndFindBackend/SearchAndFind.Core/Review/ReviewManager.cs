using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAndFind.DTO;
using SearchAndFind.Entities;

namespace SearchAndFind.Core
{
    public class ReviewManager : IReviewManager
    {
        private IReviewRepository reviewRepository;
        private IUserRepository<Client> clientRepository;
        private IUserRepository<Saler> salerRepository;
        private ITenderRepository tenderRepository;
        private IDTOBuilder<ReviewDTO, Review> dtoBuilder;

        public ReviewManager(IReviewRepository repository, IUserRepository<Client> clientRepo, IUserRepository<Saler> salerRepo, ITenderRepository tenderRepo, IDTOBuilder<ReviewDTO, Review> dtoBuild)
        {
            reviewRepository = repository;
            clientRepository = clientRepo;
            salerRepository = salerRepo;
            tenderRepository = tenderRepo;
            dtoBuilder = dtoBuild;
        }

        public ReviewDTO AddReviewFromClient(ReviewRequest request)
        {
            Tender tenderToReview = tenderRepository.GetById(Guid.Parse(request.TenderId));
            ValidateTender(tenderToReview, request);
            ValidateClientAsRemit(tenderToReview, request);
            Review review = BuildReviewFromData(request, tenderToReview.SalerId);
            try { 
                reviewRepository.AddObject(review);
                tenderToReview.ClientReviewId = review.Id;
                tenderRepository.UpdateObject(tenderToReview);
                UpdateClientData(request);
                return dtoBuilder.BuildDTO(review);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al registar review");
            }
        }

        private void UpdateClientData(ReviewRequest request)
        {
            Client client = clientRepository.GetById(Guid.Parse(request.UserId));
            client.AverageReview = CalculateNewAvg(client, request);
            client.NumberOfReview++;
            clientRepository.UpdateObject(client);
        }

        private double CalculateNewAvg(User user, ReviewRequest request)
        {
            double currentAvg = user.NumberOfReview * user.AverageReview;
            double newAvg = (currentAvg + request.Points) / (user.NumberOfReview + 1);
            return newAvg;
        }

        private Review BuildReviewFromData(ReviewRequest request, Guid destinationId)
        {
            Review review = new Review();
            review.OrigUserId = Guid.Parse(request.UserId);
            review.Points = request.Points;
            review.DestinationUserId = destinationId;
            return review;
        }

        private void ValidateClientAsRemit(Tender tenderToReview, ReviewRequest request)
        {
            if (!tenderToReview.ClientId.Equals(Guid.Parse(request.UserId)))
            {
                throw new ManagerException("No es posible registrar la review.");
            }
        }

        private void ValidateTender(Tender tenderToReview, ReviewRequest request)
        {
            if (!tenderToReview.State.Equals(Tender.ACEPT_TENDER))
            {
                throw new ManagerException("La oferta aún no está confirmada, no es posible realizar la Review");
            }
        }

        public ReviewDTO AddReviewFromSaler(ReviewRequest request)
        {
            Tender tenderToReview = tenderRepository.GetById(Guid.Parse(request.TenderId));
            ValidateTender(tenderToReview, request);
            ValidateSalerAsRemit(tenderToReview, request);
            Review review = BuildReviewFromData(request, tenderToReview.ClientId);
            try { 
                reviewRepository.AddObject(review);
                tenderToReview.SalerReviewId = review.Id;
                tenderRepository.UpdateObject(tenderToReview);
                UpdateSalerData(request);
                return dtoBuilder.BuildDTO(review);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al registrar review");
            }
        }


        private void UpdateSalerData(ReviewRequest request)
        {
            Saler saler = salerRepository.GetById(Guid.Parse(request.UserId));
            saler.AverageReview = CalculateNewAvg(saler, request);
            saler.NumberOfReview++;
            salerRepository.UpdateObject(saler);
        }
        private void ValidateSalerAsRemit(Tender tenderToReview, ReviewRequest request)
        {
            if (!tenderToReview.SalerId.Equals(Guid.Parse(request.UserId)))
            {
                throw new ManagerException("No es posible registrar la review.");
            }
        }
    }
}
