package uy.com.searchandfind.saler;

import uy.com.searchandfind.category.CategoryDTO;

/**
 * Created by Fede2 on 14/10/2017.
 */

public class SalerCategoryDTO {

    private boolean HasCategory;
    private CategoryDTO Category;
    private boolean IsUpdated;

    public boolean getHasCategory() {
        return HasCategory;
    }

    public void setHasCategory(boolean hasCategory) {
        this.HasCategory = hasCategory;
    }

    public CategoryDTO getCategory() {
        return Category;
    }

    public void setHasCategory(CategoryDTO category) {
        this.Category = category;
    }

    public boolean getIsUpdated() {
        return IsUpdated;
    }

    public void setIsUpdated(boolean isUpdated) {
        this.IsUpdated = isUpdated;
    }
}
