using System;
using SearchAndFind.DTO;
using SearchAndFind.Entities;
using System.Collections.Generic;

namespace SearchAndFind.Core
{
    public class TenderManager : ITenderManager
    {
        private ITenderRepository tenderRepository;
        private IQueryRepository queryRepository;
        private IDTOBuilder<TenderDTO, Tender> tenderDTOBuilder;
        private IUserRepository<Saler> salerRepository;
        private IDTOBuilder<FullSalerDTO, Saler> salerDTOBuilder;
        private IReviewRepository reviewRepository;
        
        public string QueryPENDING_STATE { get; private set; }

        public TenderManager(ITenderRepository repository, IQueryRepository queryRepo, IDTOBuilder<TenderDTO, Tender> dtoBuilder, IUserRepository<Saler> salerRepo, IDTOBuilder<FullSalerDTO, Saler> salerDTOBuild, IReviewRepository reviewRepo)
        {
            tenderRepository = repository;
            queryRepository = queryRepo;
            tenderDTOBuilder = dtoBuilder;
            salerRepository = salerRepo;
            salerDTOBuilder = salerDTOBuild;
            reviewRepository = reviewRepo;
        }
        public TenderDTO DoTender(TenderRequest request)
        {
            Query query = queryRepository.GetById(Guid.Parse(request.QueryId));
            ValidateQueryState(query);
            ValidateUniqueTenderBySaler(request);
            Tender tender = BuildTenderFromRequest(request);
            tender.ClientId = query.ClientId;
            BuildImagesFromRequest(tender, request);
            SaveTender(tender);
            return GetTenderById(tender.Id.ToString()).TenderDTO;
        }

        private void updateQueryState(Query query)
        {
            query.State = Query.CONFIRMED_STATE;
            queryRepository.UpdateObject(query);
        }

        private void RemoveTender(Tender tender)
        {
            tenderRepository.RemoveObject(tender);
        }

        private void SaveTender(Tender tender)
        {
            try
            {
                tenderRepository.AddObject(tender);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Ocurrió un error al agregar su oferta");
            }
        }

        private void BuildImagesFromRequest(Tender tender, TenderRequest request)
        {
            ICollection<TenderImage> images = new List<TenderImage>();
            foreach (var image in request.Images)
            {
                if (!String.IsNullOrWhiteSpace(image))
                {
                    images.Add(new TenderImage() { TenderId = tender.Id, EncodedImage = image });
                }
            }
            tender.Images = images;
        }

        private void ValidateUniqueTenderBySaler(TenderRequest request)
        {
            try
            {
                Tender oldTender = tenderRepository.GetTenderBySalerIdAndQueryId(Guid.Parse(request.UserId), Guid.Parse(request.QueryId));
                if (oldTender != null)
                {
                    throw new ManagerException("Lo sentimos, usted ya realizo una oferta a esta consulta.");
                }
            }
            catch (RepositoryException) { }
        }

        private Tender BuildTenderFromRequest(TenderRequest request)
        {
            Tender tender = new Tender();
            tender.SalerId = Guid.Parse(request.UserId);
            tender.Amount = request.TenderAmount;
            tender.Description = request.TenderDescription;
            tender.QueryId = new Guid(request.QueryId);
            return tender;

        }

        private void ValidateQueryState(Query query)
        {
            if (query == null)
            {
                throw new ManagerException("Lo sentimos, no encontramos la consulta a la que oferta");
            }
            if (query.State.Equals(Query.CONFIRMED_STATE))
            {
                throw new ManagerException("Lo sentimos, la consulta ya ha sido confirmada por el cliente");
            }
            if (query.State.Equals(Query.CANCELED_STATE))
            {
                throw new ManagerException("Lo sentimos, la consulta ha sido cancelada por el cliente");
            }
        }

        public TenderResponse GetAceptedTendersByClientId(string clientId)
        {
            try
            {
                ICollection<Tender> tenders = tenderRepository.GetAceptedTendersByClientId(Guid.Parse(clientId));
                return BuildTenderResponse(tenders);
            }
            catch (RepositoryException)
            {
                return new TenderResponse("Error al obtener ofertas confirmadas");
            }
        }

