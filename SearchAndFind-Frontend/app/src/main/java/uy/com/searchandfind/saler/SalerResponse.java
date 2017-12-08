package uy.com.searchandfind.saler;



import java.util.Collection;

import uy.com.searchandfind.Response;
public class SalerResponse extends Response {

    private SalerDTO SalerDTO;
    public Collection<SalerCategoryDTO> SalerCategoryDTO;

    public Collection<SalerCategoryDTO> getSalerCategoryDTO() {
        return SalerCategoryDTO;
    }

    public void setSalerCategoryDTO(Collection<SalerCategoryDTO> salerCategoryDTO) {
        SalerCategoryDTO = salerCategoryDTO;
    }

    public SalerResponse(String message){
        super(message);
    }


    public SalerDTO getSalerDTO() {
        return SalerDTO;
    }

    public void setSalerDTO(SalerDTO salerDTO) {
        SalerDTO = salerDTO;
    }


}

