package uy.com.searchandfind.tender;


import java.util.List;

import uy.com.searchandfind.saler.FullSalerDTO;


/**
 * Created by strat on 03/11/2017.
 */

public class TenderDTO {

    private String Description ;
    private List<String> Images;
    private double PointsFromClient ;
    private double PointsFromSaler ;
    private String QueryId ;
    private String ReviewFromClient ;
    private String ReviewFromSaler ;
    private double TenderAmount ;
    private String TenderId ;
    private FullSalerDTO SalerDTO;
    private String State;

    public String getDescription() {
        return Description;
    }

    public void setDescription(String description) {
        Description = description;
    }

    public List<String> getImages() {
        return Images;
    }

    public void setImages(List<String> images) {
        Images = images;
    }

    public double getPointsFromClient() {
        return PointsFromClient;
    }

    public void setPointsFromClient(double pointsFromClient) {
        PointsFromClient = pointsFromClient;
    }

    public double getPointsFromSaler() {
        return PointsFromSaler;
    }

    public void setPointsFromSaler(double pointsFromSaler) {
        PointsFromSaler = pointsFromSaler;
    }

    public String getQueryId() {
        return QueryId;
    }

    public void setQueryId(String queryId) {
        QueryId = queryId;
    }

    public String getReviewFromClient() {
        return ReviewFromClient;
    }

    public void setReviewFromClient(String reviewFromClient) {
        ReviewFromClient = reviewFromClient;
    }

    public String getReviewFromSaler() {
        return ReviewFromSaler;
    }

    public void setReviewFromSaler(String reviewFromSaler) {
        ReviewFromSaler = reviewFromSaler;
    }

    public double getTenderAmount() {
        return TenderAmount;
    }

    public void setTenderAmount(double tenderAmount) {
        TenderAmount = tenderAmount;
    }

    public String getTenderId() {
        return TenderId;
    }

    public void setTenderId(String tenderId) {
        TenderId = tenderId;
    }

    public FullSalerDTO getSalerDTO() {
        if(SalerDTO==null){
            SalerDTO=new FullSalerDTO();
        }
        return SalerDTO;
    }

    public void setFullSalerDTO(FullSalerDTO saler) {
        this.SalerDTO = saler;
    }




    public String getState() {
        return State;
    }

    public void setState(String state) {
        this.State = state;
    }
}