        private TenderResponse BuildTenderResponse(ICollection<Tender> tenders)
        {
            ICollection<TenderDTO> tenderDTOList = new List<TenderDTO>();
            foreach (var tender in tenders)
            {
                TenderDTO dto = tenderDTOBuilder.BuildDTO(tender);
                FillReviewPoints(dto);
                tenderDTOList.Add(dto);
            }
            TenderResponse response = new TenderResponse();
            response.Tenders = tenderDTOList;
            return response;
        }

        private void FillReviewPoints(TenderDTO dto)
        {
            if (dto.ReviewFromClient !=  Guid.Empty.ToString())
            {
                dto.PointsFromClient = GetReviewPoints(Guid.Parse(dto.ReviewFromClient));
            }
            if (dto.ReviewFromSaler != Guid.Empty.ToString())
            {
                dto.PointsFromSaler= GetReviewPoints(Guid.Parse(dto.ReviewFromSaler));
            }
        }

        private double GetReviewPoints(Guid reviewId)
        {
            try { 
            Review review = reviewRepository.GetById(reviewId);
            return review.Points;
            }
            catch (RepositoryException)
            {
                return 0;
            }
        }

        public TenderResponse GetAceptedTendersBySalerId(string salerId)
        {
            try
            {
                ICollection<Tender> tenders = tenderRepository.GetAceptedTendersBySalerId(Guid.Parse(salerId));
                return BuildTenderResponse(tenders);
            }
            catch (RepositoryException)
            {
                return new TenderResponse("Error al obtener ofertas confirmadas");
            }
        }

        public TenderResponse GetTenderById(string tenderId)
        {
            try
            {
                Tender currentTender = tenderRepository.GetById(Guid.Parse(tenderId));
                TenderResponse response = new TenderResponse();
                TenderDTO dto = tenderDTOBuilder.BuildDTO(currentTender);
                dto.SalerDTO = salerDTOBuilder.BuildDTO(getSalerById(currentTender.SalerId));
                response.TenderDTO = dto;
                return response;
            }
            catch (RepositoryException)
            {
                return new TenderResponse("Error al obtener oferta");
            }
        }

        private Saler getSalerById(Guid salerId)
        {
            try
            {
                return salerRepository.GetById(salerId);
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al obtener vendedor de la oferta");
            }
        }

        public TenderDTO UpdateTenderState(TenderRequest request)
        {
            try
            {
                Tender tender = tenderRepository.GetById(Guid.Parse(request.TenderId));
                ValidateUpdateTenderState(tender);
                tender.State = request.TenderState;
                tenderRepository.UpdateObject(tender);
                TenderDTO tenderDTO = tenderDTOBuilder.BuildDTO(tender);
                tenderDTO.SalerDTO = salerDTOBuilder.BuildDTO(salerRepository.GetById(tender.SalerId));
                return tenderDTO;
            }
            catch (RepositoryException)
            {
                throw new ManagerException("Error al actualizar estado de oferta");
            }


        }

        private void ValidateUpdateTenderState(Tender tender)
        {
            if (tender == null)
            {
                throw new ManagerException("Lo sentimos, no encontramos la oferta que desea actualizar");
            }
            if (tender.State.Equals(Tender.ACEPT_TENDER))
            {
                throw new ManagerException("Lo sentimos, la oferta ya ha sido confirmada por el cliente");
            }
            if (tender.State.Equals(Tender.REVOKE_TENDER))
            {
                throw new ManagerException("Lo sentimos, la oferta ha sido descartada por el cliente");
            }
        }

        public TenderDTO RevokeTender(TenderRequest request)
        {
            request.TenderState = Tender.REVOKE_TENDER;
            return UpdateTenderState(request);
        }

        public TenderDTO ConfirmTender(TenderRequest request)
        {
            Query query = queryRepository.GetById(Guid.Parse(request.QueryId));
            request.TenderState = Tender.ACEPT_TENDER;
            TenderDTO tenderDTO = UpdateTenderState(request);
            RevokeOtherTenders(tenderDTO.TenderId, tenderDTO.QueryId);
            updateQueryState(query);
            return tenderDTO;
        }

        private void RevokeOtherTenders(string tenderId, string queryId)
        {
            try
            {
                ICollection<Tender> tenders = tenderRepository.GetTendersByQueryId(Guid.Parse(queryId));
                foreach (var tender in tenders)
                {
                    if (!tender.Id.ToString().Equals(tenderId))
                    {

                        RevokeTender(new TenderRequest() { TenderId = tender.Id.ToString() });

                    }
                }
            }
            catch (RepositoryException) { }
        }
    }
}
