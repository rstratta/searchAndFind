package uy.com.searchandfind.saler;

import java.util.Collection;

/**
 * Created by Fede2 on 05/11/2017.
 */

public class FullSalerDTO extends SalerDTO {

    public Collection<SalerCategoryDTO> SalerCategoryDTO;



    public Collection<uy.com.searchandfind.saler.SalerCategoryDTO> getSalerCategoryDTO() {
        return SalerCategoryDTO;
    }

    public void setSalerCategoryDTO(Collection<uy.com.searchandfind.saler.SalerCategoryDTO> salerCategoryDTO) {
        SalerCategoryDTO = salerCategoryDTO;
    }
}
