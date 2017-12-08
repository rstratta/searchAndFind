package uy.com.searchandfind.tender;

import java.util.Collection;

import uy.com.searchandfind.Request;
import uy.com.searchandfind.saler.SalerCategoryDTO;

public class TenderRequest extends Request {
    private String TenderDescription;
    private double TenderAmount;
    private String TenderId;
    private String TenderState;
    private String QueryId;
    private Collection<String> Images;

    public String getTenderDescription() {
        return TenderDescription;
    }

    public void setTenderDescription(String tenderDescription) {
        TenderDescription = tenderDescription;
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

    public String getTenderState() {
        return TenderState;
    }

    public void setTenderState(String tenderState) {
        TenderState = tenderState;
    }

    public String getQueryId() {
        return QueryId;
    }

    public void setQueryId(String queryId) {
        QueryId = queryId;
    }

    public Collection<String> getImages() {
        return Images;
    }

    public void setImages(Collection<String> images) {
        Images = images;
    }
}