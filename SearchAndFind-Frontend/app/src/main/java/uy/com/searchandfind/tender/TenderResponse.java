package uy.com.searchandfind.tender;

import java.util.Collection;
import java.util.List;

import uy.com.searchandfind.Response;
import uy.com.searchandfind.saler.SalerDTO;

public class TenderResponse extends Response {

    public TenderResponse(String message){
        super(message);
    }
    private SalerDTO SalerDTO;
    private TenderDTO TenderDTO;
    private List<TenderDTO> Tenders;

    public uy.com.searchandfind.saler.SalerDTO getSalerDTO() {
        return SalerDTO;
    }

    public uy.com.searchandfind.tender.TenderDTO getTenderDTO() {
        return TenderDTO;
    }

    public void setTenderDTO(TenderDTO tenderDTO) {
        TenderDTO = tenderDTO;
    }

    public List<TenderDTO> getTenders() {
        return Tenders;
    }

    public void setTenders(List<TenderDTO> tenders) {
        Tenders = tenders;
    }

    public void setSalerDTO(SalerDTO salerDTO) {
        SalerDTO = salerDTO;
    }
}
